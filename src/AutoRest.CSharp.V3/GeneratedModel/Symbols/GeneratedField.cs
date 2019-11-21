namespace AutoRest.CSharp.V3
{
    public class GeneratedField : GeneratedMember
    {
        public GeneratedField(Visibility visibility, MemberModifiers modifiers, TypeReference? returnType, string? name) : base(visibility, modifiers, name)
        {
            ReturnType = returnType;
        }

        public TypeReference? ReturnType { get; set; }

        public GeneratedExpression? Value { get; set; }
    }
}