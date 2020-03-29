// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoRest.CSharp.V3.AutoRest.Communication;
using AutoRest.CSharp.V3.Generation.Writers;
using AutoRest.CSharp.V3.Input;
using AutoRest.CSharp.V3.Input.Source;
using AutoRest.CSharp.V3.Output.Models.Responses;
using AutoRest.CSharp.V3.Output.Models.Types;

namespace AutoRest.CSharp.V3.AutoRest.Plugins
{
    [PluginName("csharpgen")]
    internal class CSharpGen : IPlugin
    {
        public async Task<bool> Execute(IPluginCommunication autoRest, Configuration configuration)
        {
            Directory.CreateDirectory(configuration.OutputFolder);
            var codeModelTask = Task.Run(() => LoadCodeModel(autoRest, configuration));
            var project = GeneratedCodeWorkspace.Create(configuration.OutputFolder, configuration.SharedSourceFolder);
            var sourceInputModel = SourceInputModelBuilder.Build(await project.GetCompilationAsync());

            var context = new BuildContext(await codeModelTask, configuration, sourceInputModel);

            var modelWriter = new ModelWriter();
            var clientWriter = new ClientWriter();
            var restClientWriter = new RestClientWriter();
            var serializeWriter = new SerializationWriter();
            var headerModelModelWriter = new ResponseHeaderGroupWriter();

            foreach (var model in context.Library.Models)
            {
                var codeWriter = new CodeWriter();
                modelWriter.WriteModel(codeWriter, model);

                var serializerCodeWriter = new CodeWriter();
                serializeWriter.WriteSerialization(serializerCodeWriter, model);

                var name = model.Type.Name;
                project.AddGeneratedFile($"Models/{name}.cs", codeWriter.ToString());
                project.AddGeneratedFile($"Models/{name}.Serialization.cs", serializerCodeWriter.ToString());
            }

            foreach (var client in context.Library.RestClients)
            {
                var restCodeWriter = new CodeWriter();
                restClientWriter.WriteClient(restCodeWriter, client);

                project.AddGeneratedFile($"Operations/{client.Type.Name}.cs", restCodeWriter.ToString());

                var headerModels = client.Methods.Select(m => m.HeaderModel).OfType<ResponseHeaderGroupType>().Distinct();
                foreach (ResponseHeaderGroupType responseHeaderModel in headerModels)
                {
                    var headerModelCodeWriter = new CodeWriter();
                    headerModelModelWriter.WriteHeaderModel(headerModelCodeWriter, responseHeaderModel);

                    project.AddGeneratedFile($"Operations/{responseHeaderModel.Type.Name}.cs", headerModelCodeWriter.ToString());
                }
            }

            foreach (var client in context.Library.Clients)
            {
                var codeWriter = new CodeWriter();
                clientWriter.WriteClient(codeWriter, client);

                project.AddGeneratedFile($"Operations/{client.Type.Name}.cs", codeWriter.ToString());
            }

            await foreach (var file in project.GetGeneratedFilesAsync())
            {
                await autoRest.WriteFile(file.Name, file.Text, "source-file-csharp");
            }

            return true;
        }

        private async Task<CodeModel> LoadCodeModel(IPluginCommunication autoRest, Configuration configuration)
        {
            string codeModelFileName = (await autoRest.ListInputs()).FirstOrDefault();
            if (string.IsNullOrEmpty(codeModelFileName)) throw new Exception("Generator did not receive the code model file.");

            string codeModelYaml = await autoRest.ReadFile(codeModelFileName);

            if (configuration.SaveCodeModel)
            {
                await autoRest.WriteFile("CodeModel.yaml", codeModelYaml, "source-file-csharp");
            }

            return CodeModelSerialization.DeserializeCodeModel(codeModelYaml);
        }
    }
}
