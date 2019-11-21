using System;

namespace AutoRest.CSharp.V3
{
    public class FrameworkTypeReference : TypeReference
    {
        public Type FrameworkType { get; }

        public FrameworkTypeReference(Type frameworkType)
        {
            FrameworkType = frameworkType;
        }
    }
}