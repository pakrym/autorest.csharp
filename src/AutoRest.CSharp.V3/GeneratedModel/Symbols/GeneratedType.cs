using System.Collections.Generic;

namespace AutoRest.CSharp.V3
{
    public class GeneratedType
    {
        public Visibility Visibility { get; set; } = Visibility.Internal;

        public string? Name { get; set; }

        public string? Namespace { get; set; }

        public TypeReference? BaseType { get; set; }

        public TypeModifiers Modifiers { get; set; }

        public List<GeneratedMember> Members { get; } = new List<GeneratedMember>();
    }
}