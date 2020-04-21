// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Storage.Management.Models;

namespace Azure.Storage.Management
{
    /// <summary> The ManagementPolicies service client. </summary>
    public partial class ManagementPoliciesClient
    {
        private readonly ClientDiagnostics _clientDiagnostics;
        private readonly HttpPipeline _pipeline;
        internal ManagementPoliciesRestClient RestClient { get; }
        /// <summary> Initializes a new instance of ManagementPoliciesClient. </summary>
        public ManagementPoliciesClient(string subscriptionId, TokenCredential tokenCredential, StorageManagementClientOptions options = null)
        {
            options = new StorageManagementClientOptions();
            _clientDiagnostics = new ClientDiagnostics(options);
            _pipeline = ManagementPipelineBuilder.Build(tokenCredential, options);
            RestClient = new ManagementPoliciesRestClient(_clientDiagnostics, _pipeline, subscriptionId, options.Version);
        }

        /// <summary> Gets the managementpolicy associated with the specified storage account. </summary>
        /// <param name="resourceGroupName"> The name of the resource group within the user&apos;s subscription. The name is case insensitive. </param>
        /// <param name="accountName"> The name of the storage account within the specified resource group. Storage account names must be between 3 and 24 characters in length and use numbers and lower-case letters only. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<ManagementPolicy>> GetAsync(string resourceGroupName, string accountName, CancellationToken cancellationToken = default)
        {
            return await RestClient.GetAsync(resourceGroupName, accountName, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Gets the managementpolicy associated with the specified storage account. </summary>
        /// <param name="resourceGroupName"> The name of the resource group within the user&apos;s subscription. The name is case insensitive. </param>
        /// <param name="accountName"> The name of the storage account within the specified resource group. Storage account names must be between 3 and 24 characters in length and use numbers and lower-case letters only. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<ManagementPolicy> Get(string resourceGroupName, string accountName, CancellationToken cancellationToken = default)
        {
            return RestClient.Get(resourceGroupName, accountName, cancellationToken);
        }

        /// <summary> Sets the managementpolicy to the specified storage account. </summary>
        /// <param name="resourceGroupName"> The name of the resource group within the user&apos;s subscription. The name is case insensitive. </param>
        /// <param name="accountName"> The name of the storage account within the specified resource group. Storage account names must be between 3 and 24 characters in length and use numbers and lower-case letters only. </param>
        /// <param name="policy"> The Storage Account ManagementPolicy, in JSON format. See more details in: https://docs.microsoft.com/en-us/azure/storage/common/storage-lifecycle-managment-concepts. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response<ManagementPolicy>> CreateOrUpdateAsync(string resourceGroupName, string accountName, ManagementPolicySchema policy = null, CancellationToken cancellationToken = default)
        {
            return await RestClient.CreateOrUpdateAsync(resourceGroupName, accountName, policy, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Sets the managementpolicy to the specified storage account. </summary>
        /// <param name="resourceGroupName"> The name of the resource group within the user&apos;s subscription. The name is case insensitive. </param>
        /// <param name="accountName"> The name of the storage account within the specified resource group. Storage account names must be between 3 and 24 characters in length and use numbers and lower-case letters only. </param>
        /// <param name="policy"> The Storage Account ManagementPolicy, in JSON format. See more details in: https://docs.microsoft.com/en-us/azure/storage/common/storage-lifecycle-managment-concepts. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<ManagementPolicy> CreateOrUpdate(string resourceGroupName, string accountName, ManagementPolicySchema policy = null, CancellationToken cancellationToken = default)
        {
            return RestClient.CreateOrUpdate(resourceGroupName, accountName, policy, cancellationToken);
        }

        /// <summary> Deletes the managementpolicy associated with the specified storage account. </summary>
        /// <param name="resourceGroupName"> The name of the resource group within the user&apos;s subscription. The name is case insensitive. </param>
        /// <param name="accountName"> The name of the storage account within the specified resource group. Storage account names must be between 3 and 24 characters in length and use numbers and lower-case letters only. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async Task<Response> DeleteAsync(string resourceGroupName, string accountName, CancellationToken cancellationToken = default)
        {
            return await RestClient.DeleteAsync(resourceGroupName, accountName, cancellationToken).ConfigureAwait(false);
        }

        /// <summary> Deletes the managementpolicy associated with the specified storage account. </summary>
        /// <param name="resourceGroupName"> The name of the resource group within the user&apos;s subscription. The name is case insensitive. </param>
        /// <param name="accountName"> The name of the storage account within the specified resource group. Storage account names must be between 3 and 24 characters in length and use numbers and lower-case letters only. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response Delete(string resourceGroupName, string accountName, CancellationToken cancellationToken = default)
        {
            return RestClient.Delete(resourceGroupName, accountName, cancellationToken);
        }
    }
}
