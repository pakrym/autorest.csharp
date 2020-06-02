// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.CodeAnalysis;

namespace AutoRest.CSharp.V3.AutoRest.Plugins
{
    internal class MemberSignatureComparer: IEqualityComparer<ISymbol>
    {
        private Func<ISymbol, object> _getSymbol;
        private Func<object, object, bool> _equals;
        private Func<object, int> _getHashCode;

        public static MemberSignatureComparer DuplicateSourceComparer { get; } = new MemberSignatureComparer();

        internal MemberSignatureComparer()
        {
            var publicSymbolType = Type.GetType("Microsoft.CodeAnalysis.CSharp.Symbols.PublicModel.Symbol, Microsoft.CodeAnalysis.CSharp");
            Debug.Assert(publicSymbolType != null);

            var publicSymbolUnderlyingProperty = publicSymbolType.GetProperty("UnderlyingSymbol", BindingFlags.Instance | BindingFlags.NonPublic);

            var symbolParameter = Expression.Parameter(typeof(ISymbol), "symbol");

            _getSymbol = Expression.Lambda<Func<ISymbol, object>>(
                Expression.MakeMemberAccess(
                    Expression.Convert(symbolParameter, publicSymbolType),
                    publicSymbolUnderlyingProperty),
                symbolParameter
            ).Compile();

            var symbolType = Type.GetType("Microsoft.CodeAnalysis.CSharp.Symbol, Microsoft.CodeAnalysis.CSharp");
            Debug.Assert(symbolType != null);

            var type = Type.GetType("Microsoft.CodeAnalysis.CSharp.Symbols.MemberSignatureComparer, Microsoft.CodeAnalysis.CSharp");
            Debug.Assert(type != null);

            var equalsMethod = type.GetMethod("Equals", new[] {symbolType, symbolType});
            Debug.Assert(equalsMethod != null);

            var getHashCode = type.GetMethod("GetHashCode", new[] {symbolType});
            Debug.Assert(getHashCode != null);

            var property = type.GetField("DuplicateSourceComparer", BindingFlags.Static | BindingFlags.Public);
            Debug.Assert(property != null);

            var comparer = property.GetValue(null);
            var xParameter = Expression.Parameter(typeof(object), "x");
            var yParameter = Expression.Parameter(typeof(object), "y");

            _equals = Expression.Lambda<Func<object, object, bool>>(
                Expression.Call(
                    Expression.Constant(comparer, type),
                    equalsMethod,
                    Expression.Convert(xParameter, symbolType),
                    Expression.Convert(yParameter, symbolType)
                ), xParameter, yParameter
            ).Compile();

            _getHashCode = Expression.Lambda<Func<object, int>>(
                Expression.Call(
                    Expression.Constant(comparer, type),
                    getHashCode,
                    Expression.Convert(xParameter, symbolType)
                ), xParameter
            ).Compile();
        }

        public bool Equals(ISymbol x, ISymbol y)
        {
            return _equals(_getSymbol(x), _getSymbol(y));
        }

        public int GetHashCode(ISymbol obj)
        {
            return _getHashCode(_getSymbol(obj));
        }
    }
}