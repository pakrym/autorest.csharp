// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using AutoRest.CSharp.V3.Generation.Types;
using AutoRest.CSharp.V3.Output.Models.Shared;

namespace AutoRest.CSharp.V3.Output.Models.Responses
{
    internal class Response
    {
        public Response(ResponseBody? responseBody, StatusCode[] statusCodes)
        {
            ResponseBody = responseBody;
            StatusCodes = statusCodes;
        }

        public ResponseBody? ResponseBody { get; }
        public StatusCode[] StatusCodes { get; }
    }

    internal struct StatusCode
    {
        public int? Code { get; }
        public int? Family { get; } //4,3,2 etc.
    }

    internal class ConstantResponseBody: ResponseBody
    {
        public ConstantResponseBody(Constant value)
        {
            Value = value;
        }

        public override CSharpType Type => Value.Type;

        public Constant Value { get; }
    }
}
