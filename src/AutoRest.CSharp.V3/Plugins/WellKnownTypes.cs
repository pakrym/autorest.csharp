// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.CodeAnalysis;

namespace AutoRest.CSharp.V3.Plugins
{
    internal class WellKnownTypes
    {
        private readonly Compilation _compilation;

        public WellKnownTypes(Compilation compilation)
        {
            _compilation = compilation;
        }

        public INamedTypeSymbol IEquatable => _compilation.GetTypeByMetadataName("System.IEquatable`1");

        public IMethodSymbol ObjectToString => (IMethodSymbol) _compilation.GetSpecialType(SpecialType.System_Object).GetMembers("ToString").Single();
        public IMethodSymbol ObjectGetHashCode => (IMethodSymbol)_compilation.GetSpecialType(SpecialType.System_Object).GetMembers("GetHashCode").Single();
        public IMethodSymbol ObjectEquals => (IMethodSymbol)_compilation.GetSpecialType(SpecialType.System_Object).GetMembers("Equals").Single(m => !m.IsStatic);

        public INamedTypeSymbol EditorBrowsableAttribute => _compilation.GetTypeByMetadataName("System.ComponentModel.EditorBrowsableAttribute");
        public INamedTypeSymbol EditorBrowsableState => _compilation.GetTypeByMetadataName("System.ComponentModel.EditorBrowsableState");
        public ISymbol EditorBrowsableStateNever => EditorBrowsableState.GetMembers("Never").Single();

        public ITypeSymbol ArgumentNullException => _compilation.GetTypeByMetadataName("System.ArgumentNullException");
    }
}
