// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections;
using System.Collections.Generic;
using Azure.Core;

namespace CognitiveSearch.Models
{
    /// <summary> A single bucket of a facet query result. Reports the number of documents with a field value falling within a particular range or having a particular value or interval. </summary>
    public partial class FacetResult : IReadOnlyDictionary<string, object>
    {
        /// <summary> Initializes a new instance of FacetResult. </summary>
        internal FacetResult()
        {
            AdditionalProperties = new ChangeTrackingDictionary<string, object>();
        }

        /// <summary> Initializes a new instance of FacetResult. </summary>
        /// <param name="count"> The approximate count of documents falling within the bucket described by this facet. </param>
        /// <param name="additionalProperties"> . </param>
        internal FacetResult(long? count, IReadOnlyDictionary<string, object> additionalProperties)
        {
            Count = count;
            AdditionalProperties = additionalProperties;
        }

        /// <summary> The approximate count of documents falling within the bucket described by this facet. </summary>
        public long? Count { get; }
        internal IReadOnlyDictionary<string, object> AdditionalProperties { get; }
        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => AdditionalProperties.GetEnumerator();
        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => AdditionalProperties.GetEnumerator();
        /// <inheritdoc />
        public bool TryGetValue(string key, out object value) => AdditionalProperties.TryGetValue(key, out value);
        /// <inheritdoc />
        public bool ContainsKey(string key) => AdditionalProperties.ContainsKey(key);
        /// <inheritdoc />
        public IEnumerable<string> Keys => AdditionalProperties.Keys;
        /// <inheritdoc />
        public IEnumerable<object> Values => AdditionalProperties.Values;
        /// <inheritdoc />
        int IReadOnlyCollection<KeyValuePair<string, object>>.Count => AdditionalProperties.Count;
        /// <inheritdoc />
        public object this[string key]
        {
            get => AdditionalProperties[key];
        }
    }
}
