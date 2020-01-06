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
    internal partial class DirectoryOperations
    {
        private string url;
        private string xMsVersion;
        private PathRenameMode? mode;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of DirectoryOperations. </summary>
        public DirectoryOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string url, string xMsVersion, PathRenameMode? mode)
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
        internal HttpMessage CreateCreateRequest(int? timeout, string? xMsProperties, string? xMsPermissions, string? xMsUmask, string? xMsCacheControl, string? xMsContentType, string? xMsContentEncoding, string? xMsContentLanguage, string? xMsContentDisposition, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("filesystem", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("path", false);
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
            request.Uri.AppendQuery("resource", "directory", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> Create a directory. By default, the destination is overwritten and if the destination already exists and has a lease the lease is broken. This operation supports conditional HTTP requests.  For more information, see [Specifying Conditional Headers for Blob Service Operations](https://docs.microsoft.com/en-us/rest/api/storageservices/specifying-conditional-headers-for-blob-service-operations).  To fail if the destination already exists, use a conditional request with If-None-Match: &quot;*&quot;. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsProperties"> Optional.  User-defined properties to be stored with the file or directory, in the format of a comma-separated list of name and value pairs &quot;n1=v1, n2=v2, ...&quot;, where each value is base64 encoded. </param>
        /// <param name="xMsPermissions"> Optional and only valid if Hierarchical Namespace is enabled for the account. Sets POSIX access permissions for the file owner, the file owning group, and others. Each class may be granted read, write, or execute permission.  The sticky bit is also supported.  Both symbolic (rwxrw-rw-) and 4-digit octal notation (e.g. 0766) are supported. </param>
        /// <param name="xMsUmask"> Only valid if Hierarchical Namespace is enabled for the account. This umask restricts permission settings for file and directory, and will only be applied when default Acl does not exist in parent directory. If the umask bit has set, it means that the corresponding permission will be disabled. Otherwise the corresponding permission will be determined by the permission. A 4-digit octal notation (e.g. 0022) is supported here. If no umask was specified, a default umask - 0027 will be used. </param>
        /// <param name="xMsCacheControl"> Cache control for given resource. </param>
        /// <param name="xMsContentType"> Content type for given resource. </param>
        /// <param name="xMsContentEncoding"> Content encoding for given resource. </param>
        /// <param name="xMsContentLanguage"> Content language for given resource. </param>
        /// <param name="xMsContentDisposition"> Content disposition for given resource. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<CreateHeaders>> CreateAsync(int? timeout, string? xMsProperties, string? xMsPermissions, string? xMsUmask, string? xMsCacheControl, string? xMsContentType, string? xMsContentEncoding, string? xMsContentLanguage, string? xMsContentDisposition, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.Create");
            scope.Start();
            try
            {
                using var message = CreateCreateRequest(timeout, xMsProperties, xMsPermissions, xMsUmask, xMsCacheControl, xMsContentType, xMsContentEncoding, xMsContentLanguage, xMsContentDisposition, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
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
        /// <summary> Create a directory. By default, the destination is overwritten and if the destination already exists and has a lease the lease is broken. This operation supports conditional HTTP requests.  For more information, see [Specifying Conditional Headers for Blob Service Operations](https://docs.microsoft.com/en-us/rest/api/storageservices/specifying-conditional-headers-for-blob-service-operations).  To fail if the destination already exists, use a conditional request with If-None-Match: &quot;*&quot;. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsProperties"> Optional.  User-defined properties to be stored with the file or directory, in the format of a comma-separated list of name and value pairs &quot;n1=v1, n2=v2, ...&quot;, where each value is base64 encoded. </param>
        /// <param name="xMsPermissions"> Optional and only valid if Hierarchical Namespace is enabled for the account. Sets POSIX access permissions for the file owner, the file owning group, and others. Each class may be granted read, write, or execute permission.  The sticky bit is also supported.  Both symbolic (rwxrw-rw-) and 4-digit octal notation (e.g. 0766) are supported. </param>
        /// <param name="xMsUmask"> Only valid if Hierarchical Namespace is enabled for the account. This umask restricts permission settings for file and directory, and will only be applied when default Acl does not exist in parent directory. If the umask bit has set, it means that the corresponding permission will be disabled. Otherwise the corresponding permission will be determined by the permission. A 4-digit octal notation (e.g. 0022) is supported here. If no umask was specified, a default umask - 0027 will be used. </param>
        /// <param name="xMsCacheControl"> Cache control for given resource. </param>
        /// <param name="xMsContentType"> Content type for given resource. </param>
        /// <param name="xMsContentEncoding"> Content encoding for given resource. </param>
        /// <param name="xMsContentLanguage"> Content language for given resource. </param>
        /// <param name="xMsContentDisposition"> Content disposition for given resource. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<CreateHeaders> Create(int? timeout, string? xMsProperties, string? xMsPermissions, string? xMsUmask, string? xMsCacheControl, string? xMsContentType, string? xMsContentEncoding, string? xMsContentLanguage, string? xMsContentDisposition, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.Create");
            scope.Start();
            try
            {
                using var message = CreateCreateRequest(timeout, xMsProperties, xMsPermissions, xMsUmask, xMsCacheControl, xMsContentType, xMsContentEncoding, xMsContentLanguage, xMsContentDisposition, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
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
        internal HttpMessage CreateRenameRequest(int? timeout, string? continuation, string xMsRenameSource, string? xMsProperties, string? xMsPermissions, string? xMsUmask, string? xMsCacheControl, string? xMsContentType, string? xMsContentEncoding, string? xMsContentLanguage, string? xMsContentDisposition, string? xMsLeaseId, string? xMsSourceLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId)
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
            if (continuation != null)
            {
                request.Uri.AppendQuery("continuation", continuation, true);
            }
            if (mode != null)
            {
                request.Uri.AppendQuery("mode", mode.Value, true);
            }
            return message;
        }
        /// <summary> Rename a directory. By default, the destination is overwritten and if the destination already exists and has a lease the lease is broken. This operation supports conditional HTTP requests. For more information, see [Specifying Conditional Headers for Blob Service Operations](https://docs.microsoft.com/en-us/rest/api/storageservices/specifying-conditional-headers-for-blob-service-operations). To fail if the destination already exists, use a conditional request with If-None-Match: &quot;*&quot;. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="continuation"> When renaming a directory, the number of paths that are renamed with each invocation is limited.  If the number of paths to be renamed exceeds this limit, a continuation token is returned in this response header.  When a continuation token is returned in the response, it must be specified in a subsequent invocation of the rename operation to continue renaming the directory. </param>
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
        public async ValueTask<ResponseWithHeaders<RenameHeaders>> RenameAsync(int? timeout, string? continuation, string xMsRenameSource, string? xMsProperties, string? xMsPermissions, string? xMsUmask, string? xMsCacheControl, string? xMsContentType, string? xMsContentEncoding, string? xMsContentLanguage, string? xMsContentDisposition, string? xMsLeaseId, string? xMsSourceLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsRenameSource == null)
            {
                throw new ArgumentNullException(nameof(xMsRenameSource));
            }

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.Rename");
            scope.Start();
            try
            {
                using var message = CreateRenameRequest(timeout, continuation, xMsRenameSource, xMsProperties, xMsPermissions, xMsUmask, xMsCacheControl, xMsContentType, xMsContentEncoding, xMsContentLanguage, xMsContentDisposition, xMsLeaseId, xMsSourceLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
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
        /// <summary> Rename a directory. By default, the destination is overwritten and if the destination already exists and has a lease the lease is broken. This operation supports conditional HTTP requests. For more information, see [Specifying Conditional Headers for Blob Service Operations](https://docs.microsoft.com/en-us/rest/api/storageservices/specifying-conditional-headers-for-blob-service-operations). To fail if the destination already exists, use a conditional request with If-None-Match: &quot;*&quot;. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="continuation"> When renaming a directory, the number of paths that are renamed with each invocation is limited.  If the number of paths to be renamed exceeds this limit, a continuation token is returned in this response header.  When a continuation token is returned in the response, it must be specified in a subsequent invocation of the rename operation to continue renaming the directory. </param>
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
        public ResponseWithHeaders<RenameHeaders> Rename(int? timeout, string? continuation, string xMsRenameSource, string? xMsProperties, string? xMsPermissions, string? xMsUmask, string? xMsCacheControl, string? xMsContentType, string? xMsContentEncoding, string? xMsContentLanguage, string? xMsContentDisposition, string? xMsLeaseId, string? xMsSourceLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, DateTimeOffset? xMsSourceIfModifiedSince, DateTimeOffset? xMsSourceIfUnmodifiedSince, string? xMsSourceIfMatch, string? xMsSourceIfNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsRenameSource == null)
            {
                throw new ArgumentNullException(nameof(xMsRenameSource));
            }

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.Rename");
            scope.Start();
            try
            {
                using var message = CreateRenameRequest(timeout, continuation, xMsRenameSource, xMsProperties, xMsPermissions, xMsUmask, xMsCacheControl, xMsContentType, xMsContentEncoding, xMsContentLanguage, xMsContentDisposition, xMsLeaseId, xMsSourceLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsSourceIfModifiedSince, xMsSourceIfUnmodifiedSince, xMsSourceIfMatch, xMsSourceIfNoneMatch, xMsClientRequestId);
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
        internal HttpMessage CreateDeleteRequest(int? timeout, bool recursive, string? continuation, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Delete;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("filesystem", false);
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("path", false);
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
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            request.Uri.AppendQuery("recursive", recursive, true);
            if (continuation != null)
            {
                request.Uri.AppendQuery("continuation", continuation, true);
            }
            return message;
        }
        /// <summary> Deletes the directory. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="recursive"> If &quot;true&quot;, all paths beneath the directory will be deleted. If &quot;false&quot; and the directory is non-empty, an error occurs. </param>
        /// <param name="continuation"> When renaming a directory, the number of paths that are renamed with each invocation is limited.  If the number of paths to be renamed exceeds this limit, a continuation token is returned in this response header.  When a continuation token is returned in the response, it must be specified in a subsequent invocation of the rename operation to continue renaming the directory. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<DeleteHeaders>> DeleteAsync(int? timeout, bool recursive, string? continuation, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.Delete");
            scope.Start();
            try
            {
                using var message = CreateDeleteRequest(timeout, recursive, continuation, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
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
        /// <summary> Deletes the directory. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="recursive"> If &quot;true&quot;, all paths beneath the directory will be deleted. If &quot;false&quot; and the directory is non-empty, an error occurs. </param>
        /// <param name="continuation"> When renaming a directory, the number of paths that are renamed with each invocation is limited.  If the number of paths to be renamed exceeds this limit, a continuation token is returned in this response header.  When a continuation token is returned in the response, it must be specified in a subsequent invocation of the rename operation to continue renaming the directory. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="ifMatch"> Specify an ETag value to operate only on blobs with a matching value. </param>
        /// <param name="ifNoneMatch"> Specify an ETag value to operate only on blobs without a matching value. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<DeleteHeaders> Delete(int? timeout, bool recursive, string? continuation, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? ifMatch, string? ifNoneMatch, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.Delete");
            scope.Start();
            try
            {
                using var message = CreateDeleteRequest(timeout, recursive, continuation, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, ifMatch, ifNoneMatch, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
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
        /// <summary> Set the owner, group, permissions, or access control list for a directory. </summary>
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

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.SetAccessControl");
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
        /// <summary> Set the owner, group, permissions, or access control list for a directory. </summary>
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

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.SetAccessControl");
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
        /// <summary> Get the owner, group, permissions, or access control list for a directory. </summary>
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

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.GetAccessControl");
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
        /// <summary> Get the owner, group, permissions, or access control list for a directory. </summary>
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

            using var scope = clientDiagnostics.CreateScope("DirectoryOperations.GetAccessControl");
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
    }
}
