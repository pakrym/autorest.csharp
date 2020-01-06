// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Azure;
using Azure.Core;
using Azure.Storage.Blobs.Models.V20190202;

namespace Azure.Storage.Blobs
{
    internal class GetPropertiesHeaders
    {
        private readonly Response _response;
        public GetPropertiesHeaders(Response response)
        {
            _response = response;
        }
        public DateTimeOffset? LastModified => _response.Headers.TryGetValue("Last-Modified", out DateTimeOffset? value) ? value : null;
        public DateTimeOffset? XMsCreationTime => _response.Headers.TryGetValue("x-ms-creation-time", out DateTimeOffset? value) ? value : null;
        public string? XMsMeta => _response.Headers.TryGetValue("x-ms-meta", out string? value) ? value : null;
        public BlobType? XMsBlobType => _response.Headers.TryGetValue("x-ms-blob-type", out BlobType? value) ? value : null;
        public DateTimeOffset? XMsCopyCompletionTime => _response.Headers.TryGetValue("x-ms-copy-completion-time", out DateTimeOffset? value) ? value : null;
        public string? XMsCopyStatusDescription => _response.Headers.TryGetValue("x-ms-copy-status-description", out string? value) ? value : null;
        public string? XMsCopyId => _response.Headers.TryGetValue("x-ms-copy-id", out string? value) ? value : null;
        public string? XMsCopyProgress => _response.Headers.TryGetValue("x-ms-copy-progress", out string? value) ? value : null;
        public Uri? XMsCopySource => _response.Headers.TryGetValue("x-ms-copy-source", out Uri? value) ? value : null;
        public CopyStatus? XMsCopyStatus => _response.Headers.TryGetValue("x-ms-copy-status", out CopyStatus? value) ? value : null;
        public bool? XMsIncrementalCopy => _response.Headers.TryGetValue("x-ms-incremental-copy", out bool? value) ? value : null;
        public string? XMsCopyDestinationSnapshot => _response.Headers.TryGetValue("x-ms-copy-destination-snapshot", out string? value) ? value : null;
        public LeaseDurationType? XMsLeaseDuration => _response.Headers.TryGetValue("x-ms-lease-duration", out LeaseDurationType? value) ? value : null;
        public LeaseState? XMsLeaseState => _response.Headers.TryGetValue("x-ms-lease-state", out LeaseState? value) ? value : null;
        public LeaseStatus? XMsLeaseStatus => _response.Headers.TryGetValue("x-ms-lease-status", out LeaseStatus? value) ? value : null;
        public long? ContentLength => _response.Headers.TryGetValue("Content-Length", out long? value) ? value : null;
        public string? ContentType => _response.Headers.TryGetValue("Content-Type", out string? value) ? value : null;
        public string? ETag => _response.Headers.TryGetValue("ETag", out string? value) ? value : null;
        public byte[]? ContentMD5 => _response.Headers.TryGetValue("Content-MD5", out byte[]? value) ? value : null;
        public string? ContentEncoding => _response.Headers.TryGetValue("Content-Encoding", out string? value) ? value : null;
        public string? ContentDisposition => _response.Headers.TryGetValue("Content-Disposition", out string? value) ? value : null;
        public string? ContentLanguage => _response.Headers.TryGetValue("Content-Language", out string? value) ? value : null;
        public string? CacheControl => _response.Headers.TryGetValue("Cache-Control", out string? value) ? value : null;
        public long? XMsBlobSequenceNumber => _response.Headers.TryGetValue("x-ms-blob-sequence-number", out long? value) ? value : null;
        public string? XMsClientRequestId => _response.Headers.TryGetValue("x-ms-client-request-id", out string? value) ? value : null;
        public string? XMsRequestId => _response.Headers.TryGetValue("x-ms-request-id", out string? value) ? value : null;
        public string? XMsVersion => _response.Headers.TryGetValue("x-ms-version", out string? value) ? value : null;
        public DateTimeOffset? Date => _response.Headers.TryGetValue("Date", out DateTimeOffset? value) ? value : null;
        public string? AcceptRanges => _response.Headers.TryGetValue("Accept-Ranges", out string? value) ? value : null;
        public int? XMsBlobCommittedBlockCount => _response.Headers.TryGetValue("x-ms-blob-committed-block-count", out int? value) ? value : null;
        public bool? XMsServerEncrypted => _response.Headers.TryGetValue("x-ms-server-encrypted", out bool? value) ? value : null;
        public string? XMsEncryptionKeySha256 => _response.Headers.TryGetValue("x-ms-encryption-key-sha256", out string? value) ? value : null;
        public string? XMsAccessTier => _response.Headers.TryGetValue("x-ms-access-tier", out string? value) ? value : null;
        public bool? XMsAccessTierInferred => _response.Headers.TryGetValue("x-ms-access-tier-inferred", out bool? value) ? value : null;
        public string? XMsArchiveStatus => _response.Headers.TryGetValue("x-ms-archive-status", out string? value) ? value : null;
        public DateTimeOffset? XMsAccessTierChangeTime => _response.Headers.TryGetValue("x-ms-access-tier-change-time", out DateTimeOffset? value) ? value : null;
    }
}
