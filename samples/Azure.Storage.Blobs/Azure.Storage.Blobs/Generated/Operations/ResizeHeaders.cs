// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure;
using Azure.Core;

namespace Azure.Storage.Blobs
{
    internal class ResizeHeaders
    {
        private readonly Azure.Response _response;
        public ResizeHeaders(Azure.Response response)
        {
            _response = response;
        }
        public string? ETag => _response.Headers.TryGetValue("ETag", out string? value) ? value : null;
        public System.DateTimeOffset? LastModified => _response.Headers.TryGetValue("Last-Modified", out System.DateTimeOffset? value) ? value : null;
        public long? XMsBlobSequenceNumber => _response.Headers.TryGetValue("x-ms-blob-sequence-number", out long? value) ? value : null;
        public string? XMsClientRequestId => _response.Headers.TryGetValue("x-ms-client-request-id", out string? value) ? value : null;
        public string? XMsRequestId => _response.Headers.TryGetValue("x-ms-request-id", out string? value) ? value : null;
        public string? XMsVersion => _response.Headers.TryGetValue("x-ms-version", out string? value) ? value : null;
        public System.DateTimeOffset? Date => _response.Headers.TryGetValue("Date", out System.DateTimeOffset? value) ? value : null;
    }
}
