using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoRest.CSharp.V3.CodeGen;
using AutoRest.CSharp.V3.JsonRpc;
using AutoRest.CSharp.V3.Pipeline;
using AutoRest.CSharp.V3.Pipeline.Generated;

namespace AutoRest.CSharp.V3.Plugins
{
    [PluginName("cs-modeler")]
    internal class Modeler : IPlugin
    {
        public async Task<bool> Execute(AutoRestInterface autoRest, CodeModel codeModel, Configuration configuration)
        {
            // Every schema for debugging
            //foreach (var schema in codeModel.Schemas.GetAllSchemaNodes())
            //{
            //    var writer = new SchemaWriter();
            //    writer.WriteSchema(schema);
            //    await autoRest.WriteFile($"All/{schema.Language.CSharp?.Name}.cs", writer.ToFormattedCode(), "source-file-csharp");
            //}
            
            var modelBuilder = new ModelBuilder();
            var module = modelBuilder.Build(codeModel);


            var writers = GetWriters();
            var writer = new CodeWriter();

            var writerContext = new WriterContext(writer, writers);

            writerContext.Write(module);

            await autoRest.WriteFile($"generated.cs", writer.ToFormattedCode(), "source-file-csharp");

            // CodeModel for debugging
            await autoRest.WriteFile($"CodeModel-{configuration.Title}.yaml", codeModel.Serialize(), "source-file-csharp");

            return true;
        }

        private static Dictionary<Type, object> GetWriters()
        {
            Dictionary<Type, object> writers = new Dictionary<Type, object>();
            foreach (var type in typeof(Modeler).Assembly.GetTypes().Where(t=>!t.IsAbstract))
            {
                foreach (var iface in type.GetInterfaces())
                {
                    if (iface.IsGenericType && iface.GetGenericTypeDefinition() == typeof(IWriter<>))
                    {
                        writers[iface.GetGenericArguments().Single()] = Activator.CreateInstance(type)!;
                    }
                }
            }

            return writers;
        }

        public bool ReserializeCodeModel => false;
    }
}
