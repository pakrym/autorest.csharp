namespace AutoRest.CSharp.V3
{
    public class StatementWriter : IWriter<GeneratedStatement>
    {
        public void Write(GeneratedStatement model, WriterContext context)
        {
            model.Value?.Invoke(context);
        }
    }
}