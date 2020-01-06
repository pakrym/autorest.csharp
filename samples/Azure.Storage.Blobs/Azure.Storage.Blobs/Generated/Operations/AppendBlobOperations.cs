// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure.Storage.Blobs
{
    internal partial class AppendBlobOperations
    {
        private string url;
        private string xMsVersion;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of AppendBlobOperations. </summary>
        public AppendBlobOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string url, string xMsVersion)
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
        internal HttpMessage CreateCreateRequest(int? timeout, long contentLength, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, string? xMsBlobCacheControl, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-blob-type", "AppendBlob");
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
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Create Append Blob operation creates a new append blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
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
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<CreateHeaders>> CreateAsync(int? timeout, long contentLength, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, string? xMsBlobCacheControl, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("AppendBlobOperations.Create");
            scope.Start();
            try
            {
                using var message = CreateCreateRequest(timeout, contentLength, xMsBlobContentType, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsBlobContentMd5, xMsBlobCacheControl, xMsMeta, xMsLeaseId, xMsBlobContentDisposition, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new CreateHeaders(message.Response);
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
        /// <summary> The Create Append Blob operation creates a new append blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
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
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<CreateHeaders> Create(int? timeout, long contentLength, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, string? xMsBlobCacheControl, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("AppendBlobOperations.Create");
            scope.Start();
            try
            {
                using var message = CreateCreateRequest(timeout, contentLength, xMsBlobContentType, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsBlobContentMd5, xMsBlobCacheControl, xMsMeta, xMsLeaseId, xMsBlobContentDisposition, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new CreateHeaders(message.Response);
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
        internal HttpMessage CreateAppendBlockRequest(int? timeout, long contentLength, byte[]? contentMD5, byte[]? xMsContentCrc64, string? xMsLeaseId, long? xMsBlobConditionMaxsize, long? xMsBlobConditionAppendpos, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
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
            if (xMsBlobConditionMaxsize != null)
            {
                request.Headers.Add("x-ms-blob-condition-maxsize", xMsBlobConditionMaxsize.Value);
            }
            if (xMsBlobConditionAppendpos != null)
            {
                request.Headers.Add("x-ms-blob-condition-appendpos", xMsBlobConditionAppendpos.Value);
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
            request.Uri.AppendQuery("comp", "appendblock", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Append Block operation commits a new block of data to the end of an existing append blob. The Append Block operation is permitted only if the blob was created with x-ms-blob-type set to AppendBlob. Append Block is supported only on version 2015-02-21 version or later. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsContentCrc64"> Specify the transactional crc64 for the body, to be validated by the service. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobConditionMaxsize"> Optional conditional header. The max length in bytes permitted for the append blob. If the Append Block operation would cause the blob to exceed that limit or if the blob size is already greater than the value specified in this header, the request will fail with MaxBlobSizeConditionNotMet error (HTTP status code 412 - Precondition Failed). </param>
        /// <param name="xMsBlobConditionAppendpos"> Optional conditional header, used only for the Append Block operation. A number indicating the byte offset to compare. Append Block will succeed only if the append position is equal to this number. If it is not, the request will fail with the AppendPositionConditionNotMet error (HTTP status code 412 - Precondition Failed). </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<AppendBlockHeaders>> AppendBlockAsync(int? timeout, long contentLength, byte[]? contentMD5, byte[]? xMsContentCrc64, string? xMsLeaseId, long? xMsBlobConditionMaxsize, long? xMsBlobConditionAppendpos, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("AppendBlobOperations.AppendBlock");
            scope.Start();
            try
            {
                using var message = CreateAppendBlockRequest(timeout, contentLength, contentMD5, xMsContentCrc64, xMsLeaseId, xMsBlobConditionMaxsize, xMsBlobConditionAppendpos, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new AppendBlockHeaders(message.Response);
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
        /// <summary> The Append Block operation commits a new block of data to the end of an existing append blob. The Append Block operation is permitted only if the blob was created with x-ms-blob-type set to AppendBlob. Append Block is supported only on version 2015-02-21 version or later. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsContentCrc64"> Specify the transactional crc64 for the body, to be validated by the service. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobConditionMaxsize"> Optional conditional header. The max length in bytes permitted for the append blob. If the Append Block operation would cause the blob to exceed that limit or if the blob size is already greater than the value specified in this header, the request will fail with MaxBlobSizeConditionNotMet error (HTTP status code 412 - Precondition Failed). </param>
        /// <param name="xMsBlobConditionAppendpos"> Optional conditional header, used only for the Append Block operation. A number indicating the byte offset to compare. Append Block will succeed only if the append position is equal to this number. If it is not, the request will fail with the AppendPositionConditionNotMet error (HTTP status code 412 - Precondition Failed). </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<AppendBlockHeaders> AppendBlock(int? timeout, long contentLength, byte[]? contentMD5, byte[]? xMsContentCrc64, string? xMsLeaseId, long? xMsBlobConditionMaxsize, long? xMsBlobConditionAppendpos, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("AppendBlobOperations.AppendBlock");
            scope.Start();
            try
            {
                using var message = CreateAppendBlockRequest(timeout, contentLength, contentMD5, xMsContentCrc64, xMsLeaseId, xMsBlobConditionMaxsize, xMsBlobConditionAppendpos, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new AppendBlockHeaders(message.Response);
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
        internal HttpMessage CreateAppendBlockFromUriRequest(Uri xMsCopySource, string? xMsSourceRange, byte[]? xMsSourceContentMd5, byte[]? xMsSourceContentCrc64, int? timeout, long contentLength, byte[]? contentMD5, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsLeaseId, long? xMsBlobConditionMaxsize, long? xMsBlobConditionAppendpos, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
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
            request.Headers.Add("Content-Length", contentLength);
            if (contentMD5 != null)
            {
                request.Headers.Add("Content-MD5", contentMD5);
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
            if (xMsBlobConditionMaxsize != null)
            {
                request.Headers.Add("x-ms-blob-condition-maxsize", xMsBlobConditionMaxsize.Value);
            }
            if (xMsBlobConditionAppendpos != null)
            {
                request.Headers.Add("x-ms-blob-condition-appendpos", xMsBlobConditionAppendpos.Value);
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
            request.Uri.AppendQuery("comp", "appendblock", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Append Block operation commits a new block of data to the end of an existing append blob where the contents are read from a source url. The Append Block operation is permitted only if the blob was created with x-ms-blob-type set to AppendBlob. Append Block is supported only on version 2015-02-21 version or later. </summary>
        /// <param name="xMsCopySource"> Specify a URL to the copy source. </param>
        /// <param name="xMsSourceRange"> Bytes of source data in the specified range. </param>
        /// <param name="xMsSourceContentMd5"> Specify the md5 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="xMsSourceContentCrc64"> Specify the crc64 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobConditionMaxsize"> Optional conditional header. The max length in bytes permitted for the append blob. If the Append Block operation would cause the blob to exceed that limit or if the blob size is already greater than the value specified in this header, the request will fail with MaxBlobSizeConditionNotMet error (HTTP status code 412 - Precondition Failed). </param>
        /// <param name="xMsBlobConditionAppendpos"> Optional conditional header, used only for the Append Block operation. A number indicating the byte offset to compare. Append Block will succeed only if the append position is equal to this number. If it is not, the request will fail with the AppendPositionConditionNotMet error (HTTP status code 412 - Precondition Failed). </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsSourceIfModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="xMsSourceIfNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<AppendBlockFromUriHeaders>> AppendBlockFromUriAsync(Uri xMsCopySource, string? xMsSourceRange, byte[]? xMsSourceContentMd5, byte[]? xMsSourceContentCrc64, int? timeout, long contentLength, byte[]? contentMD5, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsLeaseId, long? xMsBlobConditionMaxsize, long? xMsBlobConditionAppendpos, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("AppendBlobOperations.AppendBlockFromUri");
            scope.Start();
            try
            {
                using var message = CreateAppendBlockFromUriRequest(xMsCopySource, xMsSourceRange, xMsSourceContentMd5, xMsSourceContentCrc64, timeout, contentLength, contentMD5, xMsEncryptionKey, xMsEncryptionKeySha256, xMsLeaseId, xMsBlobConditionMaxsize, xMsBlobConditionAppendpos, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new AppendBlockFromUriHeaders(message.Response);
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
        /// <summary> The Append Block operation commits a new block of data to the end of an existing append blob where the contents are read from a source url. The Append Block operation is permitted only if the blob was created with x-ms-blob-type set to AppendBlob. Append Block is supported only on version 2015-02-21 version or later. </summary>
        /// <param name="xMsCopySource"> Specify a URL to the copy source. </param>
        /// <param name="xMsSourceRange"> Bytes of source data in the specified range. </param>
        /// <param name="xMsSourceContentMd5"> Specify the md5 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="xMsSourceContentCrc64"> Specify the crc64 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobConditionMaxsize"> Optional conditional header. The max length in bytes permitted for the append blob. If the Append Block operation would cause the blob to exceed that limit or if the blob size is already greater than the value specified in this header, the request will fail with MaxBlobSizeConditionNotMet error (HTTP status code 412 - Precondition Failed). </param>
        /// <param name="xMsBlobConditionAppendpos"> Optional conditional header, used only for the Append Block operation. A number indicating the byte offset to compare. Append Block will succeed only if the append position is equal to this number. If it is not, the request will fail with the AppendPositionConditionNotMet error (HTTP status code 412 - Precondition Failed). </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsSourceIfModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="xMsSourceIfNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<AppendBlockFromUriHeaders> AppendBlockFromUri(Uri xMsCopySource, string? xMsSourceRange, byte[]? xMsSourceContentMd5, byte[]? xMsSourceContentCrc64, int? timeout, long contentLength, byte[]? contentMD5, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsLeaseId, long? xMsBlobConditionMaxsize, long? xMsBlobConditionAppendpos, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("AppendBlobOperations.AppendBlockFromUri");
            scope.Start();
            try
            {
                using var message = CreateAppendBlockFromUriRequest(xMsCopySource, xMsSourceRange, xMsSourceContentMd5, xMsSourceContentCrc64, timeout, contentLength, contentMD5, xMsEncryptionKey, xMsEncryptionKeySha256, xMsLeaseId, xMsBlobConditionMaxsize, xMsBlobConditionAppendpos, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new AppendBlockFromUriHeaders(message.Response);
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
    }
}
