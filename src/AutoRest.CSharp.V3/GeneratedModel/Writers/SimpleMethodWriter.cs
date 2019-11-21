namespace AutoRest.CSharp.V3
{
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
}