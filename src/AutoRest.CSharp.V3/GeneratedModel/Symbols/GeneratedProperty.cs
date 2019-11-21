namespace AutoRest.CSharp.V3
{
    public abstract class GeneratedProperty : GeneratedMember
    {
        protected GeneratedProperty(Visibility visibility, MemberModifiers modifiers, TypeReference? returnType, string? name) : base(visibility, modifiers, name)
        {
            ReturnType = returnType;
        }

        public TypeReference? ReturnType { get; set; }

    }
}