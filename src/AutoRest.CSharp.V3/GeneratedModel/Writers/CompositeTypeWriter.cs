namespace AutoRest.CSharp.V3
{
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
}