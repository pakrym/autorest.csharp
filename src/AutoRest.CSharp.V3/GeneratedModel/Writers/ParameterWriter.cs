namespace AutoRest.CSharp.V3
{
    public class ParameterWriter : IWriter<GeneratedParameter>
    {
        public void Write(GeneratedParameter model, WriterContext context)
        {
            context.Write(model.Type);
            context.Writer.Append(model.Name!);

            if (model.DefaultValue != null)
            {
                context.Writer.Append("=");

                context.Write(model.DefaultValue);
            }
        }
    }
}