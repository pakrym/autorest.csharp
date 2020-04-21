// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Pipeline;

namespace body_file
{
    /// <summary> The Files service client. </summary>
    public partial class FilesClient
    {
        private readonly ClientDiagnostics _clientDiagnostics;
        private readonly HttpPipeline _pipeline;
        internal FilesRestClient RestClient { get; }
        /// <summary> Initializes a new instance of FilesClient for mocking. </summary>
        protected FilesClient()
        {
        }
        /// <summary> Initializes a new instance of FilesClient. </summary>
        internal FilesClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000")
        {
            RestClient = new FilesRestClient(clientDiagnostics, pipeline, host);
            _clientDiagnostics = clientDiagnostics;
            _pipeline = pipeline;
        }

        /// <summary> Get file. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<Stream>> GetFileAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetFileAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Get file. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<Stream> GetFile(CancellationToken cancellationToken = default)
        {
            return RestClient.GetFile(cancellationToken);
        }

        /// <summary> Get a large file. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<Stream>> GetFileLargeAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetFileLargeAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Get a large file. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<Stream> GetFileLarge(CancellationToken cancellationToken = default)
        {
            return RestClient.GetFileLarge(cancellationToken);
        }

        /// <summary> Get empty file. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<Stream>> GetEmptyFileAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetEmptyFileAsync(cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Get empty file. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<Stream> GetEmptyFile(CancellationToken cancellationToken = default)
        {
            return RestClient.GetEmptyFile(cancellationToken);
        }
    }
}
