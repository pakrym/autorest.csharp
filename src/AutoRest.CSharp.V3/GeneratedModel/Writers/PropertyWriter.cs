namespace AutoRest.CSharp.V3
{
    public abstract class PropertyWriter<T> : IWriter<T> where T : GeneratedProperty
    {
        public void Write(T model, WriterContext context)
        {
            var writer = context.Writer;

            writer.AppendVisibility(model.Visibility);
            writer.AppendModifiers(model.Modifiers);

            context.Write(model.ReturnType);

            writer.AppendName(model.Name!);

            WriteAccessors(model, context);
        }

        protected abstract void WriteAccessors(T model, WriterContext context);
    }
}