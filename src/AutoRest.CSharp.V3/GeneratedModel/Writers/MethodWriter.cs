namespace AutoRest.CSharp.V3
{
    public abstract class MethodWriter<T> : IWriter<T> where T : GeneratedMethod
    {
        public void Write(T model, WriterContext context)
        {
            var writer = context.Writer;
            WritePreambule(model, context);
            writer.Append("(");

            bool first = true;

            foreach (var parameter in model.Parameters)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    writer.Append(",");
                }

                context.Write(parameter);
            }

            writer.Append(")");
            WriterBody(model, context);
        }

        protected abstract void WritePreambule(T model, WriterContext context);

        protected virtual void WriterBody(T model, WriterContext context)
        {
            var writer = context.Writer;
            using (writer.Scope())
            {
                context.Write(model.Body!);
            }
        }
    }
}