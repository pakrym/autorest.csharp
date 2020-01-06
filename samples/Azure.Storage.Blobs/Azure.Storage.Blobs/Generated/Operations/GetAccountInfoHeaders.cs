// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure;
using Azure.Core;
using Azure.Storage.Blobs.Models.V20190202;

namespace Azure.Storage.Blobs
{
    internal class GetAccountInfoHeaders
    {
        private readonly Azure.Response _response;
        public GetAccountInfoHeaders(Azure.Response response)
        {
            _response = response;
        }
        public string? XMsClientRequestId => _response.Headers.TryGetValue("x-ms-client-request-id", out string? value) ? value : null;
        public string? XMsRequestId => _response.Headers.TryGetValue("x-ms-request-id", out string? value) ? value : null;
        public string? XMsVersion => _response.Headers.TryGetValue("x-ms-version", out string? value) ? value : null;
        public System.DateTimeOffset? Date => _response.Headers.TryGetValue("Date", out System.DateTimeOffset? value) ? value : null;
        public Azure.Storage.Blobs.Models.V20190202.SkuName? XMsSkuName => _response.Headers.TryGetValue("x-ms-sku-name", out Azure.Storage.Blobs.Models.V20190202.SkuName? value) ? value : null;
        public Azure.Storage.Blobs.Models.V20190202.AccountKind? XMsAccountKind => _response.Headers.TryGetValue("x-ms-account-kind", out Azure.Storage.Blobs.Models.V20190202.AccountKind? value) ? value : null;
    }
}
