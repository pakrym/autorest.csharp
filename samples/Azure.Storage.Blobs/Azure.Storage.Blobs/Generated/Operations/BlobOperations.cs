// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Storage.Blobs.Models.V20190202;

namespace Azure.Storage.Blobs
{
    internal partial class BlobOperations
    {
        private string url;
        private string xMsVersion;
        private PathRenameMode? mode;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of BlobOperations. </summary>
        public BlobOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string url, string xMsVersion, PathRenameMode? mode)
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
            this.mode = mode;
            this.clientDiagnostics = clientDiagnostics;
            this.pipeline = pipeline;
        }
        internal HttpMessage CreateDownloadRequest(string? snapshot, int? timeout, string? xMsRange, string? xMsLeaseId, bool? xMsRangeGetContentMd5, bool? xMsRangeGetContentCrc64, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
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
            if (xMsRangeGetContentMd5 != null)
            {
                request.Headers.Add("x-ms-range-get-content-md5", xMsRangeGetContentMd5.Value);
            }
            if (xMsRangeGetContentCrc64 != null)
            {
                request.Headers.Add("x-ms-range-get-content-crc64", xMsRangeGetContentCrc64.Value);
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
        /// <summary> The Download operation reads or downloads a blob from the system, including its metadata and properties. You can also call Download to read a snapshot. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsRangeGetContentMd5"> When set to true and specified together with the Range, the service returns the MD5 hash for the range, as long as the range is less than or equal to 4 MB in size. </param>
        /// <param name="xMsRangeGetContentCrc64"> When set to true and specified together with the Range, the service returns the CRC64 hash for the range, as long as the range is less than or equal to 4 MB in size. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<DownloadHeaders>> DownloadAsync(string? snapshot, int? timeout, string? xMsRange, string? xMsLeaseId, bool? xMsRangeGetContentMd5, bool? xMsRangeGetContentCrc64, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.Download");
            scope.Start();
            try
            {
                using var message = CreateDownloadRequest(snapshot, timeout, xMsRange, xMsLeaseId, xMsRangeGetContentMd5, xMsRangeGetContentCrc64, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new DownloadHeaders(message.Response);
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
        /// <summary> The Download operation reads or downloads a blob from the system, including its metadata and properties. You can also call Download to read a snapshot. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRange"> Return only the bytes of the blob in the specified range. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsRangeGetContentMd5"> When set to true and specified together with the Range, the service returns the MD5 hash for the range, as long as the range is less than or equal to 4 MB in size. </param>
        /// <param name="xMsRangeGetContentCrc64"> When set to true and specified together with the Range, the service returns the CRC64 hash for the range, as long as the range is less than or equal to 4 MB in size. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<DownloadHeaders> Download(string? snapshot, int? timeout, string? xMsRange, string? xMsLeaseId, bool? xMsRangeGetContentMd5, bool? xMsRangeGetContentCrc64, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.Download");
            scope.Start();
            try
            {
                using var message = CreateDownloadRequest(snapshot, timeout, xMsRange, xMsLeaseId, xMsRangeGetContentMd5, xMsRangeGetContentCrc64, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new DownloadHeaders(message.Response);
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
        internal HttpMessage CreateGetPropertiesRequest(string? snapshot, int? timeout, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Head;
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
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
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
        /// <summary> The Get Properties operation returns all user-defined metadata, standard HTTP properties, and system properties for the blob. It does not return the content of the blob. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<GetPropertiesHeaders>> GetPropertiesAsync(string? snapshot, int? timeout, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.GetProperties");
            scope.Start();
            try
            {
                using var message = CreateGetPropertiesRequest(snapshot, timeout, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new GetPropertiesHeaders(message.Response);
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
        /// <summary> The Get Properties operation returns all user-defined metadata, standard HTTP properties, and system properties for the blob. It does not return the content of the blob. </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<GetPropertiesHeaders> GetProperties(string? snapshot, int? timeout, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.GetProperties");
            scope.Start();
            try
            {
                using var message = CreateGetPropertiesRequest(snapshot, timeout, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new GetPropertiesHeaders(message.Response);
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
        internal HttpMessage CreateDeleteRequest(string? snapshot, int? timeout, string? xMsLeaseId, DeleteSnapshotsOption? xMsDeleteSnapshots, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Delete;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            if (xMsDeleteSnapshots != null)
            {
                request.Headers.Add("x-ms-delete-snapshots", xMsDeleteSnapshots.Value);
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
        /// <summary> If the storage account&apos;s soft delete feature is disabled then, when a blob is deleted, it is permanently removed from the storage account. If the storage account&apos;s soft delete feature is enabled, then, when a blob is deleted, it is marked for deletion and becomes inaccessible immediately. However, the blob service retains the blob or snapshot for the number of days specified by the DeleteRetentionPolicy section of [Storage service properties] (Set-Blob-Service-Properties.md). After the specified number of days has passed, the blob&apos;s data is permanently removed from the storage account. Note that you continue to be charged for the soft-deleted blob&apos;s storage until it is permanently removed. Use the List Blobs API and specify the &quot;include=deleted&quot; query parameter to discover which blobs and snapshots have been soft deleted. You can then use the Undelete Blob API to restore a soft-deleted blob. All other operations on a soft-deleted blob or snapshot causes the service to return an HTTP status code of 404 (ResourceNotFound). </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsDeleteSnapshots"> Required if the blob has associated snapshots. Specify one of the following two options: include: Delete the base blob and all of its snapshots. only: Delete only the blob&apos;s snapshots and not the blob itself. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<DeleteHeaders>> DeleteAsync(string? snapshot, int? timeout, string? xMsLeaseId, DeleteSnapshotsOption? xMsDeleteSnapshots, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.Delete");
            scope.Start();
            try
            {
                using var message = CreateDeleteRequest(snapshot, timeout, xMsLeaseId, xMsDeleteSnapshots, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new DeleteHeaders(message.Response);
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
        /// <summary> If the storage account&apos;s soft delete feature is disabled then, when a blob is deleted, it is permanently removed from the storage account. If the storage account&apos;s soft delete feature is enabled, then, when a blob is deleted, it is marked for deletion and becomes inaccessible immediately. However, the blob service retains the blob or snapshot for the number of days specified by the DeleteRetentionPolicy section of [Storage service properties] (Set-Blob-Service-Properties.md). After the specified number of days has passed, the blob&apos;s data is permanently removed from the storage account. Note that you continue to be charged for the soft-deleted blob&apos;s storage until it is permanently removed. Use the List Blobs API and specify the &quot;include=deleted&quot; query parameter to discover which blobs and snapshots have been soft deleted. You can then use the Undelete Blob API to restore a soft-deleted blob. All other operations on a soft-deleted blob or snapshot causes the service to return an HTTP status code of 404 (ResourceNotFound). </summary>
        /// <param name="snapshot"> The snapshot parameter is an opaque DateTime value that, when present, specifies the blob snapshot to retrieve. For more information on working with blob snapshots, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/creating-a-snapshot-of-a-blob&quot;&gt;Creating a Snapshot of a Blob.&lt;/a&gt;. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsDeleteSnapshots"> Required if the blob has associated snapshots. Specify one of the following two options: include: Delete the base blob and all of its snapshots. only: Delete only the blob&apos;s snapshots and not the blob itself. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<DeleteHeaders> Delete(string? snapshot, int? timeout, string? xMsLeaseId, DeleteSnapshotsOption? xMsDeleteSnapshots, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.Delete");
            scope.Start();
            try
            {
                using var message = CreateDeleteRequest(snapshot, timeout, xMsLeaseId, xMsDeleteSnapshots, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new DeleteHeaders(message.Response);
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
        internal HttpMessage CreateSetAccessControlRequest(int? timeout, string? xMsLeaseId, string? xMsOwner, string? xMsGroup, string? xMsPermissions, string? xMsAcl, string? ifMatch, string? ifNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Patch;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("filesystem", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("path", false);
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            if (xMsOwner != null)
            {
                request.Headers.Add("x-ms-owner", xMsOwner);
            }
            if (xMsGroup != null)
            {
                request.Headers.Add("x-ms-group", xMsGroup);
            }
            if (xMsPermissions != null)
            {
                request.Headers.Add("x-ms-permissions", xMsPermissions);
            }
            if (xMsAcl != null)
            {
                request.Headers.Add("x-ms-acl", xMsAcl);
            }
            if (ifMatch != null)
            {
                request.Headers.Add("If-Match", ifMatch);
            }
            if (ifNoneMatch != null)
            {
                request.Headers.Add("If-None-Match", ifNoneMatch);
            }
            if (ifModifiedSince != null)
            {
                request.Headers.Add("If-Modified-Since", ifModifiedSince.Value, "R");
            }
            if (ifUnmodifiedSince != null)
            {
                request.Headers.Add("If-Unmodified-Since", ifUnmodifiedSince.Value, "R");
            }
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            request.Uri.AppendQuery("action", "setAccessControl", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> Set the owner, group, permissions, or access control list for a blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsOwner"> Optional. The owner of the blob or directory. </param>
        /// <param name="xMsGroup"> Optional. The owning group of the blob or directory. </param>
        /// <param name="xMsPermissions"> Optional and only valid if Hierarchical Namespace is enabled for the account. Sets POSIX access permissions for the file owner, the file owning group, and others. Each class may be granted read, write, or execute permission.  The sticky bit is also supported.  Both symbolic (rwxrw-rw-) and 4-digit octal notation (e.g. 0766) are supported. </param>
        /// <param name="xMsAcl"> Sets POSIX access control rights on files and directories. The value is a comma-separated list of access control entries. Each access control entry (ACE) consists of a scope, a type, a user or group identifier, and permissions in the format &quot;[scope:][type]:[id]:[permissions]&quot;. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<SetAccessControlHeaders>> SetAccessControlAsync(int? timeout, string? xMsLeaseId, string? xMsOwner, string? xMsGroup, string? xMsPermissions, string? xMsAcl, string? ifMatch, string? ifNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.SetAccessControl");
            scope.Start();
            try
            {
                using var message = CreateSetAccessControlRequest(timeout, xMsLeaseId, xMsOwner, xMsGroup, xMsPermissions, xMsAcl, ifMatch, ifNoneMatch, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetAccessControlHeaders(message.Response);
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
        /// <summary> Set the owner, group, permissions, or access control list for a blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsOwner"> Optional. The owner of the blob or directory. </param>
        /// <param name="xMsGroup"> Optional. The owning group of the blob or directory. </param>
        /// <param name="xMsPermissions"> Optional and only valid if Hierarchical Namespace is enabled for the account. Sets POSIX access permissions for the file owner, the file owning group, and others. Each class may be granted read, write, or execute permission.  The sticky bit is also supported.  Both symbolic (rwxrw-rw-) and 4-digit octal notation (e.g. 0766) are supported. </param>
        /// <param name="xMsAcl"> Sets POSIX access control rights on files and directories. The value is a comma-separated list of access control entries. Each access control entry (ACE) consists of a scope, a type, a user or group identifier, and permissions in the format &quot;[scope:][type]:[id]:[permissions]&quot;. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<SetAccessControlHeaders> SetAccessControl(int? timeout, string? xMsLeaseId, string? xMsOwner, string? xMsGroup, string? xMsPermissions, string? xMsAcl, string? ifMatch, string? ifNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.SetAccessControl");
            scope.Start();
            try
            {
                using var message = CreateSetAccessControlRequest(timeout, xMsLeaseId, xMsOwner, xMsGroup, xMsPermissions, xMsAcl, ifMatch, ifNoneMatch, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetAccessControlHeaders(message.Response);
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
        internal HttpMessage CreateGetAccessControlRequest(int? timeout, bool? upn, string? xMsLeaseId, string? ifMatch, string? ifNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Head;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("filesystem", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("path", false);
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            if (ifMatch != null)
            {
                request.Headers.Add("If-Match", ifMatch);
            }
            if (ifNoneMatch != null)
            {
                request.Headers.Add("If-None-Match", ifNoneMatch);
            }
            if (ifModifiedSince != null)
            {
                request.Headers.Add("If-Modified-Since", ifModifiedSince.Value, "R");
            }
            if (ifUnmodifiedSince != null)
            {
                request.Headers.Add("If-Unmodified-Since", ifUnmodifiedSince.Value, "R");
            }
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            request.Uri.AppendQuery("action", "getAccessControl", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            if (upn != null)
            {
                request.Uri.AppendQuery("upn", upn.Value, true);
            }
            return message;
        }
        /// <summary> Get the owner, group, permissions, or access control list for a blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="upn"> Optional. Valid only when Hierarchical Namespace is enabled for the account. If &quot;true&quot;, the identity values returned in the x-ms-owner, x-ms-group, and x-ms-acl response headers will be transformed from Azure Active Directory Object IDs to User Principal Names.  If &quot;false&quot;, the values will be returned as Azure Active Directory Object IDs. The default value is false. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<GetAccessControlHeaders>> GetAccessControlAsync(int? timeout, bool? upn, string? xMsLeaseId, string? ifMatch, string? ifNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.GetAccessControl");
            scope.Start();
            try
            {
                using var message = CreateGetAccessControlRequest(timeout, upn, xMsLeaseId, ifMatch, ifNoneMatch, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new GetAccessControlHeaders(message.Response);
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
        /// <summary> Get the owner, group, permissions, or access control list for a blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="upn"> Optional. Valid only when Hierarchical Namespace is enabled for the account. If &quot;true&quot;, the identity values returned in the x-ms-owner, x-ms-group, and x-ms-acl response headers will be transformed from Azure Active Directory Object IDs to User Principal Names.  If &quot;false&quot;, the values will be returned as Azure Active Directory Object IDs. The default value is false. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<GetAccessControlHeaders> GetAccessControl(int? timeout, bool? upn, string? xMsLeaseId, string? ifMatch, string? ifNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.GetAccessControl");
            scope.Start();
            try
            {
                using var message = CreateGetAccessControlRequest(timeout, upn, xMsLeaseId, ifMatch, ifNoneMatch, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new GetAccessControlHeaders(message.Response);
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
        internal HttpMessage CreateRenameRequest(int? timeout, string xMsRenameSource, string? xMsProperties, string? xMsPermissions, string? xMsUmask, string? xMsCacheControl, string? xMsContentType, string? xMsContentEncoding, string? xMsContentLanguage, string? xMsContentDisposition, string? xMsLeaseId, string? xMsSourceLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("filesystem", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("path", false);
            request.Headers.Add("x-ms-rename-source", xMsRenameSource);
            if (xMsProperties != null)
            {
                request.Headers.Add("x-ms-properties", xMsProperties);
            }
            if (xMsPermissions != null)
            {
                request.Headers.Add("x-ms-permissions", xMsPermissions);
            }
            if (xMsUmask != null)
            {
                request.Headers.Add("x-ms-umask", xMsUmask);
            }
            if (xMsCacheControl != null)
            {
                request.Headers.Add("x-ms-cache-control", xMsCacheControl);
            }
            if (xMsContentType != null)
            {
                request.Headers.Add("x-ms-content-type", xMsContentType);
            }
            if (xMsContentEncoding != null)
            {
                request.Headers.Add("x-ms-content-encoding", xMsContentEncoding);
            }
            if (xMsContentLanguage != null)
            {
                request.Headers.Add("x-ms-content-language", xMsContentLanguage);
            }
            if (xMsContentDisposition != null)
            {
                request.Headers.Add("x-ms-content-disposition", xMsContentDisposition);
            }
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            if (xMsSourceLeaseId != null)
            {
                request.Headers.Add("x-ms-source-lease-id", xMsSourceLeaseId);
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
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            if (mode != null)
            {
                request.Uri.AppendQuery("mode", mode.Value, true);
            }
            return message;
        }
        /// <summary> Rename a blob/file.  By default, the destination is overwritten and if the destination already exists and has a lease the lease is broken.  This operation supports conditional HTTP requests.  For more information, see [Specifying Conditional Headers for Blob Service Operations](https://docs.microsoft.com/en-us/rest/api/storageservices/specifying-conditional-headers-for-blob-service-operations).  To fail if the destination already exists, use a conditional request with If-None-Match: &quot;*&quot;. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRenameSource"> The file or directory to be renamed. The value must have the following format: &quot;/{filesysystem}/{path}&quot;.  If &quot;x-ms-properties&quot; is specified, the properties will overwrite the existing properties; otherwise, the existing properties will be preserved. </param>
        /// <param name="xMsProperties"> Optional.  User-defined properties to be stored with the file or directory, in the format of a comma-separated list of name and value pairs &quot;n1=v1, n2=v2, ...&quot;, where each value is base64 encoded. </param>
        /// <param name="xMsPermissions"> Optional and only valid if Hierarchical Namespace is enabled for the account. Sets POSIX access permissions for the file owner, the file owning group, and others. Each class may be granted read, write, or execute permission.  The sticky bit is also supported.  Both symbolic (rwxrw-rw-) and 4-digit octal notation (e.g. 0766) are supported. </param>
        /// <param name="xMsUmask"> Only valid if Hierarchical Namespace is enabled for the account. This umask restricts permission settings for file and directory, and will only be applied when default Acl does not exist in parent directory. If the umask bit has set, it means that the corresponding permission will be disabled. Otherwise the corresponding permission will be determined by the permission. A 4-digit octal notation (e.g. 0022) is supported here. If no umask was specified, a default umask - 0027 will be used. </param>
        /// <param name="xMsCacheControl"> Cache control for given resource. </param>
        /// <param name="xMsContentType"> Content type for given resource. </param>
        /// <param name="xMsContentEncoding"> Content encoding for given resource. </param>
        /// <param name="xMsContentLanguage"> Content language for given resource. </param>
        /// <param name="xMsContentDisposition"> Content disposition for given resource. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsSourceLeaseId"> A lease ID for the source path. If specified, the source path must have an active lease and the leaase ID must match. </param>
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
        public async ValueTask<ResponseWithHeaders<RenameHeaders>> RenameAsync(int? timeout, string xMsRenameSource, string? xMsProperties, string? xMsPermissions, string? xMsUmask, string? xMsCacheControl, string? xMsContentType, string? xMsContentEncoding, string? xMsContentLanguage, string? xMsContentDisposition, string? xMsLeaseId, string? xMsSourceLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsRenameSource == null)
            {
                throw new ArgumentNullException(nameof(xMsRenameSource));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.Rename");
            scope.Start();
            try
            {
                using var message = CreateRenameRequest(timeout, xMsRenameSource, xMsProperties, xMsPermissions, xMsUmask, xMsCacheControl, xMsContentType, xMsContentEncoding, xMsContentLanguage, xMsContentDisposition, xMsLeaseId, xMsSourceLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new RenameHeaders(message.Response);
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
        /// <summary> Rename a blob/file.  By default, the destination is overwritten and if the destination already exists and has a lease the lease is broken.  This operation supports conditional HTTP requests.  For more information, see [Specifying Conditional Headers for Blob Service Operations](https://docs.microsoft.com/en-us/rest/api/storageservices/specifying-conditional-headers-for-blob-service-operations).  To fail if the destination already exists, use a conditional request with If-None-Match: &quot;*&quot;. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsRenameSource"> The file or directory to be renamed. The value must have the following format: &quot;/{filesysystem}/{path}&quot;.  If &quot;x-ms-properties&quot; is specified, the properties will overwrite the existing properties; otherwise, the existing properties will be preserved. </param>
        /// <param name="xMsProperties"> Optional.  User-defined properties to be stored with the file or directory, in the format of a comma-separated list of name and value pairs &quot;n1=v1, n2=v2, ...&quot;, where each value is base64 encoded. </param>
        /// <param name="xMsPermissions"> Optional and only valid if Hierarchical Namespace is enabled for the account. Sets POSIX access permissions for the file owner, the file owning group, and others. Each class may be granted read, write, or execute permission.  The sticky bit is also supported.  Both symbolic (rwxrw-rw-) and 4-digit octal notation (e.g. 0766) are supported. </param>
        /// <param name="xMsUmask"> Only valid if Hierarchical Namespace is enabled for the account. This umask restricts permission settings for file and directory, and will only be applied when default Acl does not exist in parent directory. If the umask bit has set, it means that the corresponding permission will be disabled. Otherwise the corresponding permission will be determined by the permission. A 4-digit octal notation (e.g. 0022) is supported here. If no umask was specified, a default umask - 0027 will be used. </param>
        /// <param name="xMsCacheControl"> Cache control for given resource. </param>
        /// <param name="xMsContentType"> Content type for given resource. </param>
        /// <param name="xMsContentEncoding"> Content encoding for given resource. </param>
        /// <param name="xMsContentLanguage"> Content language for given resource. </param>
        /// <param name="xMsContentDisposition"> Content disposition for given resource. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsSourceLeaseId"> A lease ID for the source path. If specified, the source path must have an active lease and the leaase ID must match. </param>
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
        public ResponseWithHeaders<RenameHeaders> Rename(int? timeout, string xMsRenameSource, string? xMsProperties, string? xMsPermissions, string? xMsUmask, string? xMsCacheControl, string? xMsContentType, string? xMsContentEncoding, string? xMsContentLanguage, string? xMsContentDisposition, string? xMsLeaseId, string? xMsSourceLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsRenameSource == null)
            {
                throw new ArgumentNullException(nameof(xMsRenameSource));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.Rename");
            scope.Start();
            try
            {
                using var message = CreateRenameRequest(timeout, xMsRenameSource, xMsProperties, xMsPermissions, xMsUmask, xMsCacheControl, xMsContentType, xMsContentEncoding, xMsContentLanguage, xMsContentDisposition, xMsLeaseId, xMsSourceLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new RenameHeaders(message.Response);
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
        internal HttpMessage CreateUndeleteRequest(int? timeout, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "undelete", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> Undelete a blob that was previously soft deleted. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<UndeleteHeaders>> UndeleteAsync(int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.Undelete");
            scope.Start();
            try
            {
                using var message = CreateUndeleteRequest(timeout, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new UndeleteHeaders(message.Response);
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
        /// <summary> Undelete a blob that was previously soft deleted. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<UndeleteHeaders> Undelete(int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.Undelete");
            scope.Start();
            try
            {
                using var message = CreateUndeleteRequest(timeout, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new UndeleteHeaders(message.Response);
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
        internal HttpMessage CreateSetHttpHeadersRequest(int? timeout, string? xMsBlobCacheControl, string? xMsBlobContentType, byte[]? xMsBlobContentMd5, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsBlobContentDisposition, string? xMsClientRequestId)
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
            if (xMsBlobContentMd5 != null)
            {
                request.Headers.Add("x-ms-blob-content-md5", xMsBlobContentMd5);
            }
            if (xMsBlobContentEncoding != null)
            {
                request.Headers.Add("x-ms-blob-content-encoding", xMsBlobContentEncoding);
            }
            if (xMsBlobContentLanguage != null)
            {
                request.Headers.Add("x-ms-blob-content-language", xMsBlobContentLanguage);
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
            if (xMsBlobContentDisposition != null)
            {
                request.Headers.Add("x-ms-blob-content-disposition", xMsBlobContentDisposition);
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
        /// <summary> The Set HTTP Headers operation sets system properties on the blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsBlobCacheControl"> Optional. Sets the blob&apos;s cache control. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentType"> Optional. Sets the blob&apos;s content type. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentMd5"> Optional. An MD5 hash of the blob content. Note that this hash is not validated, as the hashes for the individual blocks were validated when each was uploaded. </param>
        /// <param name="xMsBlobContentEncoding"> Optional. Sets the blob&apos;s content encoding. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentLanguage"> Optional. Set the blob&apos;s content language. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsBlobContentDisposition"> Optional. Sets the blob&apos;s Content-Disposition header. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<SetHttpHeadersHeaders>> SetHttpHeadersAsync(int? timeout, string? xMsBlobCacheControl, string? xMsBlobContentType, byte[]? xMsBlobContentMd5, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsBlobContentDisposition, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.SetHttpHeaders");
            scope.Start();
            try
            {
                using var message = CreateSetHttpHeadersRequest(timeout, xMsBlobCacheControl, xMsBlobContentType, xMsBlobContentMd5, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsBlobContentDisposition, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetHttpHeadersHeaders(message.Response);
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
        /// <summary> The Set HTTP Headers operation sets system properties on the blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsBlobCacheControl"> Optional. Sets the blob&apos;s cache control. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentType"> Optional. Sets the blob&apos;s content type. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentMd5"> Optional. An MD5 hash of the blob content. Note that this hash is not validated, as the hashes for the individual blocks were validated when each was uploaded. </param>
        /// <param name="xMsBlobContentEncoding"> Optional. Sets the blob&apos;s content encoding. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsBlobContentLanguage"> Optional. Set the blob&apos;s content language. If specified, this property is stored with the blob and returned with a read request. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsBlobContentDisposition"> Optional. Sets the blob&apos;s Content-Disposition header. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<SetHttpHeadersHeaders> SetHttpHeaders(int? timeout, string? xMsBlobCacheControl, string? xMsBlobContentType, byte[]? xMsBlobContentMd5, string? xMsBlobContentEncoding, string? xMsBlobContentLanguage, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsBlobContentDisposition, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.SetHttpHeaders");
            scope.Start();
            try
            {
                using var message = CreateSetHttpHeadersRequest(timeout, xMsBlobCacheControl, xMsBlobContentType, xMsBlobContentMd5, xMsBlobContentEncoding, xMsBlobContentLanguage, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsBlobContentDisposition, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetHttpHeadersHeaders(message.Response);
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
        internal HttpMessage CreateSetMetadataRequest(int? timeout, string? xMsMeta, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            if (xMsMeta != null)
            {
                request.Headers.Add("x-ms-meta", xMsMeta);
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
            request.Uri.AppendQuery("comp", "metadata", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Set Blob Metadata operation sets user-defined metadata for the specified blob as one or more name-value pairs. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<SetMetadataHeaders>> SetMetadataAsync(int? timeout, string? xMsMeta, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.SetMetadata");
            scope.Start();
            try
            {
                using var message = CreateSetMetadataRequest(timeout, xMsMeta, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetMetadataHeaders(message.Response);
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
        /// <summary> The Set Blob Metadata operation sets user-defined metadata for the specified blob as one or more name-value pairs. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<SetMetadataHeaders> SetMetadata(int? timeout, string? xMsMeta, string? xMsLeaseId, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.SetMetadata");
            scope.Start();
            try
            {
                using var message = CreateSetMetadataRequest(timeout, xMsMeta, xMsLeaseId, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetMetadataHeaders(message.Response);
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
        internal HttpMessage CreateAcquireLeaseRequest(int? timeout, long? xMsLeaseDuration, string? xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-lease-action", "acquire");
            if (xMsLeaseDuration != null)
            {
                request.Headers.Add("x-ms-lease-duration", xMsLeaseDuration.Value);
            }
            if (xMsProposedLeaseId != null)
            {
                request.Headers.Add("x-ms-proposed-lease-id", xMsProposedLeaseId);
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
            request.Uri.AppendQuery("comp", "lease", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseDuration"> Specifies the duration of the lease, in seconds, or negative one (-1) for a lease that never expires. A non-infinite lease can be between 15 and 60 seconds. A lease duration cannot be changed using renew or change. </param>
        /// <param name="xMsProposedLeaseId"> Proposed lease ID, in a GUID string format. The Blob service returns 400 (Invalid request) if the proposed lease ID is not in the correct format. See Guid Constructor (String) for a list of valid GUID string formats. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<AcquireLeaseHeaders>> AcquireLeaseAsync(int? timeout, long? xMsLeaseDuration, string? xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.AcquireLease");
            scope.Start();
            try
            {
                using var message = CreateAcquireLeaseRequest(timeout, xMsLeaseDuration, xMsProposedLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new AcquireLeaseHeaders(message.Response);
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
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseDuration"> Specifies the duration of the lease, in seconds, or negative one (-1) for a lease that never expires. A non-infinite lease can be between 15 and 60 seconds. A lease duration cannot be changed using renew or change. </param>
        /// <param name="xMsProposedLeaseId"> Proposed lease ID, in a GUID string format. The Blob service returns 400 (Invalid request) if the proposed lease ID is not in the correct format. See Guid Constructor (String) for a list of valid GUID string formats. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<AcquireLeaseHeaders> AcquireLease(int? timeout, long? xMsLeaseDuration, string? xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.AcquireLease");
            scope.Start();
            try
            {
                using var message = CreateAcquireLeaseRequest(timeout, xMsLeaseDuration, xMsProposedLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new AcquireLeaseHeaders(message.Response);
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
        internal HttpMessage CreateReleaseLeaseRequest(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-lease-action", "release");
            request.Headers.Add("x-ms-lease-id", xMsLeaseId);
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
            request.Uri.AppendQuery("comp", "lease", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<ReleaseLeaseHeaders>> ReleaseLeaseAsync(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.ReleaseLease");
            scope.Start();
            try
            {
                using var message = CreateReleaseLeaseRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new ReleaseLeaseHeaders(message.Response);
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
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<ReleaseLeaseHeaders> ReleaseLease(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.ReleaseLease");
            scope.Start();
            try
            {
                using var message = CreateReleaseLeaseRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new ReleaseLeaseHeaders(message.Response);
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
        internal HttpMessage CreateRenewLeaseRequest(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-lease-action", "renew");
            request.Headers.Add("x-ms-lease-id", xMsLeaseId);
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
            request.Uri.AppendQuery("comp", "lease", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<RenewLeaseHeaders>> RenewLeaseAsync(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.RenewLease");
            scope.Start();
            try
            {
                using var message = CreateRenewLeaseRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new RenewLeaseHeaders(message.Response);
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
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<RenewLeaseHeaders> RenewLease(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.RenewLease");
            scope.Start();
            try
            {
                using var message = CreateRenewLeaseRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new RenewLeaseHeaders(message.Response);
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
        internal HttpMessage CreateChangeLeaseRequest(int? timeout, string xMsLeaseId, string xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-lease-action", "change");
            request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            request.Headers.Add("x-ms-proposed-lease-id", xMsProposedLeaseId);
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
            request.Uri.AppendQuery("comp", "lease", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="xMsProposedLeaseId"> Proposed lease ID, in a GUID string format. The Blob service returns 400 (Invalid request) if the proposed lease ID is not in the correct format. See Guid Constructor (String) for a list of valid GUID string formats. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<ChangeLeaseHeaders>> ChangeLeaseAsync(int? timeout, string xMsLeaseId, string xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }
            if (xMsProposedLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsProposedLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.ChangeLease");
            scope.Start();
            try
            {
                using var message = CreateChangeLeaseRequest(timeout, xMsLeaseId, xMsProposedLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new ChangeLeaseHeaders(message.Response);
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
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="xMsProposedLeaseId"> Proposed lease ID, in a GUID string format. The Blob service returns 400 (Invalid request) if the proposed lease ID is not in the correct format. See Guid Constructor (String) for a list of valid GUID string formats. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<ChangeLeaseHeaders> ChangeLease(int? timeout, string xMsLeaseId, string xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }
            if (xMsProposedLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsProposedLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.ChangeLease");
            scope.Start();
            try
            {
                using var message = CreateChangeLeaseRequest(timeout, xMsLeaseId, xMsProposedLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new ChangeLeaseHeaders(message.Response);
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
        internal HttpMessage CreateBreakLeaseRequest(int? timeout, long? xMsLeaseBreakPeriod, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-lease-action", "break");
            if (xMsLeaseBreakPeriod != null)
            {
                request.Headers.Add("x-ms-lease-break-period", xMsLeaseBreakPeriod.Value);
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
            request.Uri.AppendQuery("comp", "lease", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseBreakPeriod"> For a break operation, proposed duration the lease should continue before it is broken, in seconds, between 0 and 60. This break period is only used if it is shorter than the time remaining on the lease. If longer, the time remaining on the lease is used. A new lease will not be available before the break period has expired, but the lease may be held for longer than the break period. If this header does not appear with a break operation, a fixed-duration lease breaks after the remaining lease period elapses, and an infinite lease breaks immediately. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<BreakLeaseHeaders>> BreakLeaseAsync(int? timeout, long? xMsLeaseBreakPeriod, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.BreakLease");
            scope.Start();
            try
            {
                using var message = CreateBreakLeaseRequest(timeout, xMsLeaseBreakPeriod, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new BreakLeaseHeaders(message.Response);
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
        /// <summary> [Update] The Lease Blob operation establishes and manages a lock on a blob for write and delete operations. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseBreakPeriod"> For a break operation, proposed duration the lease should continue before it is broken, in seconds, between 0 and 60. This break period is only used if it is shorter than the time remaining on the lease. If longer, the time remaining on the lease is used. A new lease will not be available before the break period has expired, but the lease may be held for longer than the break period. If this header does not appear with a break operation, a fixed-duration lease breaks after the remaining lease period elapses, and an infinite lease breaks immediately. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<BreakLeaseHeaders> BreakLease(int? timeout, long? xMsLeaseBreakPeriod, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.BreakLease");
            scope.Start();
            try
            {
                using var message = CreateBreakLeaseRequest(timeout, xMsLeaseBreakPeriod, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new BreakLeaseHeaders(message.Response);
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
        internal HttpMessage CreateCreateSnapshotRequest(int? timeout, string? xMsMeta, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsLeaseId, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            if (xMsMeta != null)
            {
                request.Headers.Add("x-ms-meta", xMsMeta);
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
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "snapshot", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Create Snapshot operation creates a read-only snapshot of a blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<CreateSnapshotHeaders>> CreateSnapshotAsync(int? timeout, string? xMsMeta, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.CreateSnapshot");
            scope.Start();
            try
            {
                using var message = CreateCreateSnapshotRequest(timeout, xMsMeta, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsLeaseId, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new CreateSnapshotHeaders(message.Response);
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
        /// <summary> The Create Snapshot operation creates a read-only snapshot of a blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsEncryptionKey"> Optional. Specifies the encryption key to use to encrypt the data provided in the request. If not specified, encryption is performed with the root account encryption key.  For more information, see Encryption at Rest for Azure Storage Services. </param>
        /// <param name="xMsEncryptionKeySha256"> The SHA-256 hash of the provided encryption key. Must be provided if the x-ms-encryption-key header is provided. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<CreateSnapshotHeaders> CreateSnapshot(int? timeout, string? xMsMeta, string? xMsEncryptionKey, string? xMsEncryptionKeySha256, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.CreateSnapshot");
            scope.Start();
            try
            {
                using var message = CreateCreateSnapshotRequest(timeout, xMsMeta, xMsEncryptionKey, xMsEncryptionKeySha256, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsLeaseId, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 201:
                        var headers = new CreateSnapshotHeaders(message.Response);
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
        internal HttpMessage CreateStartCopyFromUriRequest(int? timeout, string? xMsMeta, AccessTier? xMsAccessTier, RehydratePriority? xMsRehydratePriority, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, Uri xMsCopySource, string? xMsLeaseId, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            if (xMsMeta != null)
            {
                request.Headers.Add("x-ms-meta", xMsMeta);
            }
            if (xMsAccessTier != null)
            {
                request.Headers.Add("x-ms-access-tier", xMsAccessTier.Value);
            }
            if (xMsRehydratePriority != null)
            {
                request.Headers.Add("x-ms-rehydrate-priority", xMsRehydratePriority.Value);
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
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
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
        /// <summary> The Start Copy From URL operation copies a blob or an internet resource to a new blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the blob. </param>
        /// <param name="xMsRehydratePriority"> Optional: Indicates the priority with which to rehydrate an archived blob. </param>
        /// <param name="xMsSourceIfModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="xMsSourceIfNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsCopySource"> Specifies the name of the source page blob snapshot. This value is a URL of up to 2 KB in length that specifies a page blob snapshot. The value should be URL-encoded as it would appear in a request URI. The source blob must either be public or must be authenticated via a shared access signature. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<StartCopyFromUriHeaders>> StartCopyFromUriAsync(int? timeout, string? xMsMeta, AccessTier? xMsAccessTier, RehydratePriority? xMsRehydratePriority, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, Uri xMsCopySource, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.StartCopyFromUri");
            scope.Start();
            try
            {
                using var message = CreateStartCopyFromUriRequest(timeout, xMsMeta, xMsAccessTier, xMsRehydratePriority, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsCopySource, xMsLeaseId, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new StartCopyFromUriHeaders(message.Response);
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
        /// <summary> The Start Copy From URL operation copies a blob or an internet resource to a new blob. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the blob. </param>
        /// <param name="xMsRehydratePriority"> Optional: Indicates the priority with which to rehydrate an archived blob. </param>
        /// <param name="xMsSourceIfModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="xMsSourceIfNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsCopySource"> Specifies the name of the source page blob snapshot. This value is a URL of up to 2 KB in length that specifies a page blob snapshot. The value should be URL-encoded as it would appear in a request URI. The source blob must either be public or must be authenticated via a shared access signature. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<StartCopyFromUriHeaders> StartCopyFromUri(int? timeout, string? xMsMeta, AccessTier? xMsAccessTier, RehydratePriority? xMsRehydratePriority, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, Uri xMsCopySource, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.StartCopyFromUri");
            scope.Start();
            try
            {
                using var message = CreateStartCopyFromUriRequest(timeout, xMsMeta, xMsAccessTier, xMsRehydratePriority, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsCopySource, xMsLeaseId, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new StartCopyFromUriHeaders(message.Response);
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
        internal HttpMessage CreateCopyFromUriRequest(int? timeout, string? xMsMeta, AccessTier? xMsAccessTier, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, Uri xMsCopySource, string? xMsLeaseId, string? xMsClientRequestId, byte[]? xMsSourceContentMd5)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-requires-sync", "true");
            if (xMsMeta != null)
            {
                request.Headers.Add("x-ms-meta", xMsMeta);
            }
            if (xMsAccessTier != null)
            {
                request.Headers.Add("x-ms-access-tier", xMsAccessTier.Value);
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
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            if (xMsSourceContentMd5 != null)
            {
                request.Headers.Add("x-ms-source-content-md5", xMsSourceContentMd5);
            }
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Copy From URL operation copies a blob or an internet resource to a new blob. It will not return a response until the copy is complete. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the blob. </param>
        /// <param name="xMsSourceIfModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="xMsSourceIfNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsCopySource"> Specifies the name of the source page blob snapshot. This value is a URL of up to 2 KB in length that specifies a page blob snapshot. The value should be URL-encoded as it would appear in a request URI. The source blob must either be public or must be authenticated via a shared access signature. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="xMsSourceContentMd5"> Specify the md5 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<CopyFromUriHeaders>> CopyFromUriAsync(int? timeout, string? xMsMeta, AccessTier? xMsAccessTier, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, Uri xMsCopySource, string? xMsLeaseId, string? xMsClientRequestId, byte[]? xMsSourceContentMd5, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.CopyFromUri");
            scope.Start();
            try
            {
                using var message = CreateCopyFromUriRequest(timeout, xMsMeta, xMsAccessTier, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsCopySource, xMsLeaseId, xMsClientRequestId, xMsSourceContentMd5);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new CopyFromUriHeaders(message.Response);
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
        /// <summary> The Copy From URL operation copies a blob or an internet resource to a new blob. It will not return a response until the copy is complete. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsAccessTier"> Optional. Indicates the tier to be set on the blob. </param>
        /// <param name="xMsSourceIfModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsSourceIfMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="xMsSourceIfNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsCopySource"> Specifies the name of the source page blob snapshot. This value is a URL of up to 2 KB in length that specifies a page blob snapshot. The value should be URL-encoded as it would appear in a request URI. The source blob must either be public or must be authenticated via a shared access signature. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="xMsSourceContentMd5"> Specify the md5 calculated for the range of bytes that must be read from the copy source. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<CopyFromUriHeaders> CopyFromUri(int? timeout, string? xMsMeta, AccessTier? xMsAccessTier, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, Uri xMsCopySource, string? xMsLeaseId, string? xMsClientRequestId, byte[]? xMsSourceContentMd5, CancellationToken cancellationToken = default)
        {
            if (xMsCopySource == null)
            {
                throw new ArgumentNullException(nameof(xMsCopySource));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.CopyFromUri");
            scope.Start();
            try
            {
                using var message = CreateCopyFromUriRequest(timeout, xMsMeta, xMsAccessTier, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsCopySource, xMsLeaseId, xMsClientRequestId, xMsSourceContentMd5);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new CopyFromUriHeaders(message.Response);
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
        internal HttpMessage CreateAbortCopyFromUriRequest(string copyid, int? timeout, string? xMsLeaseId, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-copy-action", "abort");
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "copy", true);
            request.Uri.AppendQuery("copyid", copyid, true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Abort Copy From URL operation aborts a pending Copy From URL operation, and leaves a destination blob with zero length and full metadata. </summary>
        /// <param name="copyid"> The copy identifier provided in the x-ms-copy-id header of the original Copy Blob operation. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<AbortCopyFromUriHeaders>> AbortCopyFromUriAsync(string copyid, int? timeout, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (copyid == null)
            {
                throw new ArgumentNullException(nameof(copyid));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.AbortCopyFromUri");
            scope.Start();
            try
            {
                using var message = CreateAbortCopyFromUriRequest(copyid, timeout, xMsLeaseId, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 204:
                        var headers = new AbortCopyFromUriHeaders(message.Response);
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
        /// <summary> The Abort Copy From URL operation aborts a pending Copy From URL operation, and leaves a destination blob with zero length and full metadata. </summary>
        /// <param name="copyid"> The copy identifier provided in the x-ms-copy-id header of the original Copy Blob operation. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<AbortCopyFromUriHeaders> AbortCopyFromUri(string copyid, int? timeout, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (copyid == null)
            {
                throw new ArgumentNullException(nameof(copyid));
            }

            using var scope = clientDiagnostics.CreateScope("BlobOperations.AbortCopyFromUri");
            scope.Start();
            try
            {
                using var message = CreateAbortCopyFromUriRequest(copyid, timeout, xMsLeaseId, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 204:
                        var headers = new AbortCopyFromUriHeaders(message.Response);
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
        internal HttpMessage CreateSetAccessTierRequest(int? timeout, AccessTier xMsAccessTier, RehydratePriority? xMsRehydratePriority, string? xMsClientRequestId, string? xMsLeaseId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("blob", false);
            request.Headers.Add("x-ms-access-tier", xMsAccessTier);
            if (xMsRehydratePriority != null)
            {
                request.Headers.Add("x-ms-rehydrate-priority", xMsRehydratePriority.Value);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            request.Uri.AppendQuery("comp", "tier", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Set Tier operation sets the tier on a blob. The operation is allowed on a page blob in a premium storage account and on a block blob in a blob storage account (locally redundant storage only). A premium page blob&apos;s tier determines the allowed size, IOPS, and bandwidth of the blob. A block blob&apos;s tier determines Hot/Cool/Archive storage type. This operation does not update the blob&apos;s ETag. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsAccessTier"> Indicates the tier to be set on the blob. </param>
        /// <param name="xMsRehydratePriority"> Optional: Indicates the priority with which to rehydrate an archived blob. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<SetAccessTierHeaders>> SetAccessTierAsync(int? timeout, AccessTier xMsAccessTier, RehydratePriority? xMsRehydratePriority, string? xMsClientRequestId, string? xMsLeaseId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.SetAccessTier");
            scope.Start();
            try
            {
                using var message = CreateSetAccessTierRequest(timeout, xMsAccessTier, xMsRehydratePriority, xMsClientRequestId, xMsLeaseId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetAccessTierHeaders(message.Response);
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
        /// <summary> The Set Tier operation sets the tier on a blob. The operation is allowed on a page blob in a premium storage account and on a block blob in a blob storage account (locally redundant storage only). A premium page blob&apos;s tier determines the allowed size, IOPS, and bandwidth of the blob. A block blob&apos;s tier determines Hot/Cool/Archive storage type. This operation does not update the blob&apos;s ETag. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsAccessTier"> Indicates the tier to be set on the blob. </param>
        /// <param name="xMsRehydratePriority"> Optional: Indicates the priority with which to rehydrate an archived blob. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<SetAccessTierHeaders> SetAccessTier(int? timeout, AccessTier xMsAccessTier, RehydratePriority? xMsRehydratePriority, string? xMsClientRequestId, string? xMsLeaseId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("BlobOperations.SetAccessTier");
            scope.Start();
            try
            {
                using var message = CreateSetAccessTierRequest(timeout, xMsAccessTier, xMsRehydratePriority, xMsClientRequestId, xMsLeaseId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetAccessTierHeaders(message.Response);
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
