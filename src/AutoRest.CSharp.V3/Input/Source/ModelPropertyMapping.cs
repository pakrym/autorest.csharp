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
        }
    }
}