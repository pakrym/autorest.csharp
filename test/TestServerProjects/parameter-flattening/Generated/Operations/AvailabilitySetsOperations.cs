// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using parameter_flattening.Models;

namespace parameter_flattening
{
    internal partial class AvailabilitySetsOperations
    {
        private string host;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of AvailabilitySetsOperations. </summary>
        public AvailabilitySetsOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000")
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            this.host = host;
            this.clientDiagnostics = clientDiagnostics;
            this.pipeline = pipeline;
        }
        internal HttpMessage CreateUpdateRequest(string resourceGroupName, string avset, AvailabilitySetUpdateParameters tags)
        {
            var message = pipeline.CreateMessage();
            var request = message.Request;
            request.Method = RequestMethodAdditional.Patch;
            var uri = new RawRequestUriBuilder();
            uri.AppendRaw(host, false);
            uri.AppendPath("/parameterFlattening/", false);
            uri.AppendPath(resourceGroupName, true);
            uri.AppendPath("/", false);
            uri.AppendPath(avset, true);
            request.Uri = uri;
            request.Headers.Add("Content-Type", "application/json");
            using var content = new Utf8JsonRequestContent();
            content.JsonWriter.WriteObjectValue(tags);
            request.Content = content;
            return message;
        }
        /// <summary> Updates the tags for an availability set. </summary>
        /// <param name="resourceGroupName"> The name of the resource group. </param>
        /// <param name="avset"> The name of the storage availability set. </param>
        /// <param name="tags"> The tags. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> UpdateAsync(string resourceGroupName, string avset, AvailabilitySetUpdateParameters tags, CancellationToken cancellationToken = default)
        {
            if (resourceGroupName == null)
            {
                throw new ArgumentNullException(nameof(resourceGroupName));
            }
            if (avset == null)
            {
                throw new ArgumentNullException(nameof(avset));
            }
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            using var scope = clientDiagnostics.CreateScope("AvailabilitySetsOperations.Update");
            scope.Start();
            try
            {
                using var message = CreateUpdateRequest(resourceGroupName, avset, tags);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
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
        /// <summary> Updates the tags for an availability set. </summary>
        /// <param name="resourceGroupName"> The name of the resource group. </param>
        /// <param name="avset"> The name of the storage availability set. </param>
        /// <param name="tags"> The tags. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public Response Update(string resourceGroupName, string avset, AvailabilitySetUpdateParameters tags, CancellationToken cancellationToken = default)
        {
            if (resourceGroupName == null)
            {
                throw new ArgumentNullException(nameof(resourceGroupName));
            }
            if (avset == null)
            {
                throw new ArgumentNullException(nameof(avset));
            }
            if (tags == null)
            {
                throw new ArgumentNullException(nameof(tags));
            }

            using var scope = clientDiagnostics.CreateScope("AvailabilitySetsOperations.Update");
            scope.Start();
            try
            {
                using var message = CreateUpdateRequest(resourceGroupName, avset, tags);
                pipeline.Send(message, cancellationToken);
                switch (message.Response.Status)
                {
                    case 200:
                        return message.Response;
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
