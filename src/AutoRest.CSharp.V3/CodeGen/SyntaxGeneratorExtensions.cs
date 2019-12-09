// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Simplification;

namespace AutoRest.CSharp.V3.CodeGen
{
    internal static class SyntaxGeneratorExtensions
    {
        public static SyntaxNode AutoProperty(this SyntaxGenerator generator,
            string name,
            SyntaxNode type,
            Accessibility accessibility,
            DeclarationModifiers modifiers,
            SyntaxNode? initializer)
        {
            var actualModifiers = modifiers - DeclarationModifiers.ReadOnly;

            List<AccessorDeclarationSyntax> accessors = new List<AccessorDeclarationSyntax>();
            SyntaxToken semicolonToken = SyntaxFactory.Token(SyntaxKind.SemicolonToken);

            accessors.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                .WithSemicolonToken(semicolonToken));

            if (!modifiers.IsReadOnly)
            {
                accessors.Add(SyntaxFactory.AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                    .WithSemicolonToken(semicolonToken));
            }

            var initializerSyntax = initializer == null ? null : SyntaxFactory.EqualsValueClause(
                (ExpressionSyntax) initializer
                );
            return SyntaxFactory.PropertyDeclaration(
                default,
                AsModifierList(accessibility, actualModifiers, SyntaxKind.PropertyDeclaration),
                (TypeSyntax)type,
                default,
                name.ToIdentifierToken(),
                SyntaxFactory.AccessorList(SyntaxFactory.List(accessors)),
                default,
                initializerSyntax,
                semicolonToken: initializer == null ? default : semicolonToken);
        }

        public static SyntaxNode OverrideMethod(this SyntaxGenerator generator, IMethodSymbol method, IEnumerable<SyntaxNode> statements)
        {
            var decl = generator.MethodDeclaration(
                method.Name,
                parameters: method.Parameters.Select(p => generator.ParameterDeclaration(p)),
                returnType: method.ReturnType.SpecialType == SpecialType.System_Void ? null : generator.TypeExpression(method.ReturnType),
                accessibility: method.DeclaredAccessibility,
                modifiers: DeclarationModifiers.From(method) - DeclarationModifiers.Virtual + DeclarationModifiers.Override,
                statements: statements);

            return decl;
        }

        public static SyntaxNode IsPattern(this SyntaxGenerator generator, SyntaxNode expression, SyntaxNode type, string variable)
        {
            return SyntaxFactory.IsPatternExpression((ExpressionSyntax) expression,
                SyntaxFactory.DeclarationPattern((TypeSyntax)type,
                    SyntaxFactory.SingleVariableDesignation(
                        variable.ToIdentifierToken()
                    )));
        }

        public static string EscapeIdentifier(
            this string identifier,
            bool isQueryContext = false)
        {
            var nullIndex = identifier.IndexOf('\0');
            if (nullIndex >= 0)
            {
                identifier = identifier.Substring(0, nullIndex);
            }

            var needsEscaping = SyntaxFacts.GetKeywordKind(identifier) != SyntaxKind.None;

            // Check if we need to escape this contextual keyword
            needsEscaping = needsEscaping || (isQueryContext && SyntaxFacts.IsQueryContextualKeyword(SyntaxFacts.GetContextualKeywordKind(identifier)));

            return needsEscaping ? "@" + identifier : identifier;
        }

        public static SyntaxToken ToIdentifierToken(
            this string identifier,
            bool isQueryContext = false)
        {
            var escaped = identifier.EscapeIdentifier(isQueryContext);

            if (escaped.Length == 0 || escaped[0] != '@')
            {
                return SyntaxFactory.Identifier(escaped);
            }

            var unescaped = identifier.StartsWith("@", StringComparison.Ordinal)
                ? identifier.Substring(1)
                : identifier;

            var token = SyntaxFactory.Identifier(
                default, SyntaxKind.None, "@" + unescaped, unescaped, default);

            if (!identifier.StartsWith("@", StringComparison.Ordinal))
            {
                token = token.WithAdditionalAnnotations(Simplifier.Annotation);
            }

            return token;
        }
        private static SyntaxTokenList AsModifierList(Accessibility accessibility, DeclarationModifiers modifiers, SyntaxKind kind)
        {
            return AsModifierList(accessibility, GetAllowedModifiers(kind) & modifiers);
        }

        private static readonly DeclarationModifiers s_fieldModifiers = DeclarationModifiers.Const | DeclarationModifiers.New | DeclarationModifiers.ReadOnly | DeclarationModifiers.Static | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_methodModifiers = DeclarationModifiers.Abstract | DeclarationModifiers.Async | DeclarationModifiers.New | DeclarationModifiers.Override | DeclarationModifiers.Partial | DeclarationModifiers.Sealed | DeclarationModifiers.Static | DeclarationModifiers.Virtual | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_constructorModifiers = DeclarationModifiers.Static | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_destructorModifiers = DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_propertyModifiers = DeclarationModifiers.Abstract | DeclarationModifiers.New | DeclarationModifiers.Override | DeclarationModifiers.ReadOnly | DeclarationModifiers.Sealed | DeclarationModifiers.Static | DeclarationModifiers.Virtual | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_eventModifiers = DeclarationModifiers.Abstract | DeclarationModifiers.New | DeclarationModifiers.Override | DeclarationModifiers.Sealed | DeclarationModifiers.Static | DeclarationModifiers.Virtual | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_eventFieldModifiers = DeclarationModifiers.New | DeclarationModifiers.Static | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_indexerModifiers = DeclarationModifiers.Abstract | DeclarationModifiers.New | DeclarationModifiers.Override | DeclarationModifiers.ReadOnly | DeclarationModifiers.Sealed | DeclarationModifiers.Static | DeclarationModifiers.Virtual | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_classModifiers = DeclarationModifiers.Abstract | DeclarationModifiers.New | DeclarationModifiers.Partial | DeclarationModifiers.Sealed | DeclarationModifiers.Static | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_structModifiers = DeclarationModifiers.New | DeclarationModifiers.Partial | DeclarationModifiers.ReadOnly | DeclarationModifiers.Ref | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_interfaceModifiers = DeclarationModifiers.New | DeclarationModifiers.Partial | DeclarationModifiers.Unsafe;
        private static readonly DeclarationModifiers s_accessorModifiers = DeclarationModifiers.Abstract | DeclarationModifiers.New | DeclarationModifiers.Override | DeclarationModifiers.Virtual;
        private static readonly DeclarationModifiers s_localFunctionModifiers = DeclarationModifiers.Async | DeclarationModifiers.Static;

        private static DeclarationModifiers GetAllowedModifiers(SyntaxKind kind)
        {
            switch (kind)
            {
                case SyntaxKind.ClassDeclaration:
                    return s_classModifiers;

                case SyntaxKind.EnumDeclaration:
                    return DeclarationModifiers.New;

                case SyntaxKind.DelegateDeclaration:
                    return DeclarationModifiers.New | DeclarationModifiers.Unsafe;

                case SyntaxKind.InterfaceDeclaration:
                    return s_interfaceModifiers;

                case SyntaxKind.StructDeclaration:
                    return s_structModifiers;

                case SyntaxKind.MethodDeclaration:
                case SyntaxKind.OperatorDeclaration:
                case SyntaxKind.ConversionOperatorDeclaration:
                    return s_methodModifiers;

                case SyntaxKind.ConstructorDeclaration:
                    return s_constructorModifiers;

                case SyntaxKind.DestructorDeclaration:
                    return s_destructorModifiers;

                case SyntaxKind.FieldDeclaration:
                    return s_fieldModifiers;

                case SyntaxKind.PropertyDeclaration:
                    return s_propertyModifiers;

                case SyntaxKind.IndexerDeclaration:
                    return s_indexerModifiers;

                case SyntaxKind.EventFieldDeclaration:
                    return s_eventFieldModifiers;

                case SyntaxKind.EventDeclaration:
                    return s_eventModifiers;

                case SyntaxKind.GetAccessorDeclaration:
                case SyntaxKind.SetAccessorDeclaration:
                case SyntaxKind.AddAccessorDeclaration:
                case SyntaxKind.RemoveAccessorDeclaration:
                    return s_accessorModifiers;

                case SyntaxKind.LocalFunctionStatement:
                    return s_localFunctionModifiers;

                case SyntaxKind.EnumMemberDeclaration:
                case SyntaxKind.Parameter:
                case SyntaxKind.LocalDeclarationStatement:
                default:
                    return DeclarationModifiers.None;
            }
        }
        private static SyntaxTokenList AsModifierList(Accessibility accessibility, DeclarationModifiers modifiers)
        {
            var list = SyntaxFactory.TokenList();

            switch (accessibility)
            {
                case Accessibility.Internal:
                    list = list.Add(SyntaxFactory.Token(SyntaxKind.InternalKeyword));
                    break;
                case Accessibility.Public:
                    list = list.Add(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
                    break;
                case Accessibility.Private:
                    list = list.Add(SyntaxFactory.Token(SyntaxKind.PrivateKeyword));
                    break;
                case Accessibility.Protected:
                    list = list.Add(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword));
                    break;
                case Accessibility.ProtectedOrInternal:
                    list = list.Add(SyntaxFactory.Token(SyntaxKind.InternalKeyword))
                               .Add(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword));
                    break;
                case Accessibility.ProtectedAndInternal:
                    list = list.Add(SyntaxFactory.Token(SyntaxKind.PrivateKeyword))
                               .Add(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword));
                    break;
                case Accessibility.NotApplicable:
                    break;
            }

            if (modifiers.IsAbstract)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.AbstractKeyword));
            }

            if (modifiers.IsNew)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.NewKeyword));
            }

            if (modifiers.IsSealed)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.SealedKeyword));
            }

            if (modifiers.IsOverride)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));
            }

            if (modifiers.IsVirtual)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.VirtualKeyword));
            }

            if (modifiers.IsStatic)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.StaticKeyword));
            }

            if (modifiers.IsAsync)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.AsyncKeyword));
            }

            if (modifiers.IsConst)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.ConstKeyword));
            }

            if (modifiers.IsReadOnly)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword));
            }

            if (modifiers.IsUnsafe)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.UnsafeKeyword));
            }

            // partial and ref must be last
            if (modifiers.IsRef)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.RefKeyword));
            }

            if (modifiers.IsPartial)
            {
                list = list.Add(SyntaxFactory.Token(SyntaxKind.PartialKeyword));
            }

            return list;
        }

    }
}
