namespace AutoRest.CSharp.V3
{
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
}