// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.CodeAnalysis;

namespace AutoRest.CSharp.V3.Input.Source
{
    public class MemberMapping
    {
        public string OriginalName { get; }
        public ISymbol ExistingMember { get; }

        public MemberMapping(string originalName, ISymbol existingMember)
        {
            OriginalName = originalName;
            ExistingMember = existingMember;
        }
    }
}