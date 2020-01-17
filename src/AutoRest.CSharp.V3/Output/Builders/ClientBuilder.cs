﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoRest.CSharp.V3.Input;
using AutoRest.CSharp.V3.Output.Models;
using AutoRest.CSharp.V3.Output.Models.Requests;
using AutoRest.CSharp.V3.Output.Models.Responses;
using AutoRest.CSharp.V3.Output.Models.Serialization;
using AutoRest.CSharp.V3.Output.Models.Shared;
using AutoRest.CSharp.V3.Output.Models.TypeReferences;
using AutoRest.CSharp.V3.Utilities;
using Azure.Core;
using Request = AutoRest.CSharp.V3.Output.Models.Requests.Request;

namespace AutoRest.CSharp.V3.Output.Builders
{
    internal class ClientBuilder
    {
        public static Client BuildClient(OperationGroup operationGroup)
        {
            var allClientParameters = operationGroup.Operations
                .SelectMany(op => op.Request.Parameters)
                .Where(p => p.Implementation == ImplementationLocation.Client)
                .Distinct();
            Dictionary<string, Parameter> clientParameters = new Dictionary<string, Parameter>();
            // Deduplication required because of https://github.com/Azure/autorest.modelerfour/issues/100
            foreach (RequestParameter clientParameter in allClientParameters)
            {
                clientParameters[clientParameter.Language.Default.Name] = BuildParameter(clientParameter);
            }

            string clientName = operationGroup.CSharpName();
            List<Method> methods = new List<Method>();
            List<Paging> pagingMethods = new List<Paging>();
            foreach (Operation operation in operationGroup.Operations)
            {
                var method = BuildMethod(operation, clientName, clientParameters);
                if (method != null)
                {
                    methods.Add(method);
                    var pageable = operation.Extensions.GetValue<IDictionary<object, object>>("x-ms-pageable");
                    if (pageable != null)
                    {
                        Method nextPageMethod = BuildNextPageMethod(method);
                        methods.Add(nextPageMethod);
                        //TODO: This is a hack since we don't have the model information at this point
                        var schemaForPaging = ((method.Response.ResponseBody as ObjectResponseBody)?.Type as SchemaTypeReference)?.Schema as ObjectSchema;
                        Paging pagingMethod = GetClientMethodPaging(method, nextPageMethod, pageable, schemaForPaging);
                        pagingMethods.Add(pagingMethod);
                    }
                }
            }

            return new Client(clientName,
                operationGroup.Language.Default.Description,
                OrderParameters(clientParameters.Values),
                methods.ToArray(),
                pagingMethods.ToArray());
        }

        private static Parameter[] OrderParameters(IEnumerable<Parameter> parameters)
        {
            return parameters.OrderBy(p => p.DefaultValue != null).ToArray();
        }

        private static Constant? CreateDefaultValueConstant(RequestParameter requestParameter)
        {
            if (requestParameter.ClientDefaultValue != null)
            {
                return BuilderHelpers.ParseClientConstant(
                    requestParameter.ClientDefaultValue,
                    (FrameworkTypeReference)BuilderHelpers.CreateType(requestParameter.Schema, requestParameter.IsNullable()));
            }

            return null;
        }

        private static Method? BuildMethod(Operation operation, string clientName, IReadOnlyDictionary<string, Parameter> clientParameters)
        {
            var httpRequest = operation.Request.Protocol.Http as HttpRequest;
            var httpRequestWithBody = httpRequest as HttpWithBodyRequest;

            //TODO: Handle multiple responses
            var response = operation.Responses.FirstOrDefault();
            var httpResponse = response?.Protocol.Http as HttpResponse;

            if (httpRequest == null || httpResponse == null)
            {
                return null;
            }

            Dictionary<string, PathSegment> uriParameters = new Dictionary<string, PathSegment>();
            Dictionary<string, PathSegment> pathParameters = new Dictionary<string, PathSegment>();
            List<QueryParameter> query = new List<QueryParameter>();
            List<RequestHeader> headers = new List<RequestHeader>();
            List<Parameter> methodParameters = new List<Parameter>();

            RequestBody? body = null;
            foreach (RequestParameter requestParameter in operation.Request.Parameters ?? Array.Empty<RequestParameter>())
            {
                string defaultName = requestParameter.Language.Default.Name;
                string serializedName = requestParameter.Language.Default.SerializedName ?? defaultName;
                ParameterOrConstant constantOrParameter;
                Schema valueSchema = requestParameter.Schema;

                if (requestParameter.Implementation == ImplementationLocation.Method)
                {
                    switch (requestParameter.Schema)
                    {
                        case ConstantSchema constant:
                            constantOrParameter = BuilderHelpers.ParseClientConstant(constant);
                            valueSchema = constant.ValueType;
                            break;
                        case BinarySchema _:
                            // skip
                            continue;
                        default:
                            constantOrParameter = BuildParameter(requestParameter);
                            break;
                    }

                    if (!constantOrParameter.IsConstant)
                    {
                        methodParameters.Add(constantOrParameter.Parameter);
                    }
                }
                else
                {
                    constantOrParameter = clientParameters[requestParameter.Language.Default.Name];
                }

                if (requestParameter.Protocol.Http is HttpParameter httpParameter)
                {
                    SerializationFormat serializationFormat = BuilderHelpers.GetSerializationFormat(valueSchema);
                    bool skipEncoding = requestParameter.Extensions!.TryGetValue("x-ms-skip-url-encoding", out var value) && Convert.ToBoolean(value);
                    switch (httpParameter.In)
                    {
                        case ParameterLocation.Header:
                            headers.Add(new RequestHeader(serializedName, constantOrParameter, serializationFormat));
                            break;
                        case ParameterLocation.Query:
                            query.Add(new QueryParameter(serializedName, constantOrParameter, GetSerializationStyle(httpParameter, valueSchema), !skipEncoding, serializationFormat));
                            break;
                        case ParameterLocation.Path:
                            pathParameters.Add(serializedName, new PathSegment(constantOrParameter, !skipEncoding, serializationFormat));
                            break;
                        case ParameterLocation.Body:
                            Debug.Assert(httpRequestWithBody != null);
                            var serialization = SerializationBuilder.Build(httpRequestWithBody.KnownMediaType, requestParameter.Schema, requestParameter.IsNullable());
                            body = new RequestBody(constantOrParameter, serialization);
                            break;
                        case ParameterLocation.Uri:
                            uriParameters[defaultName] = new PathSegment(constantOrParameter, !skipEncoding, serializationFormat);
                            break;
                    }
                }
            }

            if (httpRequestWithBody != null)
            {
                headers.AddRange(httpRequestWithBody.MediaTypes.Select(mediaType => new RequestHeader("Content-Type", BuilderHelpers.StringConstant(mediaType))));
            }

            var request = new Request(
                ToCoreRequestMethod(httpRequest.Method) ?? RequestMethod.Get,
                ToPathParts(httpRequest.Uri, uriParameters),
                ToPathParts(httpRequest.Path, pathParameters),
                query.ToArray(),
                headers.ToArray(),
                body
            );

            ResponseBody? responseBody = null;
            if (response is SchemaResponse schemaResponse)
            {
                var schema = schemaResponse.Schema is ConstantSchema constantSchema ? constantSchema.ValueType : schemaResponse.Schema;
                var responseType = BuilderHelpers.CreateType(schema, isNullable: false);

                ObjectSerialization serialization = SerializationBuilder.Build(httpResponse.KnownMediaType, schema, isNullable: false);

                responseBody = new ObjectResponseBody(responseType, serialization);
            }
            else if (response is BinaryResponse)
            {
                responseBody = new StreamResponseBody();
            }

            Response clientResponse = new Response(
                responseBody,
                httpResponse.StatusCodes.Select(ToStatusCode).ToArray(),
                BuildResponseHeaderModel(operation, httpResponse)
            );

            string operationName = operation.CSharpName();
            return new Method(
                operationName,
                BuilderHelpers.EscapeXmlDescription(operation.Language.Default.Description),
                request,
                OrderParameters(methodParameters),
                clientResponse,
                new Diagnostic($"{clientName}.{operationName}", Array.Empty<DiagnosticAttribute>())
            );
        }

        private static Method BuildNextPageMethod(Method method)
        {
            var nextPageUrlParameter = new Parameter(
                "nextLinkUrl",
                "The URL to the next page of results.",
                new FrameworkTypeReference(typeof(string)),
                null,
                true);
            var headerParameterNames = method.Request.Headers.Where(h => !h.Value.IsConstant).Select(h => h.Value.Parameter.Name).ToArray();
            var parameters = method.Parameters.Where(p =>  headerParameterNames.Contains(p.Name)).Append(nextPageUrlParameter).ToArray();
            var request = new Request(
                method.Request.Method,
                new[] { new PathSegment(nextPageUrlParameter, false, SerializationFormat.Default),  },
                Array.Empty<PathSegment>(),
                Array.Empty<QueryParameter>(),
                method.Request.Headers,
                null
            );

            return new Method(
                $"{method.Name}NextPage",
                method.Description,
                request,
                parameters,
                method.Response,
                method.Diagnostics);
        }

        private static Paging GetClientMethodPaging(Method method, Method nextPageMethod, IDictionary<object, object> pageable, ObjectSchema? schema)
        {
            var nextLinkName = pageable.GetValue<string>("nextLinkName");
            //TODO: This should actually reference an operation: https://github.com/Azure/autorest.csharp/issues/397
            var operationName = pageable.GetValue<string>("operationName");

            var itemName = pageable.GetValue<string>("itemName");
            //TODO: Hack to figure out the property name on the model
            var itemProperty = schema?.Properties?.FirstOrDefault(p => p.SerializedName == itemName);
            itemName = itemProperty?.CSharpName() ?? itemName ?? "Value";
            var nextLinkProperty = schema?.Properties?.FirstOrDefault(p => p.SerializedName == nextLinkName);
            nextLinkName = nextLinkProperty?.CSharpName() ?? nextLinkName;
            // If itemName resolves to Value, we can't use itemProperty. So, get the correct property.
            var itemTypeProperty = schema?.Properties?.FirstOrDefault(p => p.CSharpName() == itemName);
            var itemTypeValueSchema = (itemTypeProperty?.Schema as ArraySchema)?.ElementType;
            var itemType = BuilderHelpers.CreateType(itemTypeValueSchema ?? new Schema(), false);
            var name = $"{method.Name}Pageable";
            return new Paging(method, nextPageMethod, name, nextLinkName, itemName, itemType, operationName);
        }

        private static Parameter BuildParameter(RequestParameter requestParameter)
        {
            Constant? defaultValue = null;
            if (requestParameter.Schema is ConstantSchema constantSchema)
            {
                defaultValue = BuilderHelpers.ParseClientConstant(constantSchema);
            }

            return new Parameter(
                requestParameter.CSharpName(),
                CreateDescription(requestParameter),
                BuilderHelpers.CreateType(requestParameter.Schema, requestParameter.IsNullable()),
                CreateDefaultValueConstant(requestParameter) ?? defaultValue,
                requestParameter.Required == true);
        }

        private static ResponseHeaderGroupType? BuildResponseHeaderModel(Operation operation, HttpResponse httpResponse)
        {
            if (!httpResponse.Headers.Any())
            {
                return null;
            }

            ResponseHeader CreateResponseHeader(HttpResponseHeader header) =>
                new ResponseHeader(header.Header.ToCleanName(), header.Header, BuilderHelpers.CreateType(header.Schema, true));

            string operationName = operation.CSharpName();

            return new ResponseHeaderGroupType(
                operationName + "Headers",
                $"Header model for {operationName}",
                httpResponse.Headers.Select(CreateResponseHeader).ToArray()
                );
        }

        private static QuerySerializationStyle GetSerializationStyle(HttpParameter httpParameter, Schema valueSchema)
        {
            Debug.Assert(httpParameter.In == ParameterLocation.Query);

            switch (httpParameter.Style)
            {
                case null:
                case SerializationStyle.Form:
                    return valueSchema is ArraySchema ? QuerySerializationStyle.CommaDelimited : QuerySerializationStyle.Simple;
                case SerializationStyle.PipeDelimited:
                    return QuerySerializationStyle.PipeDelimited;
                case SerializationStyle.SpaceDelimited:
                    return QuerySerializationStyle.SpaceDelimited;
                case SerializationStyle.TabDelimited:
                    return QuerySerializationStyle.TabDelimited;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static ParameterOrConstant[] ToParts(string httpRequestUri, Dictionary<string, ParameterOrConstant> parameters)
        {
            List<ParameterOrConstant> host = new List<ParameterOrConstant>();
            foreach ((string text, bool isLiteral) in StringExtensions.GetPathParts(httpRequestUri))
            {
                host.Add(isLiteral ? BuilderHelpers.StringConstant(text) : parameters[text]);
            }

            return host.ToArray();
        }

        private static PathSegment[] ToPathParts(string httpRequestUri, Dictionary<string, PathSegment> parameters)
        {
            PathSegment TextSegment(string text)
            {
                return new PathSegment(BuilderHelpers.StringConstant(text), false, SerializationFormat.Default);
            }

            List<PathSegment> host = new List<PathSegment>();
            foreach ((string text, bool isLiteral) in StringExtensions.GetPathParts(httpRequestUri))
            {
                host.Add(isLiteral ? TextSegment(text) : parameters[text]);
            }

            return host.ToArray();
        }

        private static int ToStatusCode(StatusCodes arg) => int.Parse(arg.ToString().Trim('_'));

        private static RequestMethod? ToCoreRequestMethod(HttpMethod method) => method switch
        {
            HttpMethod.Delete => RequestMethod.Delete,
            HttpMethod.Get => RequestMethod.Get,
            HttpMethod.Head => RequestMethod.Head,
            HttpMethod.Options => (RequestMethod?)null,
            HttpMethod.Patch => RequestMethod.Patch,
            HttpMethod.Post => RequestMethod.Post,
            HttpMethod.Put => RequestMethod.Put,
            HttpMethod.Trace => null,
            _ => null
        };

        private static string CreateDescription(RequestParameter requestParameter)
        {
            return string.IsNullOrWhiteSpace(requestParameter.Language.Default.Description) ?
                $"The {requestParameter.Schema.Name} to use." :
                BuilderHelpers.EscapeXmlDescription(requestParameter.Language.Default.Description);
        }
    }
}
