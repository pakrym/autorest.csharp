namespace AutoRest.CSharp.V3
{
    public class FieldWriter : IWriter<GeneratedField>
    {
        public void Write(GeneratedField model, WriterContext context)
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
}