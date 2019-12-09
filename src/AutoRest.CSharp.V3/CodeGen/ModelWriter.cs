// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using AutoRest.CSharp.V3.ClientModels;
using AutoRest.CSharp.V3.Plugins;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace AutoRest.CSharp.V3.CodeGen
{
    internal class ModelWriter : StringWriter
    {
        private readonly TypeFactory _typeFactory;
        private readonly SyntaxGenerator _generator;
        private readonly WellKnownTypes _wellKnownTypes;

        public ModelWriter(TypeFactory typeFactory, SyntaxGenerator generator, Compilation compilation)
        {
            _typeFactory = typeFactory;
            _generator = generator;
            _wellKnownTypes = new WellKnownTypes(compilation);
        }

        public SyntaxNode WriteModel(ClientModel model)
        {
            switch (model)
            {
                case ClientObject objectSchema:
                    return _generator.CompilationUnit(WriteObjectSchema(objectSchema));
                case ClientEnum e when e.IsStringBased:
                    return _generator.CompilationUnit(WriteChoiceSchema(e));
                case ClientEnum e when !e.IsStringBased:
                    return _generator.CompilationUnit(WriteSealedChoiceSchema(e));
                default:
                    throw new NotImplementedException();
            }
        }

        private SyntaxNode WriteObjectSchema(ClientObject schema)
        {
            var cs = _typeFactory.CreateType(schema);
            return _generator.NamespaceDeclaration(cs.Namespace.FullName,
                _generator.ClassDeclaration(
                    schema.Name,
                    modifiers: DeclarationModifiers.Partial,
                    accessibility: Accessibility.Public,
                    members: schema.Constants.Select(BuildConstantProperty)
                        .Concat(schema.Properties.Select(BuildProperty)))
            );
        }


        private SyntaxNode TypeReference(CSharpType type)
        {
            var typeSyntax = _generator.DottedName(type.Namespace.FullName + "." + type.Name);
            if (type.Arguments.Any())
            {
                typeSyntax = _generator.WithTypeArguments(typeSyntax, type.Arguments.Select(TypeReference));
            }
            return type.IsNullable ? _generator.NullableTypeExpression(typeSyntax) : typeSyntax;
        }

        private SyntaxNode BuildConstantProperty(ClientObjectConstant constant)
        {
            return _generator.AutoProperty(constant.Name,
                TypeReference(_typeFactory.CreateType(constant.Type)),
                Accessibility.Public,
                DeclarationModifiers.Static,
                _generator.LiteralExpression(constant.Value.Value));
        }

        private SyntaxNode BuildProperty(ClientObjectProperty property)
        {
            var type = property.Type;
            var needsInitializer = type is CollectionTypeReference || type is DictionaryTypeReference;

            var initializer = needsInitializer ?
                _generator.ObjectCreationExpression(TypeReference(_typeFactory.CreateConcreteType(property.Type))) :
                null;

            return _generator.AutoProperty(property.Name,
                TypeReference(_typeFactory.CreateType(property.Type)),
                Accessibility.Public,
                property.IsReadOnly ? DeclarationModifiers.ReadOnly : DeclarationModifiers.None,
                initializer);
        }

        private SyntaxNode WriteSealedChoiceSchema(ClientEnum schema)
        {
            SyntaxNode EnumValue(ClientEnumValue value)
            {
                return _generator.EnumMember(value.Name);
            }

            var cs = _typeFactory.CreateType(schema);
            return _generator.NamespaceDeclaration(cs.Namespace.FullName,
                _generator.EnumDeclaration(
                    schema.Name,
                    modifiers: DeclarationModifiers.Partial,
                    accessibility: Accessibility.Public,
                    members: schema.Values.Select(EnumValue)));
        }

        private SyntaxNode WriteChoiceSchema(ClientEnum schema)
        {
            var cs = _typeFactory.CreateType(schema);

            return _generator.NamespaceDeclaration(cs.Namespace.FullName,
                _generator.StructDeclaration(
                    schema.Name,
                    interfaceTypes: new[]
                    {
                        _generator.WithTypeArguments(
                            _generator.TypeExpression(_wellKnownTypes.IEquatable),
                            TypeReference(cs))
                    },
                    modifiers: DeclarationModifiers.ReadOnly | DeclarationModifiers.Partial,
                    accessibility: Accessibility.Public,
                    members: WriteChoiceSchemaMembers(schema)
            ));


            //var implementType = new CSharpType(typeof(IEquatable<>), cs);
            //using (Struct(null, "readonly partial", schema.CSharpName(), Type(implementType)))
            //{
            //    var stringText = Type(typeof(string));
            //    var nullableStringText = Type(typeof(string), true);
            //    var csTypeText = Type(cs);

            //    Line($"private readonly {Pair(nullableStringText, "_value")};");
            //    Line();

            //    using (Method("public", null, schema.CSharpName(), Pair(stringText, "value")))
            //    {
            //        Line($"_value = value ?? throw new {Type(typeof(ArgumentNullException))}(nameof(value));");
            //    }
            //    Line();

            //    foreach (var choice in schema.Values.Select(c => c))
            //    {
            //        Line($"private const {Pair(stringText, $"{choice.Name}Value")} = \"{choice.Value.Value}\";");
            //    }
            //    Line();

            //    foreach (var choice in schema.Values)
            //    {
            //        Line($"public static {Pair(csTypeText, choice?.Name)} {{ get; }} = new {csTypeText}({choice?.Name}Value);");
            //    }

            //    var boolText = Type(typeof(bool));
            //    var leftRightParams = new[] {Pair(csTypeText, "left"), Pair(csTypeText, "right")};
            //    MethodExpression("public static", boolText, "operator ==", leftRightParams, "left.Equals(right)");
            //    MethodExpression("public static", boolText, "operator !=", leftRightParams, "!left.Equals(right)");
            //    MethodExpression("public static implicit", null, $"operator {csTypeText}", new[]{Pair(stringText, "value")}, $"new {csTypeText}(value)");
            //    Line();

            //    var editorBrowsableNever = $"[{AttributeType(typeof(EditorBrowsableAttribute))}({Type(typeof(EditorBrowsableState))}.Never)]";
            //    Line(editorBrowsableNever);
            //    MethodExpression("public override", boolText, "Equals", new[]{Pair(typeof(object), "obj", true)}, $"obj is {csTypeText} other && Equals(other)");
            //    MethodExpression("public", boolText, "Equals", new[] { Pair(csTypeText, "other") }, $"{stringText}.Equals(_value, other._value, {Type(typeof(StringComparison))}.Ordinal)");
            //    Line();

            //    Line(editorBrowsableNever);
            //    MethodExpression("public override", Type(typeof(int)), "GetHashCode", null, "_value?.GetHashCode() ?? 0");
            //    MethodExpression("public override", nullableStringText, "ToString", null, "_value");
            //}
        }

        private IEnumerable<SyntaxNode> WriteChoiceSchemaMembers(ClientEnum schema)
        {
            var cs = _typeFactory.CreateType(schema);
            SyntaxNode typeReference = TypeReference(cs);
            SyntaxNode stringType = _generator.TypeExpression(SpecialType.System_String);
            yield return _generator.FieldDeclaration("_value", stringType, Accessibility.Private, DeclarationModifiers.ReadOnly);

            SyntaxNode valueFieldIdentifier = _generator.IdentifierName("_value");
            yield return _generator.ConstructorDeclaration(
                schema.Name,
                parameters: new[] { _generator.ParameterDeclaration("value", stringType) },
                Accessibility.Public,
                statements: new[]
                {
                    _generator.AssignmentStatement(
                        valueFieldIdentifier,
                        _generator.CoalesceExpression(
                            _generator.IdentifierName("value"),
                            _generator.ThrowExpression(_generator.ObjectCreationExpression(_wellKnownTypes.ArgumentNullException, _generator.NameOfExpression(_generator.IdentifierName("value"))))))
                }
            );

            foreach (ClientEnumValue choice in schema.Values)
            {
                yield return _generator.FieldDeclaration($"{choice.Name}Value", stringType, Accessibility.Private, DeclarationModifiers.Const, _generator.LiteralExpression(choice.Value.Value));
            }

            foreach (ClientEnumValue choice in schema.Values)
            {
                yield return _generator.AutoProperty($"{choice.Name}", typeReference, Accessibility.Public, DeclarationModifiers.Static | DeclarationModifiers.ReadOnly,
                    _generator.ObjectCreationExpression(typeReference, _generator.IdentifierName($"{choice.Name}Value")));
            }

            yield return _generator.OverrideMethod(_wellKnownTypes.ObjectEquals, statements: new[]
            {
                _generator.ReturnStatement(
                    _generator.LogicalAndExpression(
                        _generator.IsPattern(_generator.IdentifierName("obj"), typeReference, "other"),
                    _generator.InvocationExpression(_generator.IdentifierName("Equals"), _generator.Argument(_generator.IdentifierName("other")))
                        ))
            });

            yield return _generator.OverrideMethod(_wellKnownTypes.ObjectGetHashCode, statements: new[]
            {
                _generator.ReturnStatement(
                    _generator.CoalesceExpression(
                        SyntaxFactory.ConditionalAccessExpression(
                            (ExpressionSyntax) valueFieldIdentifier,
                            (ExpressionSyntax) _generator.InvocationExpression(SyntaxFactory.MemberBindingExpression((SimpleNameSyntax) _generator.IdentifierName("GetHashCode")))),
                        _generator.LiteralExpression(0)))
            });

            yield return _generator.OverrideMethod(_wellKnownTypes.ObjectToString, new[] { _generator.ReturnStatement(valueFieldIdentifier) });
        }
    }
}
