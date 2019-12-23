// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using validation.Models.V100;

namespace validation
{
    internal partial class AllOperations
    {
        private string host;
        private string subscriptionId;
        private string ApiVersion;
        private string ApiVersion;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        public AllOperations(string host, string subscriptionId, string ApiVersion, string ApiVersion, ClientDiagnostics clientDiagnostics, HttpPipeline pipeline)
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }
            if (subscriptionId == null)
            {
                throw new ArgumentNullException(nameof(subscriptionId));
            }
            if (ApiVersion == null)
            {
                throw new ArgumentNullException(nameof(ApiVersion));
            }
            if (ApiVersion == null)
            {
                throw new ArgumentNullException(nameof(ApiVersion));
            }

            this.host = host;
            this.subscriptionId = subscriptionId;
            this.ApiVersion = ApiVersion;
            this.ApiVersion = ApiVersion;
            this.clientDiagnostics = clientDiagnostics;
            this.pipeline = pipeline;
        }
        public async ValueTask<Response<Product>> ValidationOfMethodParametersAsync(string resourceGroupName, int id, CancellationToken cancellationToken = default)
        {
            if (resourceGroupName == null)
            {
                throw new ArgumentNullException(nameof(resourceGroupName));
            }

            using var scope = clientDiagnostics.CreateScope("validation.ValidationOfMethodParameters");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}"));
                request.Uri.AppendPath("/fakepath/", false);
                request.Uri.AppendPath(subscriptionId, true);
                request.Uri.AppendPath("/", false);
                request.Uri.AppendPath(resourceGroupName, true);
                request.Uri.AppendPath("/", false);
                request.Uri.AppendPath(id, true);
                request.Uri.AppendQuery("apiVersion", ApiVersion, true);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                switch (response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            var value = Product.DeserializeProduct(document.RootElement);
                            return Response.FromValue(value, response);
                        }
                    default:
                        throw new Exception();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        public async ValueTask<Response<Product>> ValidationOfBodyAsync(string resourceGroupName, int id, Product? body, CancellationToken cancellationToken = default)
        {
            if (resourceGroupName == null)
            {
                throw new ArgumentNullException(nameof(resourceGroupName));
            }

            using var scope = clientDiagnostics.CreateScope("validation.ValidationOfBody");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Put;
                request.Uri.Reset(new Uri($"{host}"));
                request.Uri.AppendPath("/fakepath/", false);
                request.Uri.AppendPath(subscriptionId, true);
                request.Uri.AppendPath("/", false);
                request.Uri.AppendPath(resourceGroupName, true);
                request.Uri.AppendPath("/", false);
                request.Uri.AppendPath(id, true);
                request.Headers.Add("Content-Type", "application/json");
                request.Uri.AppendQuery("apiVersion", ApiVersion, true);
                using var content = new Utf8JsonRequestContent();
                content.JsonWriter.WriteObjectValue(body);
                request.Content = content;
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                switch (response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            var value = Product.DeserializeProduct(document.RootElement);
                            return Response.FromValue(value, response);
                        }
                    default:
                        throw new Exception();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        public async ValueTask<Response> GetWithConstantInPathAsync(CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("validation.GetWithConstantInPath");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}"));
                request.Uri.AppendPath("/validation/constantsInPath/", false);
                request.Uri.AppendPath("constant", true);
                request.Uri.AppendPath("/value", false);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                switch (response.Status)
                {
                    case 200:
                        return response;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        public async ValueTask<Response<Product>> PostWithConstantInBodyAsync(Product? body, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("validation.PostWithConstantInBody");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Post;
                request.Uri.Reset(new Uri($"{host}"));
                request.Uri.AppendPath("/validation/constantsInPath/", false);
                request.Uri.AppendPath("constant", true);
                request.Uri.AppendPath("/value", false);
                request.Headers.Add("Content-Type", "application/json");
                using var content = new Utf8JsonRequestContent();
                content.JsonWriter.WriteObjectValue(body);
                request.Content = content;
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                switch (response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            var value = Product.DeserializeProduct(document.RootElement);
                            return Response.FromValue(value, response);
                        }
                    default:
                        throw new Exception();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
    }
}
