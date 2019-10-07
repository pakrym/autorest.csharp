﻿using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoRest.CSharp.V3.JsonRpc;
using AutoRest.CSharp.V3.Pipeline;
using AutoRest.CSharp.V3.Utilities;

namespace AutoRest.CSharp.V3
{
    internal static class PluginProcessor
    {
        public static string[] PluginNames { get; } = { "csharp-v3" };

        public static async Task<bool> Start(AutoRestInterface autoRest)
        {
            // AutoRest sends an empty Object as a 'true' value. When the configuration item is not present, it sends a Null value.
            if ((await autoRest.GetValue<JsonElement?>($"{autoRest.PluginName}.debugger")).IsObject())
            {
                DebuggerAwaiter.Await();
            }
            try
            {
                var codeModelFile = (await autoRest.ListInputs()).FirstOrDefault();
                if (codeModelFile.IsNullOrEmpty()) throw new Exception("Generator did not receive the code model file.");

                var codeModelYaml = await autoRest.ReadFile(codeModelFile);
                //codeModelYaml = String.Join(Environment.NewLine, codeModelYaml.ToLines().Where(l => !l.Contains("uid:")));
                //codeModelYaml = Regex.Replace(codeModelYaml, @"(.*)!<!.*>(.*)", "$1$2", RegexOptions.Multiline);
                //codeModelYaml = codeModelYaml.Replace("<!CodeModel>", "<CodeModel>");
                //codeModelYaml = codeModelYaml.Replace("!<!CodeModel>", "");
                //codeModelYaml = codeModelYaml.Replace("primitives:", "primitives: !<!Primitives>");
                //codeModelYaml = codeModelYaml.Replace("https: ", "https:");
                //codeModelYaml = codeModelYaml.Replace("!<!Metadata>", "!<!OperationGroup>");
                //codeModelYaml = String.Join(Environment.NewLine, codeModelYaml.ToLines());
                //codeModelYaml = codeModelYaml.Replace($"{Environment.NewLine}    x-ms-metadata:{Environment.NewLine}      - url: 'https: //xkcd.com/json.html'", String.Empty);
                //codeModelYaml = codeModelYaml.Replace("          internal: true", $"          internal: true{Environment.NewLine}          coolCat: 'make me some bacon'");

                var inputFile = (await autoRest.GetValue<string[]>("input-file")).FirstOrDefault();
                await autoRest.Message(new Message { Channel = Channel.Fatal, Text = inputFile });
                var fileName = $"CodeModel-{Path.GetFileNameWithoutExtension(inputFile)}.yaml";
                await autoRest.WriteFile(fileName, codeModelYaml, "source-file-csharp");

                var codeModel = Serialization.DeserializeCodeModel(codeModelYaml);
                var fileName2 = $"CodeModel-{Path.GetFileNameWithoutExtension(inputFile)}-Serial.yaml";
                var codeModelYamlSerial = codeModel.Serialize();
                await autoRest.WriteFile(fileName2, codeModelYamlSerial, "source-file-csharp");
                var codeModel2 = Serialization.DeserializeCodeModel(codeModelYamlSerial);
                var codeModelYamlDeserial = codeModel2.Serialize();
                var fileName3 = $"CodeModel-{Path.GetFileNameWithoutExtension(inputFile)}-Deserial.yaml";
                await autoRest.WriteFile(fileName3, codeModelYamlDeserial, "source-file-csharp");

                return true;
            }
            catch (Exception e)
            {
                await autoRest.Message(new Message { Channel = Channel.Fatal, Text = e.ToString() });
                return false;
            }
        }
    }
}
