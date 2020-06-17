// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.CodeAnalysis;

namespace AutoRest.CSharp.V3.Input.Source
{
    public class ClientMethodMapping : ModelPropertyMapping
    {
        public ClientMethodMapping(string name, ISymbol member): base(name, member)
        {
        }
    }
}