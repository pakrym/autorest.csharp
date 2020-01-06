// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Storage.Blobs.Models.V20190202;

namespace Azure.Storage.Blobs
{
    internal partial class BlockBlobOperations
    {
        private string url;
        private string xMsVersion;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of BlockBlobOperations. </summary>
        public BlockBlobOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string url, string xMsVersion)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }
            if (xMsVersion == null)
            {
                throw new ArgumentNullException(nameof(xMsVersion));
            }

            this.url = url;
            this.xMsVersion = xMsVersion;
            this.clientDiagnostics = clientDiagnostics;
            this.pipeline = pipeline;
        }
        internal HttpMessage CreateUploadRequest(int? timeout, byte[]? contentMD5, long contentLength, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, string? xMsBlobCacheControl, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, AccessTier? xMsAccessTier, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-blob-type", "BlockBlob");
            if (contentMD5 != null)
            {
                request.Headers.Add("Content-MD5", contentMD5);
            }
            request.Headers.Add("Content-Length", contentLength);
            if (xMsBlobContentType != null)
            {
                request.Headers.Add("x-ms-blob-content-type", xMsBlobContentType);
            }
            if (xMsBlobContentEncoding != null)
            {
                request.Headers.Add("x-ms-blob-content-encoding", xMsBlobContentEncoding);
            }
            if (xMsBlobContentLanguage != null)
            {
                request.Headers.Add("x-ms-blob-content-language", xMsBlobContentLanguage);
            }
            if (xMsBlobContentMd5 != null)
            {
                request.Headers.Add("x-ms-blob-content-md5", xMsBlobContentMd5);
            }
            if (xMsBlobCacheControl != null)
            {
                request.Headers.Add("x-ms-blob-cache-control", xMsBlobCacheControl);
            }
            if (xMsMeta != null)
            {
                request.Headers.Add("x-ms-meta", xMsMeta);
            }
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            if (xMsBlobContentDisposition != null)
            {
                request.Headers.Add("x-ms-blob-content-disposition", xMsBlobContentDisposition);
            }
            if (xMsEncryptionKey != null)
            {
                request.Headers.Add("x-ms-encryption-key", xMsEncryptionKey);
            }
            if (xMsEncryptionKeySha256 != null)
            {
                request.Headers.Add("x-ms-encryption-key-sha256", xMsEncryptionKeySha256);
            }
            request.Headers.Add("x-ms-encryption-algorithm", "AES256");
            if (xMsAccessTier != null)
            {
                request.Headers.Add("x-ms-access-tier", xMsAccessTier.Value);
            }
            if (ifModifiedSince != null)
            {
                request.Headers.Add("If-Modified-Since", ifModifiedSince.Value, "R");
            }
            if (ifUnmodifiedSince != null)
            {
                request.Headers.Add("If-Unmodified-Since", ifUnmodifiedSince.Value, "R");
            }
            if (ifMatch != null)
            {
                request.Headers.Add("If-Match", ifMatch);
            }
            if (ifNoneMatch != null)
            {
                request.Headers.Add("If-None-Match", ifNoneMatch);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Headers.Add("Content-Type", "application/octet-stream");
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Upload Block Blob operation updates the content of an existing block blob. Updating an existing block blob overwrites any existing metadata on the blob. Partial updates are not supported with Put Blob; the content of the existing blob is overwritten with the content of the new blob. To perform a partial update of the content of a block blob, use the Put Block List operation. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="xMsBlobContentType"> Optional. Sets the blob&apos;s content type. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentEncoding"> Optional. Sets the blob&apos;s content encoding. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentLanguage"> Optional. Set the blob&apos;s content language. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentMd5"> Optional. An MD5 hash of the blob content. Note that this hash is not validated, as the hashes for the individual blocks were validated when each was uploaded. </param>
        /// <param name="xMsBlobCacheControl"> Optional. Sets the blob&apos;s cache control. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobContentDisposition"> Optional. Sets the blob&apos;s Content-Disposition header. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the blob. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<UploadHeaders>> UploadAsync(int? timeout, byte[]? contentMD5, long contentLength, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, string? xMsBlobCacheControl, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, AccessTier? xMsAccessTier, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.Upload");
            scope.Start();
            try
            {
                using var message = CreateUploadRequest(timeout, contentMD5, contentLength, xMsBlobContentType, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsBlobContentMd5, xMsBlobCacheControl, xMsMeta, xMsLeaseId, xMsBlobContentDisposition, xMsEncryptionKey, xMsEncryptionKeySha256, xMsAccessTier, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new UploadHeaders(message.Response);
                        return ResponseWithHeaders.FromValue(headers, message.Response);
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> The Upload Block Blob operation updates the content of an existing block blob. Updating an existing block blob overwrites any existing metadata on the blob. Partial updates are not supported with Put Blob; the content of the existing blob is overwritten with the content of the new blob. To perform a partial update of the content of a block blob, use the Put Block List operation. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="xMsBlobContentType"> Optional. Sets the blob&apos;s content type. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentEncoding"> Optional. Sets the blob&apos;s content encoding. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentLanguage"> Optional. Set the blob&apos;s content language. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentMd5"> Optional. An MD5 hash of the blob content. Note that this hash is not validated, as the hashes for the individual blocks were validated when each was uploaded. </param>
        /// <param name="xMsBlobCacheControl"> Optional. Sets the blob&apos;s cache control. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobContentDisposition"> Optional. Sets the blob&apos;s Content-Disposition header. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the blob. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<UploadHeaders> Upload(int? timeout, byte[]? contentMD5, long contentLength, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, string? xMsBlobCacheControl, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, AccessTier? xMsAccessTier, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.Upload");
            scope.Start();
            try
            {
                using var message = CreateUploadRequest(timeout, contentMD5, contentLength, xMsBlobContentType, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsBlobContentMd5, xMsBlobCacheControl, xMsMeta, xMsLeaseId, xMsBlobContentDisposition, xMsEncryptionKey, xMsEncryptionKeySha256, xMsAccessTier, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new UploadHeaders(message.Response);
                        return ResponseWithHeaders.FromValue(headers, message.Response);
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateStageBlockRequest(string blockid, long contentLength, byte[]? contentMD5, byte[]? xMsContentCrc64, int? timeout, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("Content-Length", contentLength);
            if (contentMD5 != null)
            {
                request.Headers.Add("Content-MD5", contentMD5);
            }
            if (xMsContentCrc64 != null)
            {
                request.Headers.Add("x-ms-content-crc64", xMsContentCrc64);
            }
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            if (xMsEncryptionKey != null)
            {
                request.Headers.Add("x-ms-encryption-key", xMsEncryptionKey);
            }
            if (xMsEncryptionKeySha256 != null)
            {
                request.Headers.Add("x-ms-encryption-key-sha256", xMsEncryptionKeySha256);
            }
            request.Headers.Add("x-ms-encryption-algorithm", "AES256");
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Headers.Add("Content-Type", "application/octet-stream");
            request.Uri.AppendQuery("comp", "block", true);
            request.Uri.AppendQuery("blockid", blockid, true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Stage Block operation creates a new block to be committed as part of a blob. </summary>
        /// <param name="blockid"> A valid Base64 string value that identifies the block. Prior to encoding, the string must be less than or equal to 64 bytes in size. For a given blob, the length of the value specified for the blockid parameter must be the same size for each block. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsContentCrc64"> Specify the transactional crc64 for the body, to be validated by the service. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<StageBlockHeaders>> StageBlockAsync(string blockid, long contentLength, byte[]? contentMD5, byte[]? xMsContentCrc64, int? timeout, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (blockid == null)
            {
                throw new ArgumentNullException(nameof(blockid));
            }

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.StageBlock");
            scope.Start();
            try
            {
                using var message = CreateStageBlockRequest(blockid, contentLength, contentMD5, xMsContentCrc64, timeout, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new StageBlockHeaders(message.Response);
                        return ResponseWithHeaders.FromValue(headers, message.Response);
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> The Stage Block operation creates a new block to be committed as part of a blob. </summary>
        /// <param name="blockid"> A valid Base64 string value that identifies the block. Prior to encoding, the string must be less than or equal to 64 bytes in size. For a given blob, the length of the value specified for the blockid parameter must be the same size for each block. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsContentCrc64"> Specify the transactional crc64 for the body, to be validated by the service. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<StageBlockHeaders> StageBlock(string blockid, long contentLength, byte[]? contentMD5, byte[]? xMsContentCrc64, int? timeout, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (blockid == null)
            {
                throw new ArgumentNullException(nameof(blockid));
            }

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.StageBlock");
            scope.Start();
            try
            {
                using var message = CreateStageBlockRequest(blockid, contentLength, contentMD5, xMsContentCrc64, timeout, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new StageBlockHeaders(message.Response);
                        return ResponseWithHeaders.FromValue(headers, message.Response);
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateStageBlockFromUriRequest(string blockid, long contentLength, Uri xMsCopySource, string? xMsSourceRange, byte[]? xMsSourceContentMd5, byte[]? xMsSourceContentCrc64, int? timeout, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsLeaseId, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("Content-Length", contentLength);
            request.Headers.Add("x-ms-copy-source", xMsCopySource);
            if (xMsSourceRange != null)
            {
                request.Headers.Add("x-ms-source-range", xMsSourceRange);
            }
            if (xMsSourceContentMd5 != null)
            {
                request.Headers.Add("x-ms-source-content-md5", xMsSourceContentMd5);
            }
            if (xMsSourceContentCrc64 != null)
            {
                request.Headers.Add("x-ms-source-content-crc64", xMsSourceContentCrc64);
            }
            if (xMsEncryptionKey != null)
            {
                request.Headers.Add("x-ms-encryption-key", xMsEncryptionKey);
            }
            if (xMsEncryptionKeySha256 != null)
            {
                request.Headers.Add("x-ms-encryption-key-sha256", xMsEncryptionKeySha256);
            }
            request.Headers.Add("x-ms-encryption-algorithm", "AES256");
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            if (xMsSourceIfModifiedSince != null)
            {
                request.Headers.Add("x-ms-source-if-modified-since", xMsSourceIfModifiedSince.Value, "R");
            }
            if (xMsSourceIfUnmodifiedSince != null)
            {
                request.Headers.Add("x-ms-source-if-unmodified-since", xMsSourceIfUnmodifiedSince.Value, "R");
            }
            if (xMsSourceIfMatch != null)
            {
                request.Headers.Add("x-ms-source-if-match", xMsSourceIfMatch);
            }
            if (xMsSourceIfNoneMatch != null)
            {
                request.Headers.Add("x-ms-source-if-none-match", xMsSourceIfNoneMatch);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "block", true);
            request.Uri.AppendQuery("blockid", blockid, true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Stage Block operation creates a new block to be committed as part of a blob where the contents are read from a URL. </summary>
        /// <param name="blockid"> A valid Base64 string value that identifies the block. Prior to encoding, the string must be less than or equal to 64 bytes in size. For a given blob, the length of the value specified for the blockid parameter must be the same size for each block. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="xMsCopySource"> Specify a URL to the copy source. </param>
        /// <param name="xMsSourceRange"> Bytes of source data in the specified range. </param>
        /// <param name="xMsSourceContentMd5"> Specify the md5 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="xMsSourceContentCrc64"> Specify the crc64 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsSourceIfModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="xMsSourceIfNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<StageBlockFromUriHeaders>> StageBlockFromUriAsync(string blockid, long contentLength, Uri xMsCopySource, string? xMsSourceRange, byte[]? xMsSourceContentMd5, byte[]? xMsSourceContentCrc64, int? timeout, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsLeaseId, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (blockid == null)
            {
                throw new ArgumentNullException(nameof(blockid));
            }
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.StageBlockFromUri");
            scope.Start();
            try
            {
                using var message = CreateStageBlockFromUriRequest(blockid, contentLength, xMsCopySource, xMsSourceRange, xMsSourceContentMd5, xMsSourceContentCrc64, timeout, xMsEncryptionKey, xMsEncryptionKeySha256, xMsLeaseId, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new StageBlockFromUriHeaders(message.Response);
                        return ResponseWithHeaders.FromValue(headers, message.Response);
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> The Stage Block operation creates a new block to be committed as part of a blob where the contents are read from a URL. </summary>
        /// <param name="blockid"> A valid Base64 string value that identifies the block. Prior to encoding, the string must be less than or equal to 64 bytes in size. For a given blob, the length of the value specified for the blockid parameter must be the same size for each block. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="xMsCopySource"> Specify a URL to the copy source. </param>
        /// <param name="xMsSourceRange"> Bytes of source data in the specified range. </param>
        /// <param name="xMsSourceContentMd5"> Specify the md5 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="xMsSourceContentCrc64"> Specify the crc64 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsSourceIfModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="xMsSourceIfNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<StageBlockFromUriHeaders> StageBlockFromUri(string blockid, long contentLength, Uri xMsCopySource, string? xMsSourceRange, byte[]? xMsSourceContentMd5, byte[]? xMsSourceContentCrc64, int? timeout, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsLeaseId, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (blockid == null)
            {
                throw new ArgumentNullException(nameof(blockid));
            }
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.StageBlockFromUri");
            scope.Start();
            try
            {
                using var message = CreateStageBlockFromUriRequest(blockid, contentLength, xMsCopySource, xMsSourceRange, xMsSourceContentMd5, xMsSourceContentCrc64, timeout, xMsEncryptionKey, xMsEncryptionKeySha256, xMsLeaseId, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new StageBlockFromUriHeaders(message.Response);
                        return ResponseWithHeaders.FromValue(headers, message.Response);
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateCommitBlockListRequest(int? timeout, string? xMsBlobCacheControl, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, byte[]? contentMD5, byte[]? xMsContentCrc64, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, AccessTier? xMsAccessTier, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, BlockLookupList blocks)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            if (xMsBlobCacheControl != null)
            {
                request.Headers.Add("x-ms-blob-cache-control", xMsBlobCacheControl);
            }
            if (xMsBlobContentType != null)
            {
                request.Headers.Add("x-ms-blob-content-type", xMsBlobContentType);
            }
            if (xMsBlobContentEncoding != null)
            {
                request.Headers.Add("x-ms-blob-content-encoding", xMsBlobContentEncoding);
            }
            if (xMsBlobContentLanguage != null)
            {
                request.Headers.Add("x-ms-blob-content-language", xMsBlobContentLanguage);
            }
            if (xMsBlobContentMd5 != null)
            {
                request.Headers.Add("x-ms-blob-content-md5", xMsBlobContentMd5);
            }
            if (contentMD5 != null)
            {
                request.Headers.Add("Content-MD5", contentMD5);
            }
            if (xMsContentCrc64 != null)
            {
                request.Headers.Add("x-ms-content-crc64", xMsContentCrc64);
            }
            if (xMsMeta != null)
            {
                request.Headers.Add("x-ms-meta", xMsMeta);
            }
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            if (xMsBlobContentDisposition != null)
            {
                request.Headers.Add("x-ms-blob-content-disposition", xMsBlobContentDisposition);
            }
            if (xMsEncryptionKey != null)
            {
                request.Headers.Add("x-ms-encryption-key", xMsEncryptionKey);
            }
            if (xMsEncryptionKeySha256 != null)
            {
                request.Headers.Add("x-ms-encryption-key-sha256", xMsEncryptionKeySha256);
            }
            request.Headers.Add("x-ms-encryption-algorithm", "AES256");
            if (xMsAccessTier != null)
            {
                request.Headers.Add("x-ms-access-tier", xMsAccessTier.Value);
            }
            if (ifModifiedSince != null)
            {
                request.Headers.Add("If-Modified-Since", ifModifiedSince.Value, "R");
            }
            if (ifUnmodifiedSince != null)
            {
                request.Headers.Add("If-Unmodified-Since", ifUnmodifiedSince.Value, "R");
            }
            if (ifMatch != null)
            {
                request.Headers.Add("If-Match", ifMatch);
            }
            if (ifNoneMatch != null)
            {
                request.Headers.Add("If-None-Match", ifNoneMatch);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Headers.Add("Content-Type", "application/xml");
            request.Uri.AppendQuery("comp", "blocklist", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            using var content = new XmlWriterContent();
            content.XmlWriter.WriteObjectValue(blocks, "BlockList");
            request.Content = content;
            return message;
        }
        /// <summary> The Commit Block List operation writes a blob by specifying the list of block IDs that make up the blob. In order to be written as part of a blob, a block must have been successfully written to the server in a prior Put Block operation. You can call Put Block List to update a blob by uploading only those blocks that have changed, then committing the new and existing blocks together. You can do this by specifying whether to commit a block from the committed block list or from the uncommitted block list, or to commit the most recently uploaded version of the block, whichever list it may belong to. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsBlobCacheControl"> Optional. Sets the blob&apos;s cache control. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentType"> Optional. Sets the blob&apos;s content type. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentEncoding"> Optional. Sets the blob&apos;s content encoding. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentLanguage"> Optional. Set the blob&apos;s content language. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentMd5"> Optional. An MD5 hash of the blob content. Note that this hash is not validated, as the hashes for the individual blocks were validated when each was uploaded. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsContentCrc64"> Specify the transactional crc64 for the body, to be validated by the service. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobContentDisposition"> Optional. Sets the blob&apos;s Content-Disposition header. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the blob. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="blocks"> The BlockLookupList to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<CommitBlockListHeaders>> CommitBlockListAsync(int? timeout, string? xMsBlobCacheControl, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, byte[]? contentMD5, byte[]? xMsContentCrc64, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, AccessTier? xMsAccessTier, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, BlockLookupList blocks, CancellationToken cancellationToken = default)
        {
            if (blocks == null)
            {
                throw new ArgumentNullException(nameof(blocks));
            }

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.CommitBlockList");
            scope.Start();
            try
            {
                using var message = CreateCommitBlockListRequest(timeout, xMsBlobCacheControl, xMsBlobContentType, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsBlobContentMd5, contentMD5, xMsContentCrc64, xMsMeta, xMsLeaseId, xMsBlobContentDisposition, xMsEncryptionKey, xMsEncryptionKeySha256, xMsAccessTier, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId, blocks);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new CommitBlockListHeaders(message.Response);
                        return ResponseWithHeaders.FromValue(headers, message.Response);
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> The Commit Block List operation writes a blob by specifying the list of block IDs that make up the blob. In order to be written as part of a blob, a block must have been successfully written to the server in a prior Put Block operation. You can call Put Block List to update a blob by uploading only those blocks that have changed, then committing the new and existing blocks together. You can do this by specifying whether to commit a block from the committed block list or from the uncommitted block list, or to commit the most recently uploaded version of the block, whichever list it may belong to. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsBlobCacheControl"> Optional. Sets the blob&apos;s cache control. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentType"> Optional. Sets the blob&apos;s content type. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentEncoding"> Optional. Sets the blob&apos;s content encoding. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentLanguage"> Optional. Set the blob&apos;s content language. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentMd5"> Optional. An MD5 hash of the blob content. Note that this hash is not validated, as the hashes for the individual blocks were validated when each was uploaded. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsContentCrc64"> Specify the transactional crc64 for the body, to be validated by the service. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobContentDisposition"> Optional. Sets the blob&apos;s Content-Disposition header. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the blob. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="blocks"> The BlockLookupList to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<CommitBlockListHeaders> CommitBlockList(int? timeout, string? xMsBlobCacheControl, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, byte[]? contentMD5, byte[]? xMsContentCrc64, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, AccessTier? xMsAccessTier, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, BlockLookupList blocks, CancellationToken cancellationToken = default)
        {
            if (blocks == null)
            {
                throw new ArgumentNullException(nameof(blocks));
            }

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.CommitBlockList");
            scope.Start();
            try
            {
                using var message = CreateCommitBlockListRequest(timeout, xMsBlobCacheControl, xMsBlobContentType, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsBlobContentMd5, contentMD5, xMsContentCrc64, xMsMeta, xMsLeaseId, xMsBlobContentDisposition, xMsEncryptionKey, xMsEncryptionKeySha256, xMsAccessTier, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId, blocks);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new CommitBlockListHeaders(message.Response);
                        return ResponseWithHeaders.FromValue(headers, message.Response);
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        internal HttpMessage CreateGetBlockListRequest(string? snapshot, BlockListType blocklisttype, int? timeout, string? xMsLeaseId, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "blocklist", true);
            if (snapshot != null)
            {
                request.Uri.AppendQuery("snapshot", snapshot, true);
            }
            request.Uri.AppendQuery("blocklisttype", blocklisttype, true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Get Block List operation retrieves the list of blocks that have been uploaded as part of a block blob. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="blocklisttype"> Specifies whether to return the list of committed blocks, the list of uncommitted blocks, or both lists together. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<BlockList, GetBlockListHeaders>> GetBlockListAsync(string? snapshot, BlockListType blocklisttype, int? timeout, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.GetBlockList");
            scope.Start();
            try
            {
                using var message = CreateGetBlockListRequest(snapshot, blocklisttype, timeout, xMsLeaseId, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlockList value = default;
                            var blockList = document.Element("BlockList");
                            if (blockList != null)
                            {
                                value = BlockList.DeserializeBlockList(blockList);
                            }
                            var headers = new GetBlockListHeaders(message.Response);
                            return ResponseWithHeaders.FromValue(value, headers, message.Response);
                        }
                    default:
                        throw await message.Response.CreateRequestFailedExceptionAsync().ConfigureAwait(false);
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
        /// <summary> The Get Block List operation retrieves the list of blocks that have been uploaded as part of a block blob. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="blocklisttype"> Specifies whether to return the list of committed blocks, the list of uncommitted blocks, or both lists together. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<BlockList, GetBlockListHeaders> GetBlockList(string? snapshot, BlockListType blocklisttype, int? timeout, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlockBlobOperations.GetBlockList");
            scope.Start();
            try
            {
                using var message = CreateGetBlockListRequest(snapshot, blocklisttype, timeout, xMsLeaseId, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlockList value = default;
                            var blockList = document.Element("BlockList");
                            if (blockList != null)
                            {
                                value = BlockList.DeserializeBlockList(blockList);
                            }
                            var headers = new GetBlockListHeaders(message.Response);
                            return ResponseWithHeaders.FromValue(value, headers, message.Response);
                        }
                    default:
                        throw message.Response.CreateRequestFailedException();
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
    }
}
