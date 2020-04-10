// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using AutoRest.CSharp.V3.Input;
using AutoRest.CSharp.V3.Output.Models.Requests;
using AutoRest.CSharp.V3.Output.Models.Types;
using AutoRest.CSharp.V3.Utilities;

namespace AutoRest.CSharp.V3.Generation.Writers
{
    internal class ModelFactoryWriter
    {
        public static void WriteModelFactory(CodeWriter writer, BuildContext context)
        {
            using (writer.Namespace($"{context.DefaultNamespace}.Models"))
            {
                writer.WriteXmlDocumentationSummary("Model factory for mocking.");
                using (writer.Scope($"public static partial class ModelFactory"))
                {
                    foreach (var model in context.Library.Models.OfType<ObjectType>())
                    {
                        // Generate for public models without public constructors
                        if (model.Declaration.Accessibility == "public" &&
                            model.Constructors.Any(c => c.Declaration.Accessibility == "public"))
                        {
                            continue;
                        }

                        writer.WriteXmlDocumentationSummary($"Creates a new instance of {model.Type.Name} for mocking.");

                        foreach (var objectType in model.EnumerateHierarchy())
                        {
                            foreach (var property in objectType.Properties)
                            {
                                if (property.Declaration.Accessibility == "public" ||
                                    objectType.AdditionalPropertiesProperty == property)
                                {
                                    writer.WriteXmlDocumentationParameter(property.Declaration.Name.ToVariableName(), property.Description);
                                }
                            }
                        }

                        writer.Append($"public static {model.Type} {model.Type.Name:I}(");

                        List<ObjectPropertyInitializer> initializers = new List<ObjectPropertyInitializer>();

                        foreach (var objectType in model.EnumerateHierarchy())
                        {
                            foreach (var property in objectType.Properties)
                            {
                                if (property.Declaration.Accessibility == "public" ||
                                    objectType.AdditionalPropertiesProperty == property)
                                {
                                    var parameterName = property.Declaration.Name.ToVariableName();

                                    writer.Append($"{property.Declaration.Type} {parameterName}, ");
                                    initializers.Add(new ObjectPropertyInitializer(property, new Reference(parameterName, property.Declaration.Type)));
                                }
                            }
                        }


                        writer.RemoveTrailingComma();
                        writer.Append($") => ");
                        writer.WriteInitialization(model, initializers);
                        writer.Line($";");

                        writer.Line();
                    }
                }
            }
        }
    }
}