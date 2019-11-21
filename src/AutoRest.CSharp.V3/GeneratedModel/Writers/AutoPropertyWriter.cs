namespace AutoRest.CSharp.V3
{
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
}