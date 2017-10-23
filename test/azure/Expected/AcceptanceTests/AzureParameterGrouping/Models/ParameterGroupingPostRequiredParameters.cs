// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Fixtures.Azure.AcceptanceTestsAzureParameterGrouping.Models
{
    using Microsoft.Rest;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Additional parameters for PostRequired operation.
    /// </summary>
    public partial class ParameterGroupingPostRequiredParameters
    {
        /// <summary>
        /// Initializes a new instance of the
        /// ParameterGroupingPostRequiredParameters class.
        /// </summary>
        public ParameterGroupingPostRequiredParameters()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// ParameterGroupingPostRequiredParameters class.
        /// </summary>
        /// <param name="path">Path parameter</param>
        public ParameterGroupingPostRequiredParameters(int body, string path)
        {
            Body = body;
            Path = path;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "")]
        public int Body { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "")]
        public string CustomHeader { get; set; }

        /// <summary>
        /// Gets or sets query parameter with default
        /// </summary>
        [JsonProperty(PropertyName = "")]
        public int? Query { get; set; }

        /// <summary>
        /// Gets or sets path parameter
        /// </summary>
        [JsonProperty(PropertyName = "")]
        public string Path { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate()
        {
            if (Path == null)
            {
                throw new ValidationException(ValidationRules.CannotBeNull, "Path");
            }
        }
    }
}
