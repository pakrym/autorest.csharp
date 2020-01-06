// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;

namespace body_duration
{
    internal partial class DurationOperations
    {
        private string host;
        private ClientDiagnostics clientDiagnostics;
        private HttpPipeline pipeline;
        /// <summary> Initializes a new instance of DurationOperations. </summary>
        public DurationOperations(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000")
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            this.host = host;
            this.clientDiagnostics = clientDiagnostics;
            this.pipeline = pipeline;
        }
        /// <summary> Get null duration value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<TimeSpan>> GetNullAsync(CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("body_duration.GetNull");
            scope.Start();
            try
            {
                using var message = pipeline.CreateMessage();
                var request = message.Request;
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}"));
                request.Uri.AppendPath("/duration/null", false);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            var value = document.RootElement.GetTimeSpan("P");
                            return Response.FromValue(value, message.Response);
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
        /// <summary> Put a positive duration value. </summary>
        /// <param name="durationBody"> The duration to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response> PutPositiveDurationAsync(TimeSpan durationBody, CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("body_duration.PutPositiveDuration");
            scope.Start();
            try
            {
                using var message = pipeline.CreateMessage();
                var request = message.Request;
                request.Method = RequestMethod.Put;
                request.Uri.Reset(new Uri($"{host}"));
                request.Uri.AppendPath("/duration/positiveduration", false);
                request.Headers.Add("Content-Type", "application/json");
                using var content = new Utf8JsonRequestContent();
                content.JsonWriter.WriteStringValue(durationBody, "P");
                request.Content = content;
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
        /// <summary> Get a positive duration value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<TimeSpan>> GetPositiveDurationAsync(CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("body_duration.GetPositiveDuration");
            scope.Start();
            try
            {
                using var message = pipeline.CreateMessage();
                var request = message.Request;
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}"));
                request.Uri.AppendPath("/duration/positiveduration", false);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            var value = document.RootElement.GetTimeSpan("P");
                            return Response.FromValue(value, message.Response);
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
        /// <summary> Get an invalid duration value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public async ValueTask<Response<TimeSpan>> GetInvalidAsync(CancellationToken cancellationToken = default)
        {

            using var scope = clientDiagnostics.CreateScope("body_duration.GetInvalid");
            scope.Start();
            try
            {
                using var message = pipeline.CreateMessage();
                var request = message.Request;
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}"));
                request.Uri.AppendPath("/duration/invalid", false);
                await pipeline.SendAsync(message, cancellationToken).ConfigureAwait(false);
                switch (message.Response.Status)
                {
                    case 200:
                        {
                            using var document = await JsonDocument.ParseAsync(message.Response.ContentStream, default, cancellationToken).ConfigureAwait(false);
                            var value = document.RootElement.GetTimeSpan("P");
                            return Response.FromValue(value, message.Response);
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
    }
}
