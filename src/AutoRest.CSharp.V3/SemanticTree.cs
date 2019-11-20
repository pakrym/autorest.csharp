using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoRest.CSharp.V3.Pipeline;
using AutoRest.CSharp.V3.Pipeline.Generated;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Formatting;

namespace AutoRest.CSharp.V3
{
    public class CodeWriter
    {
        private readonly StringBuilder _codeBuilder = new StringBuilder();

        public CodeWriter Append(string? s)
        {
            _codeBuilder.Append(s);
            return this;
        }

        public CodeWriter AppendLine(string? s)
        {
            _codeBuilder.AppendLine(s);
            return this;
        }

        public CodeWriter AppendKeyword(string s)
        {
            return Append(s).Append(" ");
        }

        public CodeWriterScope Scope()
        {
            AppendLine("{");
            return new CodeWriterScope(this);
        }

        public class CodeWriterScope : IDisposable
        {
            private readonly CodeWriter _codeWriter;

            public CodeWriterScope(CodeWriter codeWriter)
            {
                _codeWriter = codeWriter;
            }

            public void Dispose()
            {
                _codeWriter.Append("}");
            }
        }

        public CodeWriter AppendName(string modelName)
        {
            return Append(modelName).Append(" ");
        }

        public CodeWriter Semicolon()
        {
            return Append(";");
        }

        public override string ToString()
        {
            return _codeBuilder.ToString();
        }

        public string ToFormattedCode()
        {
            var syntax = SyntaxFactory.ParseCompilationUnit(ToString());
            return Formatter.Format(syntax, new AdhocWorkspace()).ToFullString();
        }

        public CodeWriter AppendLiteral(object? value)
        {
            switch (value)
            {
                case null:
                    return Append("null");
                case string s:
                    return Append($"\"{s}\"");
                default:
                    return Append(value.ToString()!);
            }
        }
    }

    public static class CodeWriterExtensions
    {
        public static CodeWriter AppendVisibility(this CodeWriter writer, Visibility visibility)
        {
            return writer.AppendKeyword(visibility.ToString().ToLowerInvariant());
        }

        public static CodeWriter AppendModifiers(this CodeWriter writer, TypeModifiers modifiers)
        {
            if (modifiers == TypeModifiers.None)
            {
                return writer;
            }
            return writer.AppendKeyword(modifiers.ToString().ToLowerInvariant());
        }

        public static CodeWriter AppendModifiers(this CodeWriter writer, MemberModifiers modifiers)
        {
            if (modifiers == MemberModifiers.None)
            {
                return writer;
            }

            return writer.AppendKeyword(modifiers.ToString().ToLowerInvariant());
        }
    }

    public abstract class PropertyWriter<T> : IWriter<T> where T : GeneratedProperty
    {
        public void Write(T model, WriterContext context)
        {
            var writer = context.Writer;

            writer.AppendVisibility(model.Visibility);
            writer.AppendModifiers(model.Modifiers);

            context.Write(model.ReturnType);

            writer.AppendName(model.Name!);

            WriteAccessors(model, context);
        }

        protected abstract void WriteAccessors(T model, WriterContext context);
    }

    public class AutoPropertyWriter : PropertyWriter<GeneratedAutoProperty>
    {
        protected override void WriteAccessors(GeneratedAutoProperty model, WriterContext context)
        {
            var writer = context.Writer;

            using (writer.Scope())
            {
                writer.AppendKeyword("get");
                writer.Semicolon();
                if (!model.IsReadonly)
                {
                    writer.AppendKeyword("set");
                    writer.Semicolon();
                }
            }

            if (model.Initializer != null)
            {
                writer.Append("=");
                context.Write(model.Initializer);
                writer.Semicolon();
            }
        }
    }

    public class ModuleWriter : IWriter<GeneratedModule>
    {
        public void Write(GeneratedModule model, WriterContext context)
        {
            foreach (var type in model.Types)
            {
                context.Write(type);
            }
        }
    }

    public abstract class TypeWriter<T> : IWriter<T> where T : GeneratedType
    {
        public void Write(T model, WriterContext context)
        {
            var writer = context.Writer;

            writer.AppendKeyword("namespace");
            writer.AppendName(model.Namespace!);
            using (writer.Scope())
            {
                WriteType(model, context);
            }
        }

        protected abstract void WriteType(T model, WriterContext context);
    }

    public abstract class CompositeTypeWriter<T> : TypeWriter<T> where T : GeneratedType
    {
        protected override void WriteType(T model, WriterContext context)
        {
            var writer = context.Writer;

            writer.AppendVisibility(model.Visibility);
            writer.AppendModifiers(model.Modifiers);
            writer.AppendKeyword(TypeKind);
            writer.AppendName(model.Name!);

            if (model.BaseType != null)
            {
                writer.Append(":");
                context.Write(model.BaseType);
            }

            using (writer.Scope())
            {
                foreach (var member in model.Members)
                {
                    context.Write(member);
                }
            }
        }

        protected abstract string TypeKind { get; }
    }

    public class StructWriter : CompositeTypeWriter<GeneratedStruct>
    {
        protected override string TypeKind => "struct";
    }

    public class ClassWriter : CompositeTypeWriter<GeneratedClass>
    {
        protected override string TypeKind => "class";
    }

    public class EnumWriter : CompositeTypeWriter<GeneratedEnum>
    {
        protected override string TypeKind => "enum";
    }

    public interface INamedReference
    {
        string? Name { get; }
    }

    public class GeneratedFieldMember : GeneratedMember
    {
        public TypeReference? ReturnType { get; set; }

        public GeneratedExpression? Value { get; set; }
    }

    public class FieldWriter : IWriter<GeneratedFieldMember>
    {
        public void Write(GeneratedFieldMember model, WriterContext context)
        {
            var writer = context.Writer;

            writer.AppendVisibility(model.Visibility);
            writer.AppendModifiers(model.Modifiers);

            context.Write(model.ReturnType);

            writer.AppendName(model.Name!);

            if (model.Value != null)
            {
                writer.Append("=");
                context.Write(model.Value);
            }
            writer.Semicolon();
        }
    }

    public class GeneratedEnumMember : GeneratedMember
    {
        public GeneratedExpression? Value;
    }

    public class EnumMemberWriter : IWriter<GeneratedEnumMember>
    {
        public void Write(GeneratedEnumMember model, WriterContext context)
        {
            var writer = context.Writer;

            writer.AppendName(model.Name!);
            writer.Append(",");
        }
    }

    public static class CollectionExtensions
    {
        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T>? enumerable)
        {
            return enumerable ?? Array.Empty<T>();
        }
    }

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
            var method = new GeneratedMethod()
            {
                Visibility = Visibility.Public,
                Name = operation.Language.Default!.Name,
                ReturnType = CreateTypeReference(typeof(string)),
                Body = new GeneratedStatement(context => context.Write("return ", Constant(null)))
            };

            foreach (var item in operation.Request.Parameters.EmptyIfNull())
            {
                method.Parameters.Add(new GeneratedParameter(CreateTypeReference(item.Schema), item.Language.CSharp!.Name!));
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

            var parseValueParameter = new GeneratedParameter(CreateTypeReference(typeof(string)), "value");
            type.Members.Add(new GeneratedMethod()
            {
                Modifiers = MemberModifiers.Static,
                Name = "Parse",
                ReturnType = CreateTypeReference(modelSchema),
                Visibility = Visibility.Public,
                Parameters = { parseValueParameter },
                Body = new GeneratedStatement(context =>
                {
                    var writer = context.Writer;
                    context.Write(CreateReference(parseValueParameter), "switch");

                    using(writer.Scope())
                    {
                        foreach (var choise in modelSchema.Choices)
                        {
                            context.Write(Constant(choise.Value), "=>", CreateTypeReference(modelSchema), ".", choise.Language.CSharp!.Name!, ",");
                        }
                    }

                    context.Write("_ => new ", CreateTypeReference(typeof(ArgumentException)), "(nameof(", CreateReference(parseValueParameter), "),", CreateReference(parseValueParameter), ",", Constant("Unknown value") , ")");
                })
            });

            var toStringParameterValue = new GeneratedParameter(CreateTypeReference(modelSchema), "value");
            type.Members.Add(new GeneratedMethod()
            {
                Modifiers = MemberModifiers.Static,
                Name = "ToString",
                ReturnType = CreateTypeReference(typeof(string)),
                Visibility = Visibility.Public,
                Parameters = { toStringParameterValue },
                Body = new GeneratedStatement(context =>
                {
                    var writer = context.Writer;
                    context.Write(CreateReference(parseValueParameter), "switch");

                    using (writer.Scope())
                    {
                        foreach (var choise in modelSchema.Choices)
                        {
                            context.Write(CreateTypeReference(modelSchema), ".", choise.Language.CSharp!.Name!, "=>", Constant(choise.Value), ",");
                        }
                    }

                    context.Write("_ => new ", CreateTypeReference(typeof(ArgumentException)), "(nameof(", CreateReference(parseValueParameter), "),", CreateReference(parseValueParameter), ",", Constant("Unknown value"), ")");
                })
            });

            return type;
        }

        private ConstantExpression Constant(object? value)
        {
            return new ConstantExpression(value);
        }

        private GeneratedReference CreateReference(INamedReference reference)
        {
            return new GeneratedReference(reference);
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
                type.Members.Add(new GeneratedEnumMember()
                {
                    Name = modelSchemaChoice.Language.CSharp!.Name,
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
                type.Members.Add(new GeneratedAutoProperty()
                {
                    IsReadonly = true,
                    Modifiers = MemberModifiers.Static,
                    Name = modelSchemaChoice.Language.CSharp!.Name,
                    ReturnType = CreateTypeReference(modelSchema),
                    Visibility = Visibility.Public,
                    Initializer = new ConstantExpression(modelSchemaChoice.Value)
                });
            }

            var valueField = new GeneratedFieldMember()
            {
                Modifiers = MemberModifiers.Readonly,
                Visibility = Visibility.Private,
                Name = "_value",
                ReturnType = CreateTypeReference(typeof(string))
            };

            type.Members.Add(valueField);

            var valueParameter = new GeneratedParameter(CreateTypeReference(typeof(string)), "value");
            type.Members.Add(new GeneratedConstructor()
            {
                ReturnType = CreateTypeReference(modelSchema),
                Visibility = Visibility.Public,
                Name = type.Name,
                Parameters =
                {
                    valueParameter
                },
                Body = new GeneratedStatement(context =>
                   {
                       context.Write(CreateReference(valueField), "=", CreateReference(valueParameter), "?? new ", CreateTypeReference(typeof(ArgumentNullException)), "(nameof(", CreateReference(valueParameter), "));");
                   })
            });

            return type;
        }

        private TypeReference CreateTypeReference(Type type)
        {
            return new FrameworkTypeReference(type);
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
                type.Members.Add(new GeneratedAutoProperty()
                {
                    Visibility = Visibility.Public,
                    Name = property.Language.CSharp?.Name ?? "[Name]",
                    ReturnType = CreateTypeReference(property.Schema)
                });
            }
            return type;
        }

        private TypeReference CreateTypeReference(Schema propertySchema)
        {
            var frameworkType = propertySchema.Type.GetFrameworkType();
            if (frameworkType != null)
            {
                return new FrameworkTypeReference(frameworkType.FrameworkType!);
            }
            return new SchemaTypeReference(propertySchema);
        }
    }

    public class GeneratedReference
    {
        public GeneratedReference(INamedReference source)
        {
            Source = source;
        }

        public INamedReference Source { get; }
    }

    public class ReferenceWriter : IWriter<GeneratedReference>
    {
        public void Write(GeneratedReference model, WriterContext context)
        {
            context.Writer.Append(model.Source.Name);
        }
    }

    public class FrameworkTypeReference : TypeReference
    {
        public Type FrameworkType { get; }

        public FrameworkTypeReference(Type frameworkType)
        {
            FrameworkType = frameworkType;
        }
    }


    public class FrameworkTypeWriter : IWriter<FrameworkTypeReference>
    {
        public void Write(FrameworkTypeReference model, WriterContext context)
        {
            context.Writer.AppendName(model.FrameworkType.ToString());
        }
    }

    public class SchemaTypeReference : TypeReference
    {
        internal Schema Schema { get; }

        internal SchemaTypeReference(Schema schema)
        {
            Schema = schema;
        }
    }

    public interface IWriter<in T>
    {
        void Write(T model, WriterContext context);
    }

    public class SchemaTypeWriter : IWriter<SchemaTypeReference>
    {
        public void Write(SchemaTypeReference model, WriterContext context)
        {
            context.Writer.AppendName(model.Schema.Language.CSharp?.Type!.FullName ?? "[Name]");
        }
    }

    public class WriterContext
    {
        private readonly Dictionary<Type, object> _generators;

        public WriterContext(CodeWriter writer, Dictionary<Type, object> generators)
        {
            _generators = generators;

            Writer = writer;
        }

        public CodeWriter Writer { get; }

        public void Write(object? model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (!_generators.TryGetValue(model.GetType(), out var generator))
            {
                throw new InvalidOperationException("No generator for " + model.GetType());
            }

            // TODO: Bad
            generator.GetType().GetMethod("Write")!.Invoke(generator, new object?[] { model, this });
        }

        public void Write(params object[] args)
        {
            foreach (var item in args)
            {
                if (item is string s)
                {
                    Writer.Append(s);
                }
                else
                {
                    Write(item);
                }
            }
        }
    }

    public enum Visibility
    {
        Public,
        Internal,
        Private,
        Protected
    }

    [Flags]
    public enum TypeModifiers
    {
        None = 0,
        Partial = 1,
        Readonly = Partial << 1,
        Static = Readonly << 1
    }

    [Flags]
    public enum MemberModifiers
    {
        None = 0,
        Partial = 1,
        Static = Partial << 1,
        Readonly = Static << 1,
        Virtual = Readonly << 1,
        Override = Virtual << 1,
    }

    public class TypeReference
    {
    }

    public class GeneratedModule
    {
        public List<GeneratedType> Types { get; } = new List<GeneratedType>();
    }

    public class GeneratedConstructor : GeneratedMethod
    {
    }

    public class GeneratedStatement
    {
        public GeneratedStatement(Action<WriterContext> action)
        {
            Value = action;
        }

        public Action<WriterContext>? Value { get; set; }
    }

    public class StatementWriter : IWriter<GeneratedStatement>
    {
        public void Write(GeneratedStatement model, WriterContext context)
        {
            model.Value?.Invoke(context);
        }
    }

    public class ParameterWriter : IWriter<GeneratedParameter>
    {
        public void Write(GeneratedParameter model, WriterContext context)
        {
            context.Write(model.Type);
            context.Writer.Append(model.Name!);

            if (model.DefaultValue != null)
            {
                context.Writer.Append("=");

                context.Write(model.DefaultValue);
            }
        }
    }

    public class SimpleMethodWriter : MethodWriter<GeneratedMethod>
    {
        protected override void WritePreambule(GeneratedMethod model, WriterContext context)
        {
            var writer = context.Writer;
            writer.AppendVisibility(model.Visibility);
            writer.AppendModifiers(model.Modifiers);

            context.Write(model.ReturnType);
            writer.AppendName(model.Name!);
        }
    }

    public abstract class MethodWriter<T> : IWriter<T> where T : GeneratedMethod
    {
        public void Write(T model, WriterContext context)
        {
            var writer = context.Writer;
            WritePreambule(model, context);
            writer.Append("(");

            bool first = true;

            foreach (var parameter in model.Parameters)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    writer.Append(",");
                }

                context.Write(parameter);
            }

            writer.Append(")");
            WriterBody(model, context);
        }

        protected abstract void WritePreambule(T model, WriterContext context);

        protected virtual void WriterBody(T model, WriterContext context)
        {
            var writer = context.Writer;
            using (writer.Scope())
            {
                context.Write(model.Body!);
            }
        }
    }


    public class ConstructorWriter : MethodWriter<GeneratedConstructor>
    {
        protected override void WritePreambule(GeneratedConstructor model, WriterContext context)
        {
            var writer = context.Writer;
            writer.AppendVisibility(model.Visibility);
            writer.AppendName(model.Name!);
        }
    }

    public class GeneratedMethod : GeneratedMember
    {
        public List<GeneratedParameter> Parameters { get; } = new List<GeneratedParameter>();

        public TypeReference? ReturnType { get; set; }

        public GeneratedStatement? Body { get; set; }
    }

    public class GeneratedAutoProperty : GeneratedProperty
    {
        public bool IsReadonly { get; set; }
        public GeneratedExpression? Initializer { get; set; }
    }

    public class GeneratedExpression
    {
    }


    public class ConstantExpression : GeneratedExpression
    {
        public object? Value { get; }

        public ConstantExpression(object? value)
        {
            Value = value;
        }
    }

    public class ConstantExpressionWriter : IWriter<ConstantExpression>
    {
        public void Write(ConstantExpression model, WriterContext context)
        {
            context.Writer.AppendLiteral(model.Value);
        }
    }

    public abstract class GeneratedProperty : GeneratedMember
    {
        public TypeReference? ReturnType { get; set; }
    }

    public class GeneratedParameter : INamedReference
    {
        public GeneratedParameter(TypeReference type, string name)
        {
            Type = type;
            Name = name;
        }

        public string? Name { get; set; }

        public TypeReference? Type { get; set; }
        public ConstantExpression? DefaultValue { get; set; }
    }

    public class GeneratedMember : INamedReference
    {
        public MemberModifiers Modifiers { get; set; }
        public string? Name { get; set; }
        public Visibility Visibility { get; set; } = Visibility.Internal;
    }

    public class GeneratedType
    {
        public Visibility Visibility { get; set; } = Visibility.Internal;

        public string? Name { get; set; }

        public string? Namespace { get; set; }

        public TypeReference? BaseType { get; set; }

        public TypeModifiers Modifiers { get; set; }

        public List<GeneratedMember> Members { get; } = new List<GeneratedMember>();
    }

    public class GeneratedClass : GeneratedType
    {
    }

    public class GeneratedStruct : GeneratedType
    {
    }

    public class GeneratedEnum : GeneratedType
    {
    }
}
