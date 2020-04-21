// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Threading;
using System.Threading.Tasks;
using additionalProperties.Models;
using Azure;
using Azure.Core.Pipeline;

namespace additionalProperties
{
    /// <summary> The Pets service client. </summary>
    public partial class PetsClient
    {
        private readonly ClientDiagnostics _clientDiagnostics;
        private readonly HttpPipeline _pipeline;
        internal PetsRestClient RestClient { get; }
        /// <summary> Initializes a new instance of PetsClient for mocking. </summary>
        protected PetsClient()
        {
        }
        /// <summary> Initializes a new instance of PetsClient. </summary>
        internal PetsClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000")
        {
            RestClient = new PetsRestClient(clientDiagnostics, pipeline, host);
            _clientDiagnostics = clientDiagnostics;
            _pipeline = pipeline;
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPTrue to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<PetAPTrue>> CreateAPTrueAsync(PetAPTrue createParameters, CancellationToken cancellationToken = default)
        {
            return await RestClient.CreateAPTrueAsync(createParameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPTrue to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<PetAPTrue> CreateAPTrue(PetAPTrue createParameters, CancellationToken cancellationToken = default)
        {
            return RestClient.CreateAPTrue(createParameters, cancellationToken);
        }

        /// <summary> Create a CatAPTrue which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The CatAPTrue to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<CatAPTrue>> CreateCatAPTrueAsync(CatAPTrue createParameters, CancellationToken cancellationToken = default)
        {
            return await RestClient.CreateCatAPTrueAsync(createParameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Create a CatAPTrue which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The CatAPTrue to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<CatAPTrue> CreateCatAPTrue(CatAPTrue createParameters, CancellationToken cancellationToken = default)
        {
            return RestClient.CreateCatAPTrue(createParameters, cancellationToken);
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPObject to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<PetAPObject>> CreateAPObjectAsync(PetAPObject createParameters, CancellationToken cancellationToken = default)
        {
            return await RestClient.CreateAPObjectAsync(createParameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPObject to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<PetAPObject> CreateAPObject(PetAPObject createParameters, CancellationToken cancellationToken = default)
        {
            return RestClient.CreateAPObject(createParameters, cancellationToken);
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPString to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<PetAPString>> CreateAPStringAsync(PetAPString createParameters, CancellationToken cancellationToken = default)
        {
            return await RestClient.CreateAPStringAsync(createParameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPString to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<PetAPString> CreateAPString(PetAPString createParameters, CancellationToken cancellationToken = default)
        {
            return RestClient.CreateAPString(createParameters, cancellationToken);
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPInProperties to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<PetAPInProperties>> CreateAPInPropertiesAsync(PetAPInProperties createParameters, CancellationToken cancellationToken = default)
        {
            return await RestClient.CreateAPInPropertiesAsync(createParameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPInProperties to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<PetAPInProperties> CreateAPInProperties(PetAPInProperties createParameters, CancellationToken cancellationToken = default)
        {
            return RestClient.CreateAPInProperties(createParameters, cancellationToken);
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPInPropertiesWithAPString to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<PetAPInPropertiesWithAPString>> CreateAPInPropertiesWithAPStringAsync(PetAPInPropertiesWithAPString createParameters, CancellationToken cancellationToken = default)
        {
            return await RestClient.CreateAPInPropertiesWithAPStringAsync(createParameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Create a Pet which contains more properties than what is defined. </summary>
        /// <param name="createParameters"> The PetAPInPropertiesWithAPString to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<PetAPInPropertiesWithAPString> CreateAPInPropertiesWithAPString(PetAPInPropertiesWithAPString createParameters, CancellationToken cancellationToken = default)
        {
            return RestClient.CreateAPInPropertiesWithAPString(createParameters, cancellationToken);
        }
    }
}
