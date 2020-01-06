// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure;
using Azure.Core;

namespace Azure.Storage.Blobs
{
    internal class ClearPagesHeaders
    {
        private readonly Azure.Response _response;
        public ClearPagesHeaders(Azure.Response response)
        {
            _response = response;
        }
        public string? ETag => _response.Headers.TryGetValue("ETag", out string? value) ? value : null;
        public System.DateTimeOffset? LastModified => _response.Headers.TryGetValue("Last-Modified", out System.DateTimeOffset? value) ? value : null;
        public System.Byte[]? ContentMD5 => _response.Headers.TryGetValue("Content-MD5", out System.Byte[]? value) ? value : null;
        public System.Byte[]? XMsContentCrc64 => _response.Headers.TryGetValue("x-ms-content-crc64", out System.Byte[]? value) ? value : null;
        public long? XMsBlobSequenceNumber => _response.Headers.TryGetValue("x-ms-blob-sequence-number", out long? value) ? value : null;
        public string? XMsClientRequestId => _response.Headers.TryGetValue("x-ms-client-request-id", out string? value) ? value : null;
        public string? XMsRequestId => _response.Headers.TryGetValue("x-ms-request-id", out string? value) ? value : null;
        public string? XMsVersion => _response.Headers.TryGetValue("x-ms-version", out string? value) ? value : null;
        public System.DateTimeOffset? Date => _response.Headers.TryGetValue("Date", out System.DateTimeOffset? value) ? value : null;
        public bool? XMsRequestServerEncrypted => _response.Headers.TryGetValue("x-ms-request-server-encrypted", out bool? value) ? value : null;
        public string? XMsEncryptionKeySha256 => _response.Headers.TryGetValue("x-ms-encryption-key-sha256", out string? value) ? value : null;
    }
}
