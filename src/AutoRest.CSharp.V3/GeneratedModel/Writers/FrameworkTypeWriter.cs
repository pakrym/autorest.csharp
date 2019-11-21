namespace AutoRest.CSharp.V3
{
    public class FrameworkTypeWriter : IWriter<FrameworkTypeReference>
    {
        public void Write(FrameworkTypeReference model, WriterContext context)
        {
            context.Writer.AppendName(model.FrameworkType.ToString());
        }
    }
}