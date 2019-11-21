using System;

namespace AutoRest.CSharp.V3
{
    [Flags]
    public enum MemberModifiers
    {
        None = 0,
        Partial = 1,
        Static = Partial << 1,
        Readonly = Static << 1,
        Virtual = Readonly << 1,
        Override = Virtual << 1,
    }
}