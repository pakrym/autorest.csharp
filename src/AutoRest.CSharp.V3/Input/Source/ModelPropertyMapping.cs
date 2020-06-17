// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;
using Microsoft.CodeAnalysis;

namespace AutoRest.CSharp.V3.Input.Source
{
    public class ModelPropertyMapping: MemberMapping
    {
        public ModelPropertyMapping(string originalName, ISymbol existingMember): base(originalName, existingMember)
        {
            var attributeType = existingMember.ContainingAssembly.GetTypeByMetadataName(typeof(CodeGenMemberAttribute).FullName!);

            foreach (var attributeData in existingMember.GetAttributes())
            {
                if (SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, attributeType))
                {
                    foreach (var namedArgument in attributeData.NamedArguments)
                    {
                        switch (namedArgument.Key)
                        {
                            case nameof(CodeGenMemberAttribute.Initialize):
                                Initialize = (bool) (namedArgument.Value.Value ?? false);
                                continue;
                            case nameof(CodeGenMemberAttribute.EmptyAsUndefined):
                                EmptyAsUndefined = (bool) (namedArgument.Value.Value ?? false);
                                break;
                        }
                    }
                }
            }
        }

        public bool Initialize { get; }
        public bool EmptyAsUndefined { get; }
    }
}