// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure;
using Azure.Core;

namespace Azure.Storage.Blobs
{
    internal class ChangeLeaseHeaders
    {
        private readonly Response _response;
        public ChangeLeaseHeaders(Response response)
        {
            _response = response;
        }
        public string? ETag => _response.Headers.TryGetValue("ETag", out string? value) ? value : null;
        public DateTimeOffset? LastModified => _response.Headers.TryGetValue("Last-Modified", out DateTimeOffset? value) ? value : null;
        public string? XMsLeaseId => _response.Headers.TryGetValue("x-ms-lease-id", out string? value) ? value : null;
        public string? XMsClientRequestId => _response.Headers.TryGetValue("x-ms-client-request-id", out string? value) ? value : null;
        public string? XMsRequestId => _response.Headers.TryGetValue("x-ms-request-id", out string? value) ? value : null;
        public string? XMsVersion => _response.Headers.TryGetValue("x-ms-version", out string? value) ? value : null;
        public DateTimeOffset? Date => _response.Headers.TryGetValue("Date", out DateTimeOffset? value) ? value : null;
    }
}
