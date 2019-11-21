﻿using System.Linq;
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

            var schemas = (codeModel.Schemas.Choices ?? Enumerable.Empty<ChoiceSchema>()).Cast<Schema>()
                .Concat(codeModel.Schemas.SealedChoices ?? Enumerable.Empty<SealedChoiceSchema>())
                .Concat(codeModel.Schemas.Objects ?? Enumerable.Empty<ObjectSchema>());
            foreach (var schema in schemas)
            {
                var writer = new SchemaWriter();
                writer.WriteSchema(schema);
                await autoRest.WriteFile($"Generated/Models/{schema.Language.CSharp?.Name}.cs", writer.ToFormattedCode(), "source-file-csharp");
            }

            // CodeModel for debugging
            await autoRest.WriteFile($"CodeModel-{configuration.Title}.yaml", codeModel.Serialize(), "source-file-csharp");

            return true;
        }

        public bool ReserializeCodeModel => false;
    }
}