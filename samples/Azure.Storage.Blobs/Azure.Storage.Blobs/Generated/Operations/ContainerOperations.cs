// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Storage.Blobs.Models.V20190202;

namespace Azure.Storage.Blobs
{
    internal partial class ContainerOperations
    {
        private string url;
        private string xMsVersion;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of ContainerOperations. </summary>
        public ContainerOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string url, string xMsVersion)
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
        internal HttpMessage CreateCreateRequest(int? timeout, string? xMsMeta, PublicAccessType xMsBlobPublicAccess, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            if (xMsMeta != null)
            {
                request.Headers.Add("x-ms-meta", xMsMeta);
            }
            request.Headers.Add("x-ms-blob-public-access", xMsBlobPublicAccess);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("restype", "container", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> creates a new container under the specified account. If the container with the same name already exists, the operation fails. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsBlobPublicAccess"> Specifies whether data in the container may be accessed publicly and the level of access. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<CreateHeaders>> CreateAsync(int? timeout, string? xMsMeta, PublicAccessType xMsBlobPublicAccess, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.Create");
            scope.Start();
            try
            {
                using var message = CreateCreateRequest(timeout, xMsMeta, xMsBlobPublicAccess, xMsClientRequestId);
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
        /// <summary> creates a new container under the specified account. If the container with the same name already exists, the operation fails. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="xMsBlobPublicAccess"> Specifies whether data in the container may be accessed publicly and the level of access. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<CreateHeaders> Create(int? timeout, string? xMsMeta, PublicAccessType xMsBlobPublicAccess, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.Create");
            scope.Start();
            try
            {
                using var message = CreateCreateRequest(timeout, xMsMeta, xMsBlobPublicAccess, xMsClientRequestId);
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
        internal HttpMessage CreateGetPropertiesRequest(int? timeout, string? xMsLeaseId, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("restype", "container", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> returns all user-defined metadata and system properties for the specified container. The data returned does not include the container&apos;s list of blobs. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<GetPropertiesHeaders>> GetPropertiesAsync(int? timeout, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.GetProperties");
            scope.Start();
            try
            {
                using var message = CreateGetPropertiesRequest(timeout, xMsLeaseId, xMsClientRequestId);
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
        /// <summary> returns all user-defined metadata and system properties for the specified container. The data returned does not include the container&apos;s list of blobs. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<GetPropertiesHeaders> GetProperties(int? timeout, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.GetProperties");
            scope.Start();
            try
            {
                using var message = CreateGetPropertiesRequest(timeout, xMsLeaseId, xMsClientRequestId);
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
        internal HttpMessage CreateDeleteRequest(int? timeout, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Delete;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
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
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("restype", "container", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> operation marks the specified container for deletion. The container and any blobs contained within it are later deleted during garbage collection. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<DeleteHeaders>> DeleteAsync(int? timeout, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.Delete");
            scope.Start();
            try
            {
                using var message = CreateDeleteRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        /// <summary> operation marks the specified container for deletion. The container and any blobs contained within it are later deleted during garbage collection. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<DeleteHeaders> Delete(int? timeout, string? xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.Delete");
            scope.Start();
            try
            {
                using var message = CreateDeleteRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        internal HttpMessage CreateSetMetadataRequest(int? timeout, string? xMsLeaseId, string? xMsMeta, DateTimeOffset? ifModifiedSince, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            if (xMsMeta != null)
            {
                request.Headers.Add("x-ms-meta", xMsMeta);
            }
            if (ifModifiedSince != null)
            {
                request.Headers.Add("If-Modified-Since", ifModifiedSince.Value, "R");
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("restype", "container", true);
            request.Uri.AppendQuery("comp", "metadata", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> operation sets one or more user-defined name-value pairs for the specified container. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<SetMetadataHeaders>> SetMetadataAsync(int? timeout, string? xMsLeaseId, string? xMsMeta, DateTimeOffset? ifModifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.SetMetadata");
            scope.Start();
            try
            {
                using var message = CreateSetMetadataRequest(timeout, xMsLeaseId, xMsMeta, ifModifiedSince, xMsClientRequestId);
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
        /// <summary> operation sets one or more user-defined name-value pairs for the specified container. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsMeta"> Optional. Specifies a user-defined name-value pair associated with the blob. If no name-value pairs are specified, the operation will copy the metadata from the source blob or file to the destination blob. If one or more name-value pairs are specified, the destination blob is created with the specified metadata, and metadata is not copied from the source blob or file. Note that beginning with version 2009-09-19, metadata names must adhere to the naming rules for C# identifiers. See Naming and Referencing Containers, Blobs, and Metadata for more information. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<SetMetadataHeaders> SetMetadata(int? timeout, string? xMsLeaseId, string? xMsMeta, DateTimeOffset? ifModifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.SetMetadata");
            scope.Start();
            try
            {
                using var message = CreateSetMetadataRequest(timeout, xMsLeaseId, xMsMeta, ifModifiedSince, xMsClientRequestId);
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
        internal HttpMessage CreateGetAccessPolicyRequest(int? timeout, string? xMsLeaseId, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("restype", "container", true);
            request.Uri.AppendQuery("comp", "acl", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> gets the permissions for the specified container. The permissions indicate whether container data may be accessed publicly. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<ICollection<BlobSignedIdentifier>, GetAccessPolicyHeaders>> GetAccessPolicyAsync(int? timeout, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.GetAccessPolicy");
            scope.Start();
            try
            {
                using var message = CreateGetAccessPolicyRequest(timeout, xMsLeaseId, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            ICollection<BlobSignedIdentifier> value = default;
                            var signedIdentifiers = document.Element("SignedIdentifiers");
                            if (signedIdentifiers != null)
                            {
                                value = new List<BlobSignedIdentifier>();
                                foreach (var e in signedIdentifiers.Elements("SignedIdentifier"))
                                {
                                    BlobSignedIdentifier value0 = default;
                                    value0 = BlobSignedIdentifier.DeserializeBlobSignedIdentifier(e);
                                    value.Add(value0);
                                }
                            }
                            var headers = new GetAccessPolicyHeaders(message.Response);
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
        /// <summary> gets the permissions for the specified container. The permissions indicate whether container data may be accessed publicly. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<ICollection<BlobSignedIdentifier>, GetAccessPolicyHeaders> GetAccessPolicy(int? timeout, string? xMsLeaseId, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.GetAccessPolicy");
            scope.Start();
            try
            {
                using var message = CreateGetAccessPolicyRequest(timeout, xMsLeaseId, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            ICollection<BlobSignedIdentifier> value = default;
                            var signedIdentifiers = document.Element("SignedIdentifiers");
                            if (signedIdentifiers != null)
                            {
                                value = new List<BlobSignedIdentifier>();
                                foreach (var e in signedIdentifiers.Elements("SignedIdentifier"))
                                {
                                    BlobSignedIdentifier value0 = default;
                                    value0 = BlobSignedIdentifier.DeserializeBlobSignedIdentifier(e);
                                    value.Add(value0);
                                }
                            }
                            var headers = new GetAccessPolicyHeaders(message.Response);
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
        internal HttpMessage CreateSetAccessPolicyRequest(int? timeout, string? xMsLeaseId, PublicAccessType xMsBlobPublicAccess, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, IEnumerable<BlobSignedIdentifier>? permissions)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            if (xMsLeaseId != null)
            {
                request.Headers.Add("x-ms-lease-id", xMsLeaseId);
            }
            request.Headers.Add("x-ms-blob-public-access", xMsBlobPublicAccess);
            if (ifModifiedSince != null)
            {
                request.Headers.Add("If-Modified-Since", ifModifiedSince.Value, "R");
            }
            if (ifUnmodifiedSince != null)
            {
                request.Headers.Add("If-Unmodified-Since", ifUnmodifiedSince.Value, "R");
            }
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Headers.Add("Content-Type", "application/xml");
            request.Uri.AppendQuery("restype", "container", true);
            request.Uri.AppendQuery("comp", "acl", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            using var content = new XmlWriterContent();
            content.XmlWriter.WriteStartElement("SignedIdentifiers");
            foreach (var item in permissions)
            {
                content.XmlWriter.WriteObjectValue(item, "SignedIdentifier");
            }
            content.XmlWriter.WriteEndElement();
            request.Content = content;
            return message;
        }
        /// <summary> sets the permissions for the specified container. The permissions indicate whether blobs in a container may be accessed publicly. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobPublicAccess"> Specifies whether data in the container may be accessed publicly and the level of access. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="permissions"> the acls for the container. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<SetAccessPolicyHeaders>> SetAccessPolicyAsync(int? timeout, string? xMsLeaseId, PublicAccessType xMsBlobPublicAccess, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, IEnumerable<BlobSignedIdentifier>? permissions, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.SetAccessPolicy");
            scope.Start();
            try
            {
                using var message = CreateSetAccessPolicyRequest(timeout, xMsLeaseId, xMsBlobPublicAccess, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId, permissions);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetAccessPolicyHeaders(message.Response);
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
        /// <summary> sets the permissions for the specified container. The permissions indicate whether blobs in a container may be accessed publicly. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> If specified, the operation only succeeds if the resource&apos;s lease is active and matches this ID. </param>
        /// <param name="xMsBlobPublicAccess"> Specifies whether data in the container may be accessed publicly and the level of access. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="permissions"> the acls for the container. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<SetAccessPolicyHeaders> SetAccessPolicy(int? timeout, string? xMsLeaseId, PublicAccessType xMsBlobPublicAccess, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, IEnumerable<BlobSignedIdentifier>? permissions, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.SetAccessPolicy");
            scope.Start();
            try
            {
                using var message = CreateSetAccessPolicyRequest(timeout, xMsLeaseId, xMsBlobPublicAccess, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId, permissions);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new SetAccessPolicyHeaders(message.Response);
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
        internal HttpMessage CreateAcquireLeaseRequest(int? timeout, long? xMsLeaseDuration, string? xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
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
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "lease", true);
            request.Uri.AppendQuery("restype", "container", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseDuration"> Specifies the duration of the lease, in seconds, or negative one (-1) for a lease that never expires. A non-infinite lease can be between 15 and 60 seconds. A lease duration cannot be changed using renew or change. </param>
        /// <param name="xMsProposedLeaseId"> Proposed lease ID, in a GUID string format. The Blob service returns 400 (Invalid request) if the proposed lease ID is not in the correct format. See Guid Constructor (String) for a list of valid GUID string formats. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<AcquireLeaseHeaders>> AcquireLeaseAsync(int? timeout, long? xMsLeaseDuration, string? xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.AcquireLease");
            scope.Start();
            try
            {
                using var message = CreateAcquireLeaseRequest(timeout, xMsLeaseDuration, xMsProposedLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseDuration"> Specifies the duration of the lease, in seconds, or negative one (-1) for a lease that never expires. A non-infinite lease can be between 15 and 60 seconds. A lease duration cannot be changed using renew or change. </param>
        /// <param name="xMsProposedLeaseId"> Proposed lease ID, in a GUID string format. The Blob service returns 400 (Invalid request) if the proposed lease ID is not in the correct format. See Guid Constructor (String) for a list of valid GUID string formats. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<AcquireLeaseHeaders> AcquireLease(int? timeout, long? xMsLeaseDuration, string? xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.AcquireLease");
            scope.Start();
            try
            {
                using var message = CreateAcquireLeaseRequest(timeout, xMsLeaseDuration, xMsProposedLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        internal HttpMessage CreateReleaseLeaseRequest(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
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
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "lease", true);
            request.Uri.AppendQuery("restype", "container", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<ReleaseLeaseHeaders>> ReleaseLeaseAsync(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.ReleaseLease");
            scope.Start();
            try
            {
                using var message = CreateReleaseLeaseRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<ReleaseLeaseHeaders> ReleaseLease(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.ReleaseLease");
            scope.Start();
            try
            {
                using var message = CreateReleaseLeaseRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        internal HttpMessage CreateRenewLeaseRequest(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
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
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "lease", true);
            request.Uri.AppendQuery("restype", "container", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<RenewLeaseHeaders>> RenewLeaseAsync(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.RenewLease");
            scope.Start();
            try
            {
                using var message = CreateRenewLeaseRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<RenewLeaseHeaders> RenewLease(int? timeout, string xMsLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.RenewLease");
            scope.Start();
            try
            {
                using var message = CreateRenewLeaseRequest(timeout, xMsLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        internal HttpMessage CreateBreakLeaseRequest(int? timeout, long? xMsLeaseBreakPeriod, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
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
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "lease", true);
            request.Uri.AppendQuery("restype", "container", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseBreakPeriod"> For a break operation, proposed duration the lease should continue before it is broken, in seconds, between 0 and 60. This break period is only used if it is shorter than the time remaining on the lease. If longer, the time remaining on the lease is used. A new lease will not be available before the break period has expired, but the lease may be held for longer than the break period. If this header does not appear with a break operation, a fixed-duration lease breaks after the remaining lease period elapses, and an infinite lease breaks immediately. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<BreakLeaseHeaders>> BreakLeaseAsync(int? timeout, long? xMsLeaseBreakPeriod, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.BreakLease");
            scope.Start();
            try
            {
                using var message = CreateBreakLeaseRequest(timeout, xMsLeaseBreakPeriod, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseBreakPeriod"> For a break operation, proposed duration the lease should continue before it is broken, in seconds, between 0 and 60. This break period is only used if it is shorter than the time remaining on the lease. If longer, the time remaining on the lease is used. A new lease will not be available before the break period has expired, but the lease may be held for longer than the break period. If this header does not appear with a break operation, a fixed-duration lease breaks after the remaining lease period elapses, and an infinite lease breaks immediately. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<BreakLeaseHeaders> BreakLease(int? timeout, long? xMsLeaseBreakPeriod, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.BreakLease");
            scope.Start();
            try
            {
                using var message = CreateBreakLeaseRequest(timeout, xMsLeaseBreakPeriod, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        internal HttpMessage CreateChangeLeaseRequest(int? timeout, string xMsLeaseId, string xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
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
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "lease", true);
            request.Uri.AppendQuery("restype", "container", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="xMsProposedLeaseId"> Proposed lease ID, in a GUID string format. The Blob service returns 400 (Invalid request) if the proposed lease ID is not in the correct format. See Guid Constructor (String) for a list of valid GUID string formats. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<ChangeLeaseHeaders>> ChangeLeaseAsync(int? timeout, string xMsLeaseId, string xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }
            if (xMsProposedLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsProposedLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.ChangeLease");
            scope.Start();
            try
            {
                using var message = CreateChangeLeaseRequest(timeout, xMsLeaseId, xMsProposedLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        /// <summary> [Update] establishes and manages a lock on a container for delete operations. The lock duration can be 15 to 60 seconds, or can be infinite. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsLeaseId"> Specifies the current lease ID on the resource. </param>
        /// <param name="xMsProposedLeaseId"> Proposed lease ID, in a GUID string format. The Blob service returns 400 (Invalid request) if the proposed lease ID is not in the correct format. See Guid Constructor (String) for a list of valid GUID string formats. </param>
        /// <param name="ifModifiedSince"> Specify this header value to operate only on a blob if it has been modified since the specified date/time. </param>
        /// <param name="ifUnmodifiedSince"> Specify this header value to operate only on a blob if it has not been modified since the specified date/time. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<ChangeLeaseHeaders> ChangeLease(int? timeout, string xMsLeaseId, string xMsProposedLeaseId, DateTimeOffset? ifModifiedSince, DateTimeOffset? ifUnmodifiedSince, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (xMsLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsLeaseId));
            }
            if (xMsProposedLeaseId == null)
            {
                throw new ArgumentNullException(nameof(xMsProposedLeaseId));
            }

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.ChangeLease");
            scope.Start();
            try
            {
                using var message = CreateChangeLeaseRequest(timeout, xMsLeaseId, xMsProposedLeaseId, ifModifiedSince, ifUnmodifiedSince, xMsClientRequestId);
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
        internal HttpMessage CreateListBlobsFlatSegmentRequest(string? prefix, string? marker, int? maxresults, IEnumerable<ListBlobsIncludeItem>? include, int? timeout, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("restype", "container", true);
            request.Uri.AppendQuery("comp", "list", true);
            if (prefix != null)
            {
                request.Uri.AppendQuery("prefix", prefix, true);
            }
            if (marker != null)
            {
                request.Uri.AppendQuery("marker", marker, true);
            }
            if (maxresults != null)
            {
                request.Uri.AppendQuery("maxresults", maxresults.Value, true);
            }
            if (include != null)
            {
                request.Uri.AppendQueryDelimited("include", include, ",", true);
            }
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] The List Blobs operation returns a list of the blobs under the specified container. </summary>
        /// <param name="prefix"> Filters the results to return only containers whose name begins with the specified prefix. </param>
        /// <param name="marker"> A string value that identifies the portion of the list of containers to be returned with the next listing operation. The operation returns the NextMarker value within the response body if the listing operation did not return all containers remaining to be listed with the current page. The NextMarker value can be used as the value for the marker parameter in a subsequent call to request the next page of list items. The marker value is opaque to the client. </param>
        /// <param name="maxresults"> Specifies the maximum number of containers to return. If the request does not specify maxresults, or specifies a value greater than 5000, the server will return up to 5000 items. Note that if the listing operation crosses a partition boundary, then the service will return a continuation token for retrieving the remainder of the results. For this reason, it is possible that the service will return fewer results than specified by maxresults, or than the default of 5000. </param>
        /// <param name="include"> Include this parameter to specify one or more datasets to include in the response. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<BlobsFlatSegment, ListBlobsFlatSegmentHeaders>> ListBlobsFlatSegmentAsync(string? prefix, string? marker, int? maxresults, IEnumerable<ListBlobsIncludeItem>? include, int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.ListBlobsFlatSegment");
            scope.Start();
            try
            {
                using var message = CreateListBlobsFlatSegmentRequest(prefix, marker, maxresults, include, timeout, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobsFlatSegment value = default;
                            var enumerationResults = document.Element("EnumerationResults");
                            if (enumerationResults != null)
                            {
                                value = BlobsFlatSegment.DeserializeBlobsFlatSegment(enumerationResults);
                            }
                            var headers = new ListBlobsFlatSegmentHeaders(message.Response);
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
        /// <summary> [Update] The List Blobs operation returns a list of the blobs under the specified container. </summary>
        /// <param name="prefix"> Filters the results to return only containers whose name begins with the specified prefix. </param>
        /// <param name="marker"> A string value that identifies the portion of the list of containers to be returned with the next listing operation. The operation returns the NextMarker value within the response body if the listing operation did not return all containers remaining to be listed with the current page. The NextMarker value can be used as the value for the marker parameter in a subsequent call to request the next page of list items. The marker value is opaque to the client. </param>
        /// <param name="maxresults"> Specifies the maximum number of containers to return. If the request does not specify maxresults, or specifies a value greater than 5000, the server will return up to 5000 items. Note that if the listing operation crosses a partition boundary, then the service will return a continuation token for retrieving the remainder of the results. For this reason, it is possible that the service will return fewer results than specified by maxresults, or than the default of 5000. </param>
        /// <param name="include"> Include this parameter to specify one or more datasets to include in the response. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<BlobsFlatSegment, ListBlobsFlatSegmentHeaders> ListBlobsFlatSegment(string? prefix, string? marker, int? maxresults, IEnumerable<ListBlobsIncludeItem>? include, int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.ListBlobsFlatSegment");
            scope.Start();
            try
            {
                using var message = CreateListBlobsFlatSegmentRequest(prefix, marker, maxresults, include, timeout, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobsFlatSegment value = default;
                            var enumerationResults = document.Element("EnumerationResults");
                            if (enumerationResults != null)
                            {
                                value = BlobsFlatSegment.DeserializeBlobsFlatSegment(enumerationResults);
                            }
                            var headers = new ListBlobsFlatSegmentHeaders(message.Response);
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
        internal HttpMessage CreateListBlobsHierarchySegmentRequest(string? prefix, string? delimiter, string? marker, int? maxresults, IEnumerable<ListBlobsIncludeItem>? include, int? timeout, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Uri.AppendPath("containerName", false);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("restype", "container", true);
            request.Uri.AppendQuery("comp", "list", true);
            if (prefix != null)
            {
                request.Uri.AppendQuery("prefix", prefix, true);
            }
            if (delimiter != null)
            {
                request.Uri.AppendQuery("delimiter", delimiter, true);
            }
            if (marker != null)
            {
                request.Uri.AppendQuery("marker", marker, true);
            }
            if (maxresults != null)
            {
                request.Uri.AppendQuery("maxresults", maxresults.Value, true);
            }
            if (include != null)
            {
                request.Uri.AppendQueryDelimited("include", include, ",", true);
            }
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> [Update] The List Blobs operation returns a list of the blobs under the specified container. </summary>
        /// <param name="prefix"> Filters the results to return only containers whose name begins with the specified prefix. </param>
        /// <param name="delimiter"> When the request includes this parameter, the operation returns a BlobPrefix element in the response body that acts as a placeholder for all blobs whose names begin with the same substring up to the appearance of the delimiter character. The delimiter may be a single character or a string. </param>
        /// <param name="marker"> A string value that identifies the portion of the list of containers to be returned with the next listing operation. The operation returns the NextMarker value within the response body if the listing operation did not return all containers remaining to be listed with the current page. The NextMarker value can be used as the value for the marker parameter in a subsequent call to request the next page of list items. The marker value is opaque to the client. </param>
        /// <param name="maxresults"> Specifies the maximum number of containers to return. If the request does not specify maxresults, or specifies a value greater than 5000, the server will return up to 5000 items. Note that if the listing operation crosses a partition boundary, then the service will return a continuation token for retrieving the remainder of the results. For this reason, it is possible that the service will return fewer results than specified by maxresults, or than the default of 5000. </param>
        /// <param name="include"> Include this parameter to specify one or more datasets to include in the response. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<BlobsHierarchySegment, ListBlobsHierarchySegmentHeaders>> ListBlobsHierarchySegmentAsync(string? prefix, string? delimiter, string? marker, int? maxresults, IEnumerable<ListBlobsIncludeItem>? include, int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.ListBlobsHierarchySegment");
            scope.Start();
            try
            {
                using var message = CreateListBlobsHierarchySegmentRequest(prefix, delimiter, marker, maxresults, include, timeout, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobsHierarchySegment value = default;
                            var enumerationResults = document.Element("EnumerationResults");
                            if (enumerationResults != null)
                            {
                                value = BlobsHierarchySegment.DeserializeBlobsHierarchySegment(enumerationResults);
                            }
                            var headers = new ListBlobsHierarchySegmentHeaders(message.Response);
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
        /// <summary> [Update] The List Blobs operation returns a list of the blobs under the specified container. </summary>
        /// <param name="prefix"> Filters the results to return only containers whose name begins with the specified prefix. </param>
        /// <param name="delimiter"> When the request includes this parameter, the operation returns a BlobPrefix element in the response body that acts as a placeholder for all blobs whose names begin with the same substring up to the appearance of the delimiter character. The delimiter may be a single character or a string. </param>
        /// <param name="marker"> A string value that identifies the portion of the list of containers to be returned with the next listing operation. The operation returns the NextMarker value within the response body if the listing operation did not return all containers remaining to be listed with the current page. The NextMarker value can be used as the value for the marker parameter in a subsequent call to request the next page of list items. The marker value is opaque to the client. </param>
        /// <param name="maxresults"> Specifies the maximum number of containers to return. If the request does not specify maxresults, or specifies a value greater than 5000, the server will return up to 5000 items. Note that if the listing operation crosses a partition boundary, then the service will return a continuation token for retrieving the remainder of the results. For this reason, it is possible that the service will return fewer results than specified by maxresults, or than the default of 5000. </param>
        /// <param name="include"> Include this parameter to specify one or more datasets to include in the response. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<BlobsHierarchySegment, ListBlobsHierarchySegmentHeaders> ListBlobsHierarchySegment(string? prefix, string? delimiter, string? marker, int? maxresults, IEnumerable<ListBlobsIncludeItem>? include, int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ContainerOperations.ListBlobsHierarchySegment");
            scope.Start();
            try
            {
                using var message = CreateListBlobsHierarchySegmentRequest(prefix, delimiter, marker, maxresults, include, timeout, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobsHierarchySegment value = default;
                            var enumerationResults = document.Element("EnumerationResults");
                            if (enumerationResults != null)
                            {
                                value = BlobsHierarchySegment.DeserializeBlobsHierarchySegment(enumerationResults);
                            }
                            var headers = new ListBlobsHierarchySegmentHeaders(message.Response);
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
