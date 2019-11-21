using System;
using AutoRest.CSharp.V3.Pipeline;
using AutoRest.CSharp.V3.Pipeline.Generated;

namespace AutoRest.CSharp.V3
{
    internal class ModelBuilder
    {
        public GeneratedModule Build(CodeModel model)
        {
            var generatedModule = new GeneratedModule();

            foreach (var modelSchema in model.Schemas.Objects.EmptyIfNull())
            {
                generatedModule.Types.Add(BuildObjectModel(modelSchema));
            }

            foreach (var modelSchema in model.Schemas.Choices.EmptyIfNull())
            {
                generatedModule.Types.Add(BuildStringEnumModel(modelSchema));
            }

            foreach (var modelSchema in model.Schemas.SealedChoices.EmptyIfNull())
            {
                generatedModule.Types.Add(BuildEnumModel(modelSchema));
                generatedModule.Types.Add(BuildEnumExtensions(modelSchema));
            }

            foreach (var operationGroup in model.OperationGroups)
            {
                generatedModule.Types.Add(BuildOperationGroup(operationGroup));
            }

            return generatedModule;
        }

        private GeneratedType BuildOperationGroup(OperationGroup operationGroup)
        {
            var type = new GeneratedClass();
            type.Name = operationGroup.Key + "Client";
            type.Modifiers = TypeModifiers.Partial;
            type.Visibility = Visibility.Internal;
            // type.Namespace = operationGroup.Language.CSharp!.Type!.Namespace!.FullName;

            foreach (var operation in operationGroup.Operations)
            {
                type.Members.Add(BuildOperation(operation));
            }

            return type;
        }

        private GeneratedMember BuildOperation(Operation operation)
        {
            var method = new GeneratedMethod(Visibility.Public, MemberModifiers.None, TypeReference(typeof(string)), operation.Language.Default!.Name)
            {
                Body = new GeneratedStatement(context => context.Write("return ", Constant(null)))
            };

            foreach (var item in operation.Request.Parameters.EmptyIfNull())
            {
                method.Parameters.Add(new GeneratedParameter(TypeReference(item.Schema), item.Language.CSharp!.Name!));
            }

            return method;
        }

        private GeneratedType BuildEnumExtensions(SealedChoiceSchema modelSchema)
        {
            var type = new GeneratedClass();
            type.Name = modelSchema.Language.CSharp!.Type!.Name + "Extensions";
            type.Modifiers = TypeModifiers.Partial;
            type.Visibility = Visibility.Internal;
            type.Namespace = modelSchema.Language.CSharp!.Type!.Namespace!.FullName;

            var parseValueParameter = new GeneratedParameter(TypeReference(typeof(string)), "value");
            type.Members.Add(new GeneratedMethod(Visibility.Public, MemberModifiers.Static, TypeReference(modelSchema), "Parse")
            {
                Parameters = { parseValueParameter },
                Body = new GeneratedStatement(context =>
                {
                    var writer = context.Writer;
                    context.Write(Reference(parseValueParameter), "switch");

                    using(writer.Scope())
                    {
                        foreach (var choise in modelSchema.Choices)
                        {
                            context.Write(Constant(choise.Value), "=>", TypeReference(modelSchema), ".", choise.Language.CSharp!.Name!, ",");
                        }
                    }

                    context.Write("_ => new ", TypeReference(typeof(ArgumentException)), "(nameof(", Reference(parseValueParameter), "),", Reference(parseValueParameter), ",", Constant("Unknown value") , ")");
                })
            });

            var toStringParameterValue = new GeneratedParameter(TypeReference(modelSchema), "value");
            type.Members.Add(new GeneratedMethod(Visibility.Public, MemberModifiers.Static, TypeReference(typeof(string)), "ToString")
            {
                Parameters = { toStringParameterValue },
                Body = new GeneratedStatement(context =>
                {
                    var writer = context.Writer;
                    context.Write(Reference(parseValueParameter), "switch");

                    using (writer.Scope())
                    {
                        foreach (var choise in modelSchema.Choices)
                        {
                            context.Write(TypeReference(modelSchema), ".", choise.Language.CSharp!.Name!, "=>", Constant(choise.Value), ",");
                        }
                    }

                    context.Write("_ => new ", TypeReference(typeof(ArgumentException)), "(nameof(", Reference(parseValueParameter), "),", Reference(parseValueParameter), ",", Constant("Unknown value"), ")");
                })
            });

            return type;
        }

        private GeneratedType BuildEnumModel(SealedChoiceSchema modelSchema)
        {
            var type = new GeneratedEnum();
            type.Name = modelSchema.Language.CSharp!.Type!.Name;
            type.Modifiers = TypeModifiers.Partial;
            type.Visibility = Visibility.Public;
            type.Namespace = modelSchema.Language.CSharp!.Type!.Namespace!.FullName;

            foreach (var modelSchemaChoice in modelSchema.Choices)
            {
                type.Members.Add(new GeneratedEnumMember(modelSchemaChoice.Language.CSharp!.Name)
                {
                    Value = new ConstantExpression(modelSchemaChoice.Value)
                });
            }

            return type;
        }

        private GeneratedType BuildStringEnumModel(ChoiceSchema modelSchema)
        {
            var type = new GeneratedStruct();
            type.Name = modelSchema.Language.CSharp!.Type!.Name;
            type.Modifiers = TypeModifiers.Partial;
            type.Visibility = Visibility.Public;
            type.Namespace = modelSchema.Language.CSharp!.Type!.Namespace!.FullName;

            foreach (var modelSchemaChoice in modelSchema.Choices)
            {
                type.Members.Add(new GeneratedAutoProperty(Visibility.Public, MemberModifiers.Static, TypeReference(modelSchema), modelSchemaChoice.Language.CSharp!.Name)
                {
                    IsReadonly = true,
                    Initializer = new ConstantExpression(modelSchemaChoice.Value)
                });
            }

            var valueField = new GeneratedField(Visibility.Private, MemberModifiers.Readonly, TypeReference(typeof(string)), "_value");

            type.Members.Add(valueField);

            var valueParameter = new GeneratedParameter(TypeReference(typeof(string)), "value");
            type.Members.Add(new GeneratedConstructor(Visibility.Public, type.Name)
            {
                Parameters =
                {
                    valueParameter
                },
                Body = new GeneratedStatement(context =>
                {
                    context.Write(Reference(valueField), "=", Reference(valueParameter), "?? new ", TypeReference(typeof(ArgumentNullException)), "(nameof(", Reference(valueParameter), "));");
                })
            });

            return type;
        }

        private GeneratedType BuildObjectModel(ObjectSchema modelSchema)
        {
            var type = new GeneratedClass();
            type.Name = modelSchema.Language.CSharp!.Type!.Name;
            type.Modifiers = TypeModifiers.Partial;
            type.Visibility = Visibility.Public;
            type.Namespace = modelSchema.Language.CSharp!.Type!.Namespace!.FullName;

            foreach (var property in modelSchema.Properties.EmptyIfNull())
            {
                type.Members.Add(new GeneratedAutoProperty(Visibility.Public, MemberModifiers.None, TypeReference(property.Schema), property.Language.CSharp?.Name ?? "[Name]"));
            }
            return type;
        }

        private TypeReference TypeReference(Schema propertySchema)
        {
            var frameworkType = propertySchema.Type.GetFrameworkType();
            if (frameworkType != null)
            {
                return new FrameworkTypeReference(frameworkType.FrameworkType!);
            }
            return new SchemaTypeReference(propertySchema);
        }

        private ConstantExpression Constant(object? value)
        {
            return new ConstantExpression(value);
        }

        private GeneratedReference Reference(INamedReference reference)
        {
            return new GeneratedReference(reference);
        }

        private TypeReference TypeReference(Type type)
        {
            return new FrameworkTypeReference(type);
        }
    }
}