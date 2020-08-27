// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace httpInfrastructure.Models
{
    public partial class MyException
    {
        internal static MyException DeserializeMyException(JsonElement element)
        {
            Optional<string> statusCode = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("statusCode"))
                {
                    statusCode = property.Value.GetString();
                    continue;
                }
            }
            return new MyException(statusCode.Value);
        }
    }
}
