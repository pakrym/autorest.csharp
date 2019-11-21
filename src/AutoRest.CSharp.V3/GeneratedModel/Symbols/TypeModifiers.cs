using System;

namespace AutoRest.CSharp.V3
{
    [Flags]
    public enum TypeModifiers
    {
        None = 0,
        Partial = 1,
        Readonly = Partial << 1,
        Static = Readonly << 1
    }
}