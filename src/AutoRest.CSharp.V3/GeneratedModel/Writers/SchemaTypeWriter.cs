namespace AutoRest.CSharp.V3
{
    public class SchemaTypeWriter : IWriter<SchemaTypeReference>
    {
        public void Write(SchemaTypeReference model, WriterContext context)
        {
            context.Writer.AppendName(model.Schema.Language.CSharp?.Type!.FullName ?? "[Name]");
        }
    }
}