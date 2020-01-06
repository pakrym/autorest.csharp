// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure;
using Azure.Core;
using Azure.Storage.Blobs.Models.V20190202;

namespace Azure.Storage.Blobs
{
    internal class DownloadHeaders
    {
        private readonly Azure.Response _response;
        public DownloadHeaders(Azure.Response response)
        {
            _response = response;
        }
        public System.DateTimeOffset? LastModified => _response.Headers.TryGetValue("Last-Modified", out System.DateTimeOffset? value) ? value : null;
        public string? XMsMeta => _response.Headers.TryGetValue("x-ms-meta", out string? value) ? value : null;
        public long? ContentLength => _response.Headers.TryGetValue("Content-Length", out long? value) ? value : null;
        public string? ContentType => _response.Headers.TryGetValue("Content-Type", out string? value) ? value : null;
        public string? ContentRange => _response.Headers.TryGetValue("Content-Range", out string? value) ? value : null;
        public string? ETag => _response.Headers.TryGetValue("ETag", out string? value) ? value : null;
        public System.Byte[]? ContentMD5 => _response.Headers.TryGetValue("Content-MD5", out System.Byte[]? value) ? value : null;
        public string? ContentEncoding => _response.Headers.TryGetValue("Content-Encoding", out string? value) ? value : null;
        public string? CacheControl => _response.Headers.TryGetValue("Cache-Control", out string? value) ? value : null;
        public string? ContentDisposition => _response.Headers.TryGetValue("Content-Disposition", out string? value) ? value : null;
        public string? ContentLanguage => _response.Headers.TryGetValue("Content-Language", out string? value) ? value : null;
        public long? XMsBlobSequenceNumber => _response.Headers.TryGetValue("x-ms-blob-sequence-number", out long? value) ? value : null;
        public Azure.Storage.Blobs.Models.V20190202.BlobType? XMsBlobType => _response.Headers.TryGetValue("x-ms-blob-type", out Azure.Storage.Blobs.Models.V20190202.BlobType? value) ? value : null;
        public System.DateTimeOffset? XMsCopyCompletionTime => _response.Headers.TryGetValue("x-ms-copy-completion-time", out System.DateTimeOffset? value) ? value : null;
        public string? XMsCopyStatusDescription => _response.Headers.TryGetValue("x-ms-copy-status-description", out string? value) ? value : null;
        public string? XMsCopyId => _response.Headers.TryGetValue("x-ms-copy-id", out string? value) ? value : null;
        public string? XMsCopyProgress => _response.Headers.TryGetValue("x-ms-copy-progress", out string? value) ? value : null;
        public System.Uri? XMsCopySource => _response.Headers.TryGetValue("x-ms-copy-source", out System.Uri? value) ? value : null;
        public Azure.Storage.Blobs.Models.V20190202.CopyStatus? XMsCopyStatus => _response.Headers.TryGetValue("x-ms-copy-status", out Azure.Storage.Blobs.Models.V20190202.CopyStatus? value) ? value : null;
        public Azure.Storage.Blobs.Models.V20190202.LeaseDurationType? XMsLeaseDuration => _response.Headers.TryGetValue("x-ms-lease-duration", out Azure.Storage.Blobs.Models.V20190202.LeaseDurationType? value) ? value : null;
        public Azure.Storage.Blobs.Models.V20190202.LeaseState? XMsLeaseState => _response.Headers.TryGetValue("x-ms-lease-state", out Azure.Storage.Blobs.Models.V20190202.LeaseState? value) ? value : null;
        public Azure.Storage.Blobs.Models.V20190202.LeaseStatus? XMsLeaseStatus => _response.Headers.TryGetValue("x-ms-lease-status", out Azure.Storage.Blobs.Models.V20190202.LeaseStatus? value) ? value : null;
        public string? XMsClientRequestId => _response.Headers.TryGetValue("x-ms-client-request-id", out string? value) ? value : null;
        public string? XMsRequestId => _response.Headers.TryGetValue("x-ms-request-id", out string? value) ? value : null;
        public string? XMsVersion => _response.Headers.TryGetValue("x-ms-version", out string? value) ? value : null;
        public string? AcceptRanges => _response.Headers.TryGetValue("Accept-Ranges", out string? value) ? value : null;
        public System.DateTimeOffset? Date => _response.Headers.TryGetValue("Date", out System.DateTimeOffset? value) ? value : null;
        public int? XMsBlobCommittedBlockCount => _response.Headers.TryGetValue("x-ms-blob-committed-block-count", out int? value) ? value : null;
        public bool? XMsServerEncrypted => _response.Headers.TryGetValue("x-ms-server-encrypted", out bool? value) ? value : null;
        public string? XMsEncryptionKeySha256 => _response.Headers.TryGetValue("x-ms-encryption-key-sha256", out string? value) ? value : null;
        public System.Byte[]? XMsBlobContentMd5 => _response.Headers.TryGetValue("x-ms-blob-content-md5", out System.Byte[]? value) ? value : null;
    }
}
