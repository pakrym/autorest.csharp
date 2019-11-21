namespace AutoRest.CSharp.V3
{
    public abstract class TypeWriter<T> : IWriter<T> where T : GeneratedType
    {
        public void Write(T model, WriterContext context)
        {
            var writer = context.Writer;

            writer.AppendKeyword("namespace");
            writer.AppendName(model.Namespace!);
            using (writer.Scope())
            {
                WriteType(model, context);
            }
        }

        protected abstract void WriteType(T model, WriterContext context);
    }
}