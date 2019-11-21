namespace AutoRest.CSharp.V3
{
    public class ConstructorWriter : MethodWriter<GeneratedConstructor>
    {
        protected override void WritePreambule(GeneratedConstructor model, WriterContext context)
        {
            var writer = context.Writer;
            writer.AppendVisibility(model.Visibility);
            writer.AppendName(model.Name!);
        }
    }
}