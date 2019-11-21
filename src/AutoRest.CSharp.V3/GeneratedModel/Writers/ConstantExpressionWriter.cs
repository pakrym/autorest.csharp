namespace AutoRest.CSharp.V3
{
    public class ConstantExpressionWriter : IWriter<ConstantExpression>
    {
        public void Write(ConstantExpression model, WriterContext context)
        {
            context.Writer.AppendLiteral(model.Value);
        }
    }
}