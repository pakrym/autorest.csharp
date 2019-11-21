using System;
using System.Collections.Generic;

namespace AutoRest.CSharp.V3
{
    public class WriterContext
    {
        private readonly Dictionary<Type, object> _generators;

        public WriterContext(CodeWriter writer, Dictionary<Type, object> generators)
        {
            _generators = generators;

            Writer = writer;
        }

        public CodeWriter Writer { get; }

        public void Write(object? model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            if (!_generators.TryGetValue(model.GetType(), out var generator))
            {
                throw new InvalidOperationException("No generator for " + model.GetType());
            }

            // TODO: Bad
            generator.GetType().GetMethod("Write")!.Invoke(generator, new object?[] { model, this });
        }

        public void Write(params object[] args)
        {
            foreach (var item in args)
            {
                if (item is string s)
                {
                    Writer.Append(s);
                }
                else
                {
                    Write(item);
                }
            }
        }
    }
}