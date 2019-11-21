namespace AutoRest.CSharp.V3
{
    public class GeneratedParameter : INamedReference
    {
        public GeneratedParameter(TypeReference type, string name)
        {
            Type = type;
            Name = name;
        }

        public string? Name { get; set; }

        public TypeReference? Type { get; set; }
        public ConstantExpression? DefaultValue { get; set; }
    }
}