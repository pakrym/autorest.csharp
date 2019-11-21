using System;
using System.Linq;

namespace AutoRest.CSharp.V3
{
    public class GeneratedStatement
    {
        public GeneratedStatement(Action<WriterContext> action)
        {
            Value = action;
        }

        public Action<WriterContext>? Value { get; set; }
    }
}
