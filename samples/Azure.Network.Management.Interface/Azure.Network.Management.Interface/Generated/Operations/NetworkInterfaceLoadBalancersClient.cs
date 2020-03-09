// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Network.Management.Interface.Models;

namespace Azure.Network.Management.Interface
{
    public partial class NetworkInterfaceLoadBalancersClient
    {
        private readonly ClientDiagnostics clientDiagnostics;
        private readonly HttpPipeline pipeline;
        internal NetworkInterfaceLoadBalancersRestClient RestClient { get; }
        /// <summary> Initializes a new instance of NetworkInterfaceLoadBalancersClient for mocking. </summary>
        protected NetworkInterfaceLoadBalancersClient()
        {
        }
        /// <summary> Initializes a new instance of NetworkInterfaceLoadBalancersClient. </summary>
        internal NetworkInterfaceLoadBalancersClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string subscriptionId, string host = "https://management.azure.com", string ApiVersion = "2019-11-01")
        {
            RestClient = new NetworkInterfaceLoadBalancersRestClient(clientDiagnostics, pipeline, subscriptionId, host, ApiVersion);
            this.clientDiagnostics = clientDiagnostics;
            this.pipeline = pipeline;
        }
        /// <summary> List all load balancers in a network interface. </summary>
        /// <param name="resourceGroupName"> The name of the resource group. </param>
        /// <param name="networkInterfaceName"> The name of the network interface. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual AsyncPageable<LoadBalancer> ListAsync(string resourceGroupName, string networkInterfaceName, CancellationToken cancellationToken = default)
        {
            if (resourceGroupName == null)
            {
                throw new ArgumentNullException(nameof(resourceGroupName));
            }
            if (networkInterfaceName == null)
            {
                throw new ArgumentNullException(nameof(networkInterfaceName));
            }

            async Task<Page<LoadBalancer>> FirstPageFunc(int? pageSizeHint)
            {
                var response = await RestClient.ListAsync(resourceGroupName, networkInterfaceName, cancellationToken).ConfigureAwait(false);
                return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
            }
            async Task<Page<LoadBalancer>> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                var response = await RestClient.ListNextPageAsync(nextLink, cancellationToken).ConfigureAwait(false);
                return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
            }
            return PageableHelpers.CreateAsyncEnumerable(FirstPageFunc, NextPageFunc);
        }
        /// <summary> List all load balancers in a network interface. </summary>
        /// <param name="resourceGroupName"> The name of the resource group. </param>
        /// <param name="networkInterfaceName"> The name of the network interface. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Pageable<LoadBalancer> List(string resourceGroupName, string networkInterfaceName, CancellationToken cancellationToken = default)
        {
            if (resourceGroupName == null)
            {
                throw new ArgumentNullException(nameof(resourceGroupName));
            }
            if (networkInterfaceName == null)
            {
                throw new ArgumentNullException(nameof(networkInterfaceName));
            }

            Page<LoadBalancer> FirstPageFunc(int? pageSizeHint)
            {
                var response = RestClient.List(resourceGroupName, networkInterfaceName, cancellationToken);
                return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
            }
            Page<LoadBalancer> NextPageFunc(string nextLink, int? pageSizeHint)
            {
                var response = RestClient.ListNextPage(nextLink, cancellationToken);
                return Page.FromValues(response.Value.Value, response.Value.NextLink, response.GetRawResponse());
            }
            return PageableHelpers.CreateEnumerable(FirstPageFunc, NextPageFunc);
        }
    }
}
