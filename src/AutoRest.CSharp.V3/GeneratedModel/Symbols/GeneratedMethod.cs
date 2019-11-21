using System.Collections.Generic;

namespace AutoRest.CSharp.V3
{
    public class GeneratedMethod : GeneratedMember
    {
        public GeneratedMethod(Visibility visibility, MemberModifiers modifiers, TypeReference? returnType, string? name) : base(visibility, modifiers, name)
        {
            ReturnType = returnType;
        }

        public List<GeneratedParameter> Parameters { get; } = new List<GeneratedParameter>();

        public TypeReference? ReturnType { get; set; }

        public GeneratedStatement? Body { get; set; }
    }
}