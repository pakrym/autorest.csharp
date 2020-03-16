// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.AI.FormRecognizer.Models
{
    public partial class FormFieldsReport
    {
        internal static FormFieldsReport DeserializeFormFieldsReport(JsonElement element)
        {
            FormFieldsReport result = new FormFieldsReport();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("fieldName"))
                {
                    result.FieldName = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("accuracy"))
                {
                    result.Accuracy = property.Value.GetSingle();
                    continue;
                }
            }
            return result;
        }
    }
}
