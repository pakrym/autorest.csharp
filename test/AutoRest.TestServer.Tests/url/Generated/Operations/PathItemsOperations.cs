// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;

namespace url.Operations.V100
{
    public static class PathItemsOperations
    {
        public static async ValueTask<Response> GetAllWithValuesAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string pathItemStringPath, string localStringPath, string host = "http://localhost:3000", string? pathItemStringQuery = default, string? localStringQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetAllWithValues");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/pathitem/nullable/globalStringPath/{globalStringPath}/pathItemStringPath/{pathItemStringPath}/localStringPath/{localStringPath}/globalStringQuery/pathItemStringQuery/localStringQuery"));
                if (pathItemStringQuery != null)
                {
                    request.Uri.AppendQuery("pathItemStringQuery", pathItemStringQuery.ToString()!);
                }
                if (localStringQuery != null)
                {
                    request.Uri.AppendQuery("localStringQuery", localStringQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetGlobalQueryNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string pathItemStringPath, string localStringPath, string host = "http://localhost:3000", string? pathItemStringQuery = default, string? localStringQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetGlobalQueryNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/pathitem/nullable/globalStringPath/{globalStringPath}/pathItemStringPath/{pathItemStringPath}/localStringPath/{localStringPath}/null/pathItemStringQuery/localStringQuery"));
                if (pathItemStringQuery != null)
                {
                    request.Uri.AppendQuery("pathItemStringQuery", pathItemStringQuery.ToString()!);
                }
                if (localStringQuery != null)
                {
                    request.Uri.AppendQuery("localStringQuery", localStringQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetGlobalAndLocalQueryNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string pathItemStringPath, string localStringPath, string host = "http://localhost:3000", string? pathItemStringQuery = default, string? localStringQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetGlobalAndLocalQueryNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/pathitem/nullable/globalStringPath/{globalStringPath}/pathItemStringPath/{pathItemStringPath}/localStringPath/{localStringPath}/null/pathItemStringQuery/null"));
                if (pathItemStringQuery != null)
                {
                    request.Uri.AppendQuery("pathItemStringQuery", pathItemStringQuery.ToString()!);
                }
                if (localStringQuery != null)
                {
                    request.Uri.AppendQuery("localStringQuery", localStringQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetLocalPathItemQueryNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string pathItemStringPath, string localStringPath, string host = "http://localhost:3000", string? pathItemStringQuery = default, string? localStringQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetLocalPathItemQueryNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/pathitem/nullable/globalStringPath/{globalStringPath}/pathItemStringPath/{pathItemStringPath}/localStringPath/{localStringPath}/globalStringQuery/null/null"));
                if (pathItemStringQuery != null)
                {
                    request.Uri.AppendQuery("pathItemStringQuery", pathItemStringQuery.ToString()!);
                }
                if (localStringQuery != null)
                {
                    request.Uri.AppendQuery("localStringQuery", localStringQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
    }
}
