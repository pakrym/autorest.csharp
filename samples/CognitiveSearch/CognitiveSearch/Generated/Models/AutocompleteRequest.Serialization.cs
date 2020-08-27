// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace CognitiveSearch.Models
{
    public partial class AutocompleteRequest : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("search");
            writer.WriteStringValue(SearchText);
            if (Optional.IsDefined(AutocompleteMode))
            {
                writer.WritePropertyName("autocompleteMode");
                writer.WriteStringValue(AutocompleteMode.Value.ToSerialString());
            }
            if (Optional.IsDefined(Filter))
            {
                writer.WritePropertyName("filter");
                writer.WriteStringValue(Filter);
            }
            if (Optional.IsDefined(UseFuzzyMatching))
            {
                writer.WritePropertyName("fuzzy");
                writer.WriteBooleanValue(UseFuzzyMatching.Value);
            }
            if (Optional.IsDefined(HighlightPostTag))
            {
                writer.WritePropertyName("highlightPostTag");
                writer.WriteStringValue(HighlightPostTag);
            }
            if (Optional.IsDefined(HighlightPreTag))
            {
                writer.WritePropertyName("highlightPreTag");
                writer.WriteStringValue(HighlightPreTag);
            }
            if (Optional.IsDefined(MinimumCoverage))
            {
                writer.WritePropertyName("minimumCoverage");
                writer.WriteNumberValue(MinimumCoverage.Value);
            }
            if (Optional.IsDefined(SearchFields))
            {
                writer.WritePropertyName("searchFields");
                writer.WriteStringValue(SearchFields);
            }
            writer.WritePropertyName("suggesterName");
            writer.WriteStringValue(SuggesterName);
            if (Optional.IsDefined(Top))
            {
                writer.WritePropertyName("top");
                writer.WriteNumberValue(Top.Value);
            }
            writer.WriteEndObject();
        }
    }
}
