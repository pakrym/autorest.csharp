// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure;
using Azure.Core;
using Azure.Storage.Blobs.Models.V20190202;

namespace Azure.Storage.Blobs
{
    internal class CopyFromUriHeaders
    {
        private readonly Azure.Response _response;
        public CopyFromUriHeaders(Azure.Response response)
        {
            _response = response;
        }
        public string? ETag => _response.Headers.TryGetValue("ETag", out string? value) ? value : null;
        public System.DateTimeOffset? LastModified => _response.Headers.TryGetValue("Last-Modified", out System.DateTimeOffset? value) ? value : null;
        public string? XMsClientRequestId => _response.Headers.TryGetValue("x-ms-client-request-id", out string? value) ? value : null;
        public string? XMsRequestId => _response.Headers.TryGetValue("x-ms-request-id", out string? value) ? value : null;
        public string? XMsVersion => _response.Headers.TryGetValue("x-ms-version", out string? value) ? value : null;
        public System.DateTimeOffset? Date => _response.Headers.TryGetValue("Date", out System.DateTimeOffset? value) ? value : null;
        public string? XMsCopyId => _response.Headers.TryGetValue("x-ms-copy-id", out string? value) ? value : null;
        public Azure.Storage.Blobs.Models.V20190202.CopyStatus? XMsCopyStatus => _response.Headers.TryGetValue("x-ms-copy-status", out Azure.Storage.Blobs.Models.V20190202.CopyStatus? value) ? value : null;
    }
}
