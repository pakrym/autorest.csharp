// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure;
using Azure.Core;

namespace Azure.Storage.Blobs
{
    internal class ListBlobContainersSegmentHeaders
    {
        private readonly Azure.Response _response;
        public ListBlobContainersSegmentHeaders(Azure.Response response)
        {
            _response = response;
        }
        public string? XMsClientRequestId => _response.Headers.TryGetValue("x-ms-client-request-id", out string? value) ? value : null;
        public string? XMsRequestId => _response.Headers.TryGetValue("x-ms-request-id", out string? value) ? value : null;
        public string? XMsVersion => _response.Headers.TryGetValue("x-ms-version", out string? value) ? value : null;
    }
}
