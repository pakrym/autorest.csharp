// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using Azure.Core;

namespace paging.Models
{
    /// <summary> The ProductResult. </summary>
    public partial class ProductResult
    {
        /// <summary> Initializes a new instance of ProductResult. </summary>
        internal ProductResult()
        {
            Values = new ChangeTrackingList<Product>();
        }

        /// <summary> Initializes a new instance of ProductResult. </summary>
        /// <param name="values"> . </param>
        /// <param name="nextLink"> . </param>
        internal ProductResult(IReadOnlyList<Product> values, string nextLink)
        {
            Values = values;
            NextLink = nextLink;
        }

        public IReadOnlyList<Product> Values { get; }
        public string NextLink { get; }
    }
}
