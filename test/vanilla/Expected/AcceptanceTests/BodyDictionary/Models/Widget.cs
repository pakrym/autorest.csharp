// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Fixtures.AcceptanceTestsBodyDictionary.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class Widget
    {
        /// <summary>
        /// Initializes a new instance of the Widget class.
        /// </summary>
        public Widget()
        {
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "integer")]
        public int? Integer { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "string")]
        public string StringProperty { get; set; }

    }
}
