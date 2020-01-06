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
    internal partial class ServiceOperations
    {
        private string url;
        private string xMsVersion;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of ServiceOperations. </summary>
        public ServiceOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string url, string xMsVersion)
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
        internal HttpMessage CreateSetPropertiesRequest(int? timeout, string? xMsClientRequestId, BlobServiceProperties blobServiceProperties)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Put;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Headers.Add("Content-Type", "application/xml");
            request.Uri.AppendQuery("restype", "service", true);
            request.Uri.AppendQuery("comp", "properties", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            using var content = new XmlWriterContent();
            content.XmlWriter.WriteObjectValue(blobServiceProperties, "StorageServiceProperties");
            request.Content = content;
            return message;
        }
        /// <summary> Sets properties for a storage account&apos;s Blob service endpoint, including properties for Storage Analytics and CORS (Cross-Origin Resource Sharing) rules. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="blobServiceProperties"> The StorageService properties. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<SetPropertiesHeaders>> SetPropertiesAsync(int? timeout, string? xMsClientRequestId, BlobServiceProperties blobServiceProperties, CancellationToken cancellationToken = default)
        {
            if (blobServiceProperties == null)
            {
                throw new ArgumentNullException(nameof(blobServiceProperties));
            }

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.SetProperties");
            scope.Start();
            try
            {
                using var message = CreateSetPropertiesRequest(timeout, xMsClientRequestId, blobServiceProperties);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new SetPropertiesHeaders(message.Response);
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
        /// <summary> Sets properties for a storage account&apos;s Blob service endpoint, including properties for Storage Analytics and CORS (Cross-Origin Resource Sharing) rules. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="blobServiceProperties"> The StorageService properties. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<SetPropertiesHeaders> SetProperties(int? timeout, string? xMsClientRequestId, BlobServiceProperties blobServiceProperties, CancellationToken cancellationToken = default)
        {
            if (blobServiceProperties == null)
            {
                throw new ArgumentNullException(nameof(blobServiceProperties));
            }

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.SetProperties");
            scope.Start();
            try
            {
                using var message = CreateSetPropertiesRequest(timeout, xMsClientRequestId, blobServiceProperties);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new SetPropertiesHeaders(message.Response);
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
        internal HttpMessage CreateGetPropertiesRequest(int? timeout, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("restype", "service", true);
            request.Uri.AppendQuery("comp", "properties", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> gets the properties of a storage account&apos;s Blob service, including properties for Storage Analytics and CORS (Cross-Origin Resource Sharing) rules. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<BlobServiceProperties, GetPropertiesHeaders>> GetPropertiesAsync(int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.GetProperties");
            scope.Start();
            try
            {
                using var message = CreateGetPropertiesRequest(timeout, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobServiceProperties value = default;
                            var storageServiceProperties = document.Element("StorageServiceProperties");
                            if (storageServiceProperties != null)
                            {
                                value = BlobServiceProperties.DeserializeBlobServiceProperties(storageServiceProperties);
                            }
                            var headers = new GetPropertiesHeaders(message.Response);
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
        /// <summary> gets the properties of a storage account&apos;s Blob service, including properties for Storage Analytics and CORS (Cross-Origin Resource Sharing) rules. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<BlobServiceProperties, GetPropertiesHeaders> GetProperties(int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.GetProperties");
            scope.Start();
            try
            {
                using var message = CreateGetPropertiesRequest(timeout, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobServiceProperties value = default;
                            var storageServiceProperties = document.Element("StorageServiceProperties");
                            if (storageServiceProperties != null)
                            {
                                value = BlobServiceProperties.DeserializeBlobServiceProperties(storageServiceProperties);
                            }
                            var headers = new GetPropertiesHeaders(message.Response);
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
        internal HttpMessage CreateGetStatisticsRequest(int? timeout, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("restype", "service", true);
            request.Uri.AppendQuery("comp", "stats", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> Retrieves statistics related to replication for the Blob service. It is only available on the secondary location endpoint when read-access geo-redundant replication is enabled for the storage account. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<BlobServiceStatistics, GetStatisticsHeaders>> GetStatisticsAsync(int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.GetStatistics");
            scope.Start();
            try
            {
                using var message = CreateGetStatisticsRequest(timeout, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobServiceStatistics value = default;
                            var storageServiceStats = document.Element("StorageServiceStats");
                            if (storageServiceStats != null)
                            {
                                value = BlobServiceStatistics.DeserializeBlobServiceStatistics(storageServiceStats);
                            }
                            var headers = new GetStatisticsHeaders(message.Response);
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
        /// <summary> Retrieves statistics related to replication for the Blob service. It is only available on the secondary location endpoint when read-access geo-redundant replication is enabled for the storage account. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<BlobServiceStatistics, GetStatisticsHeaders> GetStatistics(int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.GetStatistics");
            scope.Start();
            try
            {
                using var message = CreateGetStatisticsRequest(timeout, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobServiceStatistics value = default;
                            var storageServiceStats = document.Element("StorageServiceStats");
                            if (storageServiceStats != null)
                            {
                                value = BlobServiceStatistics.DeserializeBlobServiceStatistics(storageServiceStats);
                            }
                            var headers = new GetStatisticsHeaders(message.Response);
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
        internal HttpMessage CreateListBlobContainersSegmentRequest(string? prefix, string? marker, int? maxresults, int? timeout, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
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
            request.Uri.AppendQuery("include", "metadata", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The List Containers Segment operation returns a list of the containers under the specified account. </summary>
        /// <param name="prefix"> Filters the results to return only containers whose name begins with the specified prefix. </param>
        /// <param name="marker"> A string value that identifies the portion of the list of containers to be returned with the next listing operation. The operation returns the NextMarker value within the response body if the listing operation did not return all containers remaining to be listed with the current page. The NextMarker value can be used as the value for the marker parameter in a subsequent call to request the next page of list items. The marker value is opaque to the client. </param>
        /// <param name="maxresults"> Specifies the maximum number of containers to return. If the request does not specify maxresults, or specifies a value greater than 5000, the server will return up to 5000 items. Note that if the listing operation crosses a partition boundary, then the service will return a continuation token for retrieving the remainder of the results. For this reason, it is possible that the service will return fewer results than specified by maxresults, or than the default of 5000. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<BlobContainersSegment, ListBlobContainersSegmentHeaders>> ListBlobContainersSegmentAsync(string? prefix, string? marker, int? maxresults, int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.ListBlobContainersSegment");
            scope.Start();
            try
            {
                using var message = CreateListBlobContainersSegmentRequest(prefix, marker, maxresults, timeout, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobContainersSegment value = default;
                            var enumerationResults = document.Element("EnumerationResults");
                            if (enumerationResults != null)
                            {
                                value = BlobContainersSegment.DeserializeBlobContainersSegment(enumerationResults);
                            }
                            var headers = new ListBlobContainersSegmentHeaders(message.Response);
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
        /// <summary> The List Containers Segment operation returns a list of the containers under the specified account. </summary>
        /// <param name="prefix"> Filters the results to return only containers whose name begins with the specified prefix. </param>
        /// <param name="marker"> A string value that identifies the portion of the list of containers to be returned with the next listing operation. The operation returns the NextMarker value within the response body if the listing operation did not return all containers remaining to be listed with the current page. The NextMarker value can be used as the value for the marker parameter in a subsequent call to request the next page of list items. The marker value is opaque to the client. </param>
        /// <param name="maxresults"> Specifies the maximum number of containers to return. If the request does not specify maxresults, or specifies a value greater than 5000, the server will return up to 5000 items. Note that if the listing operation crosses a partition boundary, then the service will return a continuation token for retrieving the remainder of the results. For this reason, it is possible that the service will return fewer results than specified by maxresults, or than the default of 5000. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<BlobContainersSegment, ListBlobContainersSegmentHeaders> ListBlobContainersSegment(string? prefix, string? marker, int? maxresults, int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.ListBlobContainersSegment");
            scope.Start();
            try
            {
                using var message = CreateListBlobContainersSegmentRequest(prefix, marker, maxresults, timeout, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            BlobContainersSegment value = default;
                            var enumerationResults = document.Element("EnumerationResults");
                            if (enumerationResults != null)
                            {
                                value = BlobContainersSegment.DeserializeBlobContainersSegment(enumerationResults);
                            }
                            var headers = new ListBlobContainersSegmentHeaders(message.Response);
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
        internal HttpMessage CreateGetUserDelegationKeyRequest(int? timeout, string? xMsClientRequestId, KeyInfo keyInfo)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Post;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Headers.Add("Content-Type", "application/xml");
            request.Uri.AppendQuery("restype", "service", true);
            request.Uri.AppendQuery("comp", "userdelegationkey", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            using var content = new XmlWriterContent();
            content.XmlWriter.WriteObjectValue(keyInfo, "KeyInfo");
            request.Content = content;
            return message;
        }
        /// <summary> Retrieves a user delegation key for the Blob service. This is only a valid operation when using bearer token authentication. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="keyInfo"> The KeyInfo to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<UserDelegationKey, GetUserDelegationKeyHeaders>> GetUserDelegationKeyAsync(int? timeout, string? xMsClientRequestId, KeyInfo keyInfo, CancellationToken cancellationToken = default)
        {
            if (keyInfo == null)
            {
                throw new ArgumentNullException(nameof(keyInfo));
            }

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.GetUserDelegationKey");
            scope.Start();
            try
            {
                using var message = CreateGetUserDelegationKeyRequest(timeout, xMsClientRequestId, keyInfo);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            UserDelegationKey value = default;
                            var userDelegationKey = document.Element("UserDelegationKey");
                            if (userDelegationKey != null)
                            {
                                value = UserDelegationKey.DeserializeUserDelegationKey(userDelegationKey);
                            }
                            var headers = new GetUserDelegationKeyHeaders(message.Response);
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
        /// <summary> Retrieves a user delegation key for the Blob service. This is only a valid operation when using bearer token authentication. </summary>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="keyInfo"> The KeyInfo to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<UserDelegationKey, GetUserDelegationKeyHeaders> GetUserDelegationKey(int? timeout, string? xMsClientRequestId, KeyInfo keyInfo, CancellationToken cancellationToken = default)
        {
            if (keyInfo == null)
            {
                throw new ArgumentNullException(nameof(keyInfo));
            }

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.GetUserDelegationKey");
            scope.Start();
            try
            {
                using var message = CreateGetUserDelegationKeyRequest(timeout, xMsClientRequestId, keyInfo);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            var document = XDocument.Load(message.Response.ContentStream, LoadOptions.PreserveWhitespace);
                            UserDelegationKey value = default;
                            var userDelegationKey = document.Element("UserDelegationKey");
                            if (userDelegationKey != null)
                            {
                                value = UserDelegationKey.DeserializeUserDelegationKey(userDelegationKey);
                            }
                            var headers = new GetUserDelegationKeyHeaders(message.Response);
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
        internal HttpMessage CreateGetAccountInfoRequest()
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Get;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Headers.Add("x-ms-version", xMsVersion);
            request.Uri.AppendQuery("restype", "account", true);
            request.Uri.AppendQuery("comp", "properties", true);
            return message;
        }
        /// <summary> Returns the sku name and account kind. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<GetAccountInfoHeaders>> GetAccountInfoAsync(CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.GetAccountInfo");
            scope.Start();
            try
            {
                using var message = CreateGetAccountInfoRequest();
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new GetAccountInfoHeaders(message.Response);
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
        /// <summary> Returns the sku name and account kind. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<GetAccountInfoHeaders> GetAccountInfo(CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.GetAccountInfo");
            scope.Start();
            try
            {
                using var message = CreateGetAccountInfoRequest();
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        var headers = new GetAccountInfoHeaders(message.Response);
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
        internal HttpMessage CreateSubmitBatchRequest(long contentLength, string contentType, int? timeout, string? xMsClientRequestId)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethod.Post;
            request.Uri.Reset(new Uri($"{url}"));
            request.Uri.AppendPath("/", false);
            request.Headers.Add("Content-Length", contentLength);
            request.Headers.Add("Content-Type", contentType);
            request.Headers.Add("x-ms-version", xMsVersion);
            if (xMsClientRequestId != null)
            {
                request.Headers.Add("x-ms-client-request-id", xMsClientRequestId);
            }
            request.Uri.AppendQuery("comp", "batch", true);
            if (timeout != null)
            {
                request.Uri.AppendQuery("timeout", timeout.Value, true);
            }
            return message;
        }
        /// <summary> The Batch operation allows multiple API calls to be embedded into a single HTTP request. </summary>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentType"> Required. The value of this header must be multipart/mixed with a batch boundary. Example header value: multipart/mixed; boundary=batch_{GUID}. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<ResponseWithHeaders<SubmitBatchHeaders>> SubmitBatchAsync(long contentLength, string contentType, int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (contentType == null)
            {
                throw new ArgumentNullException(nameof(contentType));
            }

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.SubmitBatch");
            scope.Start();
            try
            {
                using var message = CreateSubmitBatchRequest(contentLength, contentType, timeout, xMsClientRequestId);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new SubmitBatchHeaders(message.Response);
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
        /// <summary> The Batch operation allows multiple API calls to be embedded into a single HTTP request. </summary>
        /// <param name="contentLength"> The length of the request. </param>
        /// <param name="contentType"> Required. The value of this header must be multipart/mixed with a batch boundary. Example header value: multipart/mixed; boundary=batch_{GUID}. </param>
        /// <param name="timeout"> The timeout parameter is expressed in seconds. For more information, see &lt;a href=&quot;https://docs.microsoft.com/en-us/rest/api/storageservices/fileservices/setting-timeouts-for-blob-service-operations&quot;&gt;Setting Timeouts for Blob Service Operations.&lt;/a&gt;. </param>
        /// <param name="xMsClientRequestId"> Provides a client-generated, opaque value with a 1 KB character limit that is recorded in the analytics logs when storage analytics logging is enabled. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public ResponseWithHeaders<SubmitBatchHeaders> SubmitBatch(long contentLength, string contentType, int? timeout, string? xMsClientRequestId, CancellationToken cancellationToken = default)
        {
            if (contentType == null)
            {
                throw new ArgumentNullException(nameof(contentType));
            }

            using var scope = clientDiagnostics.CreateScope("ServiceOperations.SubmitBatch");
            scope.Start();
            try
            {
                using var message = CreateSubmitBatchRequest(contentLength, contentType, timeout, xMsClientRequestId);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 202:
                        var headers = new SubmitBatchHeaders(message.Response);
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
