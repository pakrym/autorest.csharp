// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure;
using Azure.Core;

namespace Azure.Storage.Blobs
{
    internal class GetAccessControlHeaders
    {
        private readonly Response _response;
        public GetAccessControlHeaders(Response response)
        {
            _response = response;
        }
        public DateTimeOffset? Date => _response.Headers.TryGetValue("Date", out DateTimeOffset? value) ? value : null;
        public string? ETag => _response.Headers.TryGetValue("ETag", out string? value) ? value : null;
        public DateTimeOffset? LastModified => _response.Headers.TryGetValue("Last-Modified", out DateTimeOffset? value) ? value : null;
        public string? XMsOwner => _response.Headers.TryGetValue("x-ms-owner", out string? value) ? value : null;
        public string? XMsGroup => _response.Headers.TryGetValue("x-ms-group", out string? value) ? value : null;
        public string? XMsPermissions => _response.Headers.TryGetValue("x-ms-permissions", out string? value) ? value : null;
        public string? XMsAcl => _response.Headers.TryGetValue("x-ms-acl", out string? value) ? value : null;
        public string? XMsRequestId => _response.Headers.TryGetValue("x-ms-request-id", out string? value) ? value : null;
        public string? XMsVersion => _response.Headers.TryGetValue("x-ms-version", out string? value) ? value : null;
    }
}
