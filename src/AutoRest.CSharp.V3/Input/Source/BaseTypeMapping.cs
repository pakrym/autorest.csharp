// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace AutoRest.CSharp.V3.Input.Source
{
    public abstract class BaseTypeMapping<T> where T: MemberMapping
    {
        private readonly INamedTypeSymbol? _existingType;
        private T[] Members { get; }

        protected BaseTypeMapping(INamedTypeSymbol memberAttribute, INamedTypeSymbol? existingType)
        {
            _existingType = existingType;
            List<T> memberMappings = new List<T>();
            foreach (ISymbol member in GetMembers(existingType))
            {
                if (SourceInputModel.TryGetName(member, memberAttribute, out var schemaMemberName))
                {
                    memberMappings.Add(CreateMember(schemaMemberName, member));
                }
            }

            Members = memberMappings.ToArray();
        }

        public T? GetForMember(string name)
        {
            var memberMapping = Members.SingleOrDefault(p => string.Equals(p.OriginalName, name, StringComparison.InvariantCultureIgnoreCase));
            if (memberMapping == null)
            {
                var memberSymbol = _existingType?.GetMembers(name).FirstOrDefault();
                if (memberSymbol != null)
                {
                    memberMapping = CreateMember(memberSymbol.Name, memberSymbol);
                }
            }

            return memberMapping;
        }

        private IEnumerable<ISymbol> GetMembers(INamedTypeSymbol? typeSymbol)
        {
            while (typeSymbol != null)
            {
                foreach (var symbol in typeSymbol.GetMembers())
                {
                    yield return symbol;
                }

                typeSymbol = typeSymbol.BaseType;
            }
        }

        protected abstract T CreateMember(string name, ISymbol member);
    }
}