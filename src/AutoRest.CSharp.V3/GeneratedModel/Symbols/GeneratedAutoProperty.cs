namespace AutoRest.CSharp.V3
{
    public class GeneratedAutoProperty : GeneratedProperty
    {
        public GeneratedAutoProperty(Visibility visibility, MemberModifiers modifiers, TypeReference? returnType, string? name) : base(visibility, modifiers, returnType, name)
        {
        }

        public bool IsReadonly { get; set; }
        public GeneratedExpression? Initializer { get; set; }
    }
}