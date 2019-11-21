namespace AutoRest.CSharp.V3
{
    public class ReferenceWriter : IWriter<GeneratedReference>
    {
        public void Write(GeneratedReference model, WriterContext context)
        {
            context.Writer.Append(model.Source.Name);
        }
    }
}