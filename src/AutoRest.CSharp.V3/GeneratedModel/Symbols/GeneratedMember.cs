namespace AutoRest.CSharp.V3
{
    public class GeneratedMember : INamedReference
    {
        public GeneratedMember(Visibility visibility, MemberModifiers modifiers, string? name)
        {
            Visibility = visibility;
            Modifiers = modifiers;
            Name = name;
        }

        public MemberModifiers Modifiers { get; set; }
        public string? Name { get; set; }
        public Visibility Visibility { get; set; }
    }
}