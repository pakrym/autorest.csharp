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
    internal partial class PageBlobOperations
    {
        private string url;
        private string xMsVersion;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of PageBlobOperations. </summary>
        public PageBlobOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string url, string xMsVersion)
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
        internal HttpMessage CreateCreateRequest(int? timeout, long contentLength, AccessTier? xMsAccessTier, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, string? xMsBlobCacheControl, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, long xMsBlobContentLength, long? xMsBlobSequenceNumber, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-blob-type", "PageBlob");
            request.Headers.Add("Content-Length", contentLength);
            if (xMsAccessTier != null)
            {
                request.Headers.Add("x-ms-access-tier", xMsAccessTier.Value);
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
            request.Headers.Add("x-ms-blob-content-length", xMsBlobContentLength);
            if (xMsBlobSequenceNumber != null)
            {
                request.Headers.Add("x-ms-blob-sequence-number", xMsBlobSequenceNumber.Value);
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
        /// <summary> The Create operation creates a new page blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the page blob. </param>
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
        /// <param name="xMsBlobContentLength"> This header specifies the maximum size for the page blob, up to 1 TB. The page blob size must be aligned to a 512-byte boundary. </param>
        /// <param name="xMsBlobSequenceNumber"> Set for page blobs only. The sequence number is a user-controlled value that you can use to track requests. The value of the sequence number must be between 0 and 2^63 - 1. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<CreateHeaders>> CreateAsync(int? timeout, long contentLength, AccessTier? xMsAccessTier, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, string? xMsBlobCacheControl, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, long xMsBlobContentLength, long? xMsBlobSequenceNumber, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.Create");
            scope.Start();
            try
            {
                using var message = CreateCreateRequest(timeout, contentLength, xMsAccessTier, xMsBlobContentType, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsBlobContentMd5, xMsBlobCacheControl, xMsMeta, xMsLeaseId, xMsBlobContentDisposition, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsBlobContentLength, xMsBlobSequenceNumber, xMsClientRequestId);
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
        /// <summary> The Create operation creates a new page blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the page blob. </param>
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
        /// <param name="xMsBlobContentLength"> This header specifies the maximum size for the page blob, up to 1 TB. The page blob size must be aligned to a 512-byte boundary. </param>
        /// <param name="xMsBlobSequenceNumber"> Set for page blobs only. The sequence number is a user-controlled value that you can use to track requests. The value of the sequence number must be between 0 and 2^63 - 1. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<CreateHeaders> Create(int? timeout, long contentLength, AccessTier? xMsAccessTier, string? xMsBlobContentType, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, byte[]? xMsBlobContentMd5, string? xMsBlobCacheControl, string? xMsMeta, string? xMsLeaseId, string? xMsBlobContentDisposition, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, long xMsBlobContentLength, long? xMsBlobSequenceNumber, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.Create");
            scope.Start();
            try
            {
                using var message = CreateCreateRequest(timeout, contentLength, xMsAccessTier, xMsBlobContentType, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsBlobContentMd5, xMsBlobCacheControl, xMsMeta, xMsLeaseId, xMsBlobContentDisposition, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsBlobContentLength, xMsBlobSequenceNumber, xMsClientRequestId);
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
        internal HttpMessage CreateUploadPagesRequest(long contentLength, byte[]? contentMD5, byte[]? xMsContentCrc64, int? timeout, string? xMsRange, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, long? xMsIfSequenceNumberLe, long? xMsIfSequenceNumberLt, long? xMsIfSequenceNumberEq, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-page-write", "update");
            request.Headers.Add("Content-Length", contentLength);
            if (contentMD5 != null)
            {
                request.Headers.Add("Content-MD5", contentMD5);
            }
            if (xMsContentCrc64 != null)
            {
                request.Headers.Add("x-ms-content-crc64", xMsContentCrc64);
            }
            if (xMsRange != null)
            {
                request.Headers.Add("x-ms-range", xMsRange);
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
            if (xMsIfSequenceNumberLe != null)
            {
                request.Headers.Add("x-ms-if-sequence-number-le", xMsIfSequenceNumberLe.Value);
            }
            if (xMsIfSequenceNumberLt != null)
            {
                request.Headers.Add("x-ms-if-sequence-number-lt", xMsIfSequenceNumberLt.Value);
            }
            if (xMsIfSequenceNumberEq != null)
            {
                request.Headers.Add("x-ms-if-sequence-number-eq", xMsIfSequenceNumberEq.Value);
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
            request.Uri.AppendQuery("comp", "page", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Upload Pages operation writes a range of pages to a page blob. </summary>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsContentCrc64"> Specify the transactional crc64 for the body, to be validated by the service. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsIfSequenceNumberLe"> Specify this header value to operate only on a blob if it has a sequence number less than or equal to the specified. </param>
        /// <param name="xMsIfSequenceNumberLt"> Specify this header value to operate only on a blob if it has a sequence number less than the specified. </param>
        /// <param name="xMsIfSequenceNumberEq"> Specify this header value to operate only on a blob if it has the specified sequence number. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<UploadPagesHeaders>> UploadPagesAsync(long contentLength, byte[]? contentMD5, byte[]? xMsContentCrc64, int? timeout, string? xMsRange, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, long? xMsIfSequenceNumberLe, long? xMsIfSequenceNumberLt, long? xMsIfSequenceNumberEq, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.UploadPages");
            scope.Start();
            try
            {
                using var message = CreateUploadPagesRequest(contentLength, contentMD5, xMsContentCrc64, timeout, xMsRange, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, xMsIfSequenceNumberLe, xMsIfSequenceNumberLt, xMsIfSequenceNumberEq, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new UploadPagesHeaders(message.Response);
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
        /// <summary> The Upload Pages operation writes a range of pages to a page blob. </summary>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentMD5"> Specify the transactional md5 for the body, to be validated by the service. </param>
        /// <param name="xMsContentCrc64"> Specify the transactional crc64 for the body, to be validated by the service. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsIfSequenceNumberLe"> Specify this header value to operate only on a blob if it has a sequence number less than or equal to the specified. </param>
        /// <param name="xMsIfSequenceNumberLt"> Specify this header value to operate only on a blob if it has a sequence number less than the specified. </param>
        /// <param name="xMsIfSequenceNumberEq"> Specify this header value to operate only on a blob if it has the specified sequence number. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<UploadPagesHeaders> UploadPages(long contentLength, byte[]? contentMD5, byte[]? xMsContentCrc64, int? timeout, string? xMsRange, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, long? xMsIfSequenceNumberLe, long? xMsIfSequenceNumberLt, long? xMsIfSequenceNumberEq, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.UploadPages");
            scope.Start();
            try
            {
                using var message = CreateUploadPagesRequest(contentLength, contentMD5, xMsContentCrc64, timeout, xMsRange, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, xMsIfSequenceNumberLe, xMsIfSequenceNumberLt, xMsIfSequenceNumberEq, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new UploadPagesHeaders(message.Response);
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
        internal HttpMessage CreateClearPagesRequest(long contentLength, int? timeout, string? xMsRange, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, long? xMsIfSequenceNumberLe, long? xMsIfSequenceNumberLt, long? xMsIfSequenceNumberEq, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-page-write", "clear");
            request.Headers.Add("Content-Length", contentLength);
            if (xMsRange != null)
            {
                request.Headers.Add("x-ms-range", xMsRange);
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
            if (xMsIfSequenceNumberLe != null)
            {
                request.Headers.Add("x-ms-if-sequence-number-le", xMsIfSequenceNumberLe.Value);
            }
            if (xMsIfSequenceNumberLt != null)
            {
                request.Headers.Add("x-ms-if-sequence-number-lt", xMsIfSequenceNumberLt.Value);
            }
            if (xMsIfSequenceNumberEq != null)
            {
                request.Headers.Add("x-ms-if-sequence-number-eq", xMsIfSequenceNumberEq.Value);
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
            request.Uri.AppendQuery("comp", "page", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Clear Pages operation clears a set of pages from a page blob. </summary>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsIfSequenceNumberLe"> Specify this header value to operate only on a blob if it has a sequence number less than or equal to the specified. </param>
        /// <param name="xMsIfSequenceNumberLt"> Specify this header value to operate only on a blob if it has a sequence number less than the specified. </param>
        /// <param name="xMsIfSequenceNumberEq"> Specify this header value to operate only on a blob if it has the specified sequence number. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<ClearPagesHeaders>> ClearPagesAsync(long contentLength, int? timeout, string? xMsRange, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, long? xMsIfSequenceNumberLe, long? xMsIfSequenceNumberLt, long? xMsIfSequenceNumberEq, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.ClearPages");
            scope.Start();
            try
            {
                using var message = CreateClearPagesRequest(contentLength, timeout, xMsRange, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, xMsIfSequenceNumberLe, xMsIfSequenceNumberLt, xMsIfSequenceNumberEq, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new ClearPagesHeaders(message.Response);
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
        /// <summary> The Clear Pages operation clears a set of pages from a page blob. </summary>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsIfSequenceNumberLe"> Specify this header value to operate only on a blob if it has a sequence number less than or equal to the specified. </param>
        /// <param name="xMsIfSequenceNumberLt"> Specify this header value to operate only on a blob if it has a sequence number less than the specified. </param>
        /// <param name="xMsIfSequenceNumberEq"> Specify this header value to operate only on a blob if it has the specified sequence number. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<ClearPagesHeaders> ClearPages(long contentLength, int? timeout, string? xMsRange, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, long? xMsIfSequenceNumberLe, long? xMsIfSequenceNumberLt, long? xMsIfSequenceNumberEq, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.ClearPages");
            scope.Start();
            try
            {
                using var message = CreateClearPagesRequest(contentLength, timeout, xMsRange, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, xMsIfSequenceNumberLe, xMsIfSequenceNumberLt, xMsIfSequenceNumberEq, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new ClearPagesHeaders(message.Response);
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
        internal HttpMessage CreateUploadPagesFromUriRequest(Uri xMsCopySource, string xMsSourceRange, byte[]? xMsSourceContentMd5, byte[]? xMsSourceContentCrc64, long contentLength, int? timeout, string xMsRange, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsLeaseId, long? xMsIfSequenceNumberLe, long? xMsIfSequenceNumberLt, long? xMsIfSequenceNumberEq, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-page-write", "update");
            request.Headers.Add("x-ms-copy-source", xMsCopySource);
            request.Headers.Add("x-ms-source-range", xMsSourceRange);
            if (xMsSourceContentMd5 != null)
            {
                request.Headers.Add("x-ms-source-content-md5", xMsSourceContentMd5);
            }
            if (xMsSourceContentCrc64 != null)
            {
                request.Headers.Add("x-ms-source-content-crc64", xMsSourceContentCrc64);
            }
            request.Headers.Add("Content-Length", contentLength);
            request.Headers.Add("x-ms-range", xMsRange);
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
            if (xMsIfSequenceNumberLe != null)
            {
                request.Headers.Add("x-ms-if-sequence-number-le", xMsIfSequenceNumberLe.Value);
            }
            if (xMsIfSequenceNumberLt != null)
            {
                request.Headers.Add("x-ms-if-sequence-number-lt", xMsIfSequenceNumberLt.Value);
            }
            if (xMsIfSequenceNumberEq != null)
            {
                request.Headers.Add("x-ms-if-sequence-number-eq", xMsIfSequenceNumberEq.Value);
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
            request.Uri.AppendQuery("comp", "page", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Upload Pages operation writes a range of pages to a page blob where the contents are read from a URL. </summary>
        /// <param name="xMsCopySource"> Specify a URL to the copy source. </param>
        /// <param name="xMsSourceRange"> Bytes of source data in the specified range. The length of this range should match the ContentLength header and x-ms-range/Range destination range header. </param>
        /// <param name="xMsSourceContentMd5"> Specify the md5 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="xMsSourceContentCrc64"> Specify the crc64 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> The range of bytes to which the source range would be written. The range should be 512 aligned and range-end is required. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsIfSequenceNumberLe"> Specify this header value to operate only on a blob if it has a sequence number less than or equal to the specified. </param>
        /// <param name="xMsIfSequenceNumberLt"> Specify this header value to operate only on a blob if it has a sequence number less than the specified. </param>
        /// <param name="xMsIfSequenceNumberEq"> Specify this header value to operate only on a blob if it has the specified sequence number. </param>
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
        public async ValueTask<ResponseWithHeaders<UploadPagesFromUriHeaders>> UploadPagesFromUriAsync(Uri xMsCopySource, string xMsSourceRange, byte[]? xMsSourceContentMd5, byte[]? xMsSourceContentCrc64, long contentLength, int? timeout, string xMsRange, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsLeaseId, long? xMsIfSequenceNumberLe, long? xMsIfSequenceNumberLt, long? xMsIfSequenceNumberEq, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }
            if (xMsSourceRange == null)
            {
                throw new ArgumentNullException(nameof(xMsSourceRange));
            }
            if (xMsRange == null)
            {
                throw new ArgumentNullException(nameof(xMsRange));
            }

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.UploadPagesFromUri");
            scope.Start();
            try
            {
                using var message = CreateUploadPagesFromUriRequest(xMsCopySource, xMsSourceRange, xMsSourceContentMd5, xMsSourceContentCrc64, contentLength, timeout, xMsRange, xMsEncryptionKey, xMsEncryptionKeySha256, xMsLeaseId, xMsIfSequenceNumberLe, xMsIfSequenceNumberLt, xMsIfSequenceNumberEq, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new UploadPagesFromUriHeaders(message.Response);
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
        /// <summary> The Upload Pages operation writes a range of pages to a page blob where the contents are read from a URL. </summary>
        /// <param name="xMsCopySource"> Specify a URL to the copy source. </param>
        /// <param name="xMsSourceRange"> Bytes of source data in the specified range. The length of this range should match the ContentLength header and x-ms-range/Range destination range header. </param>
        /// <param name="xMsSourceContentMd5"> Specify the md5 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="xMsSourceContentCrc64"> Specify the crc64 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> The range of bytes to which the source range would be written. The range should be 512 aligned and range-end is required. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsIfSequenceNumberLe"> Specify this header value to operate only on a blob if it has a sequence number less than or equal to the specified. </param>
        /// <param name="xMsIfSequenceNumberLt"> Specify this header value to operate only on a blob if it has a sequence number less than the specified. </param>
        /// <param name="xMsIfSequenceNumberEq"> Specify this header value to operate only on a blob if it has the specified sequence number. </param>
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
        public ResponseWithHeaders<UploadPagesFromUriHeaders> UploadPagesFromUri(Uri xMsCopySource, string xMsSourceRange, byte[]? xMsSourceContentMd5, byte[]? xMsSourceContentCrc64, long contentLength, int? timeout, string xMsRange, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, string? xMsLeaseId, long? xMsIfSequenceNumberLe, long? xMsIfSequenceNumberLt, long? xMsIfSequenceNumberEq, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }
            if (xMsSourceRange == null)
            {
                throw new ArgumentNullException(nameof(xMsSourceRange));
            }
            if (xMsRange == null)
            {
                throw new ArgumentNullException(nameof(xMsRange));
            }

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.UploadPagesFromUri");
            scope.Start();
            try
            {
                using var message = CreateUploadPagesFromUriRequest(xMsCopySource, xMsSourceRange, xMsSourceContentMd5, xMsSourceContentCrc64, contentLength, timeout, xMsRange, xMsEncryptionKey, xMsEncryptionKeySha256, xMsLeaseId, xMsIfSequenceNumberLe, xMsIfSequenceNumberLt, xMsIfSequenceNumberEq, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new UploadPagesFromUriHeaders(message.Response);
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
        internal HttpMessage CreateGetPageRangesRequest(string? snapshot, int? timeout, string? xMsRange, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            if (xMsRange != null)
            {
                request.Headers.Add("x-ms-range", xMsRange);
            }
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
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
            request.Uri.AppendQuery("comp", "pagelist", true);
            if (snapshot != null)
            {
                request.Uri.AppendQuery("snapshot", snapshot, true);
            }
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Get Page Ranges operation returns the list of valid page ranges for a page blob or snapshot of a page blob. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<PageList, GetPageRangesHeaders>> GetPageRangesAsync(string? snapshot, int? timeout, string? xMsRange, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.GetPageRanges");
            scope.Start();
            try
            {
                using var message = CreateGetPageRangesRequest(snapshot, timeout, xMsRange, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            PageList value = default;
                            var pageList = document.Element("PageList");
                            if (pageList != null)
                            {
                                value = PageList.DeserializePageList(pageList);
                            }
                            var headers = new GetPageRangesHeaders(message.Response);
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
        /// <summary> The Get Page Ranges operation returns the list of valid page ranges for a page blob or snapshot of a page blob. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<PageList, GetPageRangesHeaders> GetPageRanges(string? snapshot, int? timeout, string? xMsRange, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.GetPageRanges");
            scope.Start();
            try
            {
                using var message = CreateGetPageRangesRequest(snapshot, timeout, xMsRange, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            PageList value = default;
                            var pageList = document.Element("PageList");
                            if (pageList != null)
                            {
                                value = PageList.DeserializePageList(pageList);
                            }
                            var headers = new GetPageRangesHeaders(message.Response);
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
        internal HttpMessage CreateGetPageRangesDiffRequest(string? snapshot, int? timeout, string? prevsnapshot, string? xMsRange, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            if (xMsRange != null)
            {
                request.Headers.Add("x-ms-range", xMsRange);
            }
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
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
            request.Uri.AppendQuery("comp", "pagelist", true);
            if (snapshot != null)
            {
                request.Uri.AppendQuery("snapshot", snapshot, true);
            }
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            if (prevsnapshot != null)
            {
                request.Uri.AppendQuery("prevsnapshot", prevsnapshot, true);
            }
            return message;
        }
        /// <summary> The Get Page Ranges Diff operation returns the list of valid page ranges for a page blob that were changed between target blob and previous snapshot. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="prevsnapshot"> Optional in version 2015-07-08 and newer. The prevsnapshot parameter is a DateTime value that specifies that the response will contain only pages that were changed between target blob and previous snapshot. Changed pages include both updated and cleared pages. The target blob may be a snapshot, as long as the snapshot specified by prevsnapshot is the older of the two. Note that incremental snapshots are currently supported only for blobs created on or after January 1, 2016. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<PageList, GetPageRangesDiffHeaders>> GetPageRangesDiffAsync(string? snapshot, int? timeout, string? prevsnapshot, string? xMsRange, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.GetPageRangesDiff");
            scope.Start();
            try
            {
                using var message = CreateGetPageRangesDiffRequest(snapshot, timeout, prevsnapshot, xMsRange, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            PageList value = default;
                            var pageList = document.Element("PageList");
                            if (pageList != null)
                            {
                                value = PageList.DeserializePageList(pageList);
                            }
                            var headers = new GetPageRangesDiffHeaders(message.Response);
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
        /// <summary> The Get Page Ranges Diff operation returns the list of valid page ranges for a page blob that were changed between target blob and previous snapshot. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="prevsnapshot"> Optional in version 2015-07-08 and newer. The prevsnapshot parameter is a DateTime value that specifies that the response will contain only pages that were changed between target blob and previous snapshot. Changed pages include both updated and cleared pages. The target blob may be a snapshot, as long as the snapshot specified by prevsnapshot is the older of the two. Note that incremental snapshots are currently supported only for blobs created on or after January 1, 2016. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<PageList, GetPageRangesDiffHeaders> GetPageRangesDiff(string? snapshot, int? timeout, string? prevsnapshot, string? xMsRange, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.GetPageRangesDiff");
            scope.Start();
            try
            {
                using var message = CreateGetPageRangesDiffRequest(snapshot, timeout, prevsnapshot, xMsRange, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            PageList value = default;
                            var pageList = document.Element("PageList");
                            if (pageList != null)
                            {
                                value = PageList.DeserializePageList(pageList);
                            }
                            var headers = new GetPageRangesDiffHeaders(message.Response);
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
        internal HttpMessage CreateResizeRequest(int? timeout, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, long xMsBlobContentLength, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
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
            request.Headers.Add("x-ms-blob-content-length", xMsBlobContentLength);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "properties", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> Resize the Blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsBlobContentLength"> This header specifies the maximum size for the page blob, up to 1 TB. The page blob size must be aligned to a 512-byte boundary. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<ResizeHeaders>> ResizeAsync(int? timeout, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, long xMsBlobContentLength, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.Resize");
            scope.Start();
            try
            {
                using var message = CreateResizeRequest(timeout, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsBlobContentLength, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new ResizeHeaders(message.Response);
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
        /// <summary> Resize the Blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsBlobContentLength"> This header specifies the maximum size for the page blob, up to 1 TB. The page blob size must be aligned to a 512-byte boundary. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<ResizeHeaders> Resize(int? timeout, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, long xMsBlobContentLength, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.Resize");
            scope.Start();
            try
            {
                using var message = CreateResizeRequest(timeout, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsBlobContentLength, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new ResizeHeaders(message.Response);
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
        internal HttpMessage CreateUpdateSequenceNumberRequest(int? timeout, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, SequenceNumberAction xMsSequenceNumberAction, long? xMsBlobSequenceNumber, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
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
            request.Headers.Add("x-ms-sequence-number-action", xMsSequenceNumberAction);
            if (xMsBlobSequenceNumber != null)
            {
                request.Headers.Add("x-ms-blob-sequence-number", xMsBlobSequenceNumber.Value);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "properties", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> Update the sequence number of the blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsSequenceNumberAction"> Required if the x-ms-blob-sequence-number header is set for the request. This property applies to page blobs only. This property indicates how the service should modify the blob&apos;s sequence number. </param>
        /// <param name="xMsBlobSequenceNumber"> Set for page blobs only. The sequence number is a user-controlled value that you can use to track requests. The value of the sequence number must be between 0 and 2^63 - 1. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<UpdateSequenceNumberHeaders>> UpdateSequenceNumberAsync(int? timeout, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, SequenceNumberAction xMsSequenceNumberAction, long? xMsBlobSequenceNumber, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.UpdateSequenceNumber");
            scope.Start();
            try
            {
                using var message = CreateUpdateSequenceNumberRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSequenceNumberAction, xMsBlobSequenceNumber, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new UpdateSequenceNumberHeaders(message.Response);
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
        /// <summary> Update the sequence number of the blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsSequenceNumberAction"> Required if the x-ms-blob-sequence-number header is set for the request. This property applies to page blobs only. This property indicates how the service should modify the blob&apos;s sequence number. </param>
        /// <param name="xMsBlobSequenceNumber"> Set for page blobs only. The sequence number is a user-controlled value that you can use to track requests. The value of the sequence number must be between 0 and 2^63 - 1. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<UpdateSequenceNumberHeaders> UpdateSequenceNumber(int? timeout, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, SequenceNumberAction xMsSequenceNumberAction, long? xMsBlobSequenceNumber, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.UpdateSequenceNumber");
            scope.Start();
            try
            {
                using var message = CreateUpdateSequenceNumberRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSequenceNumberAction, xMsBlobSequenceNumber, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new UpdateSequenceNumberHeaders(message.Response);
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
        internal HttpMessage CreateCopyIncrementalRequest(int? timeout, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, Uri xMsCopySource, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
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
            request.Headers.Add("x-ms-copy-source", xMsCopySource);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "incrementalcopy", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Copy Incremental operation copies a snapshot of the source page blob to a destination page blob. The snapshot is copied such that only the differential changes between the previously copied snapshot are transferred to the destination. The copied snapshots are complete copies of the original snapshot and can be read or copied from as usual. This API is supported since REST version 2016-05-31. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsCopySource"> Specifies the name of the source page blob snapshot. This value is a URL of up to 2 KB in length that specifies a page blob snapshot. The value should be URL-encoded as it would appear in a request URI. The source blob must either be public or must be authenticated via a shared access signature. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<CopyIncrementalHeaders>> CopyIncrementalAsync(int? timeout, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, Uri xMsCopySource, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.CopyIncremental");
            scope.Start();
            try
            {
                using var message = CreateCopyIncrementalRequest(timeout, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsCopySource, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new CopyIncrementalHeaders(message.Response);
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
        /// <summary> The Copy Incremental operation copies a snapshot of the source page blob to a destination page blob. The snapshot is copied such that only the differential changes between the previously copied snapshot are transferred to the destination. The copied snapshots are complete copies of the original snapshot and can be read or copied from as usual. This API is supported since REST version 2016-05-31. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsCopySource"> Specifies the name of the source page blob snapshot. This value is a URL of up to 2 KB in length that specifies a page blob snapshot. The value should be URL-encoded as it would appear in a request URI. The source blob must either be public or must be authenticated via a shared access signature. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<CopyIncrementalHeaders> CopyIncremental(int? timeout, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, Uri xMsCopySource, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("PageBlobOperations.CopyIncremental");
            scope.Start();
            try
            {
                using var message = CreateCopyIncrementalRequest(timeout, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsCopySource, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new CopyIncrementalHeaders(message.Response);
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
