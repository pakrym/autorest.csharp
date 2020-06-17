// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using CustomNamespace;

namespace TypeSchemaMapping
{
    internal partial class MainRestClient
    {
#if GENERATOR
        [CodeGenMember("OperationStructAsync")]
        public partial Task<Response<RenamedModelStruct>> DoWorkAsync(RenamedModelStruct? body = null, CancellationToken cancellationToken = default);

        [CodeGenMember("OperationStruct")]
        public partial Response<RenamedModelStruct> DoWork(RenamedModelStruct? body = null, CancellationToken cancellationToken = default);
#endif
    }
}