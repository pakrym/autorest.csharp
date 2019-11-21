namespace AutoRest.CSharp.V3
{
    public class EnumMemberWriter : IWriter<GeneratedEnumMember>
    {
        public void Write(GeneratedEnumMember model, WriterContext context)
        {
            var writer = context.Writer;

            writer.AppendName(model.Name!);
            writer.Append(",");
        }
    }
}