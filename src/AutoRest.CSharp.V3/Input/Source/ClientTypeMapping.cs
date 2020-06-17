// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.CodeAnalysis;

namespace AutoRest.CSharp.V3.Input.Source
{
    public class ClientTypeMapping: BaseTypeMapping<ModelPropertyMapping>
    {
        public ClientTypeMapping(INamedTypeSymbol memberAttribute, INamedTypeSymbol? existingType) : base(memberAttribute, existingType)
        {
        }

        protected override ModelPropertyMapping CreateMember(string name, ISymbol member)
        {
            return new ClientMethodMapping(name, member);
        }
    }
}