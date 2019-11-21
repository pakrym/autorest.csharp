namespace AutoRest.CSharp.V3
{
    public class ConstantExpression : GeneratedExpression
    {
        public object? Value { get; }

        public ConstantExpression(object? value)
        {
            Value = value;
        }
    }
}