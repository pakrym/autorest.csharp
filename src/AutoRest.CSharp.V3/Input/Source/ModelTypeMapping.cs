// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.CodeAnalysis;

namespace AutoRest.CSharp.V3.Input.Source
{
    public class ModelTypeMapping: BaseTypeMapping<ModelPropertyMapping>
    {
        protected override ModelPropertyMapping CreateMember(string schemaMemberName, ISymbol member)
        {
            return new ModelPropertyMapping(schemaMemberName, member);
        }

        public ModelTypeMapping(INamedTypeSymbol memberAttribute, INamedTypeSymbol? existingType) : base(memberAttribute, existingType)
        {
        }
    }
}
