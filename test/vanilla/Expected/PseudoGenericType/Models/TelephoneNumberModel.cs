// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Zapappi.Client.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    public partial class TelephoneNumberModel
    {
        /// <summary>
        /// Initializes a new instance of the TelephoneNumberModel class.
        /// </summary>
        public TelephoneNumberModel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TelephoneNumberModel class.
        /// </summary>
        public TelephoneNumberModel(string number = default(string), System.DateTime? created = default(System.DateTime?), System.DateTime? nextBillingDate = default(System.DateTime?), string description = default(string), string countryCode = default(string), string sMSEndpoint = default(string), string sMSEndpointType = default(string), string mappingId = default(string), string type = default(string), string networkType = default(string), string countryName = default(string))
        {
            Number = number;
            Created = created;
            NextBillingDate = nextBillingDate;
            Description = description;
            CountryCode = countryCode;
            SMSEndpoint = sMSEndpoint;
            SMSEndpointType = sMSEndpointType;
            MappingId = mappingId;
            Type = type;
            NetworkType = networkType;
            CountryName = countryName;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Number")]
        public string Number { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Created")]
        public System.DateTime? Created { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "NextBillingDate")]
        public System.DateTime? NextBillingDate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CountryCode")]
        public string CountryCode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "SMSEndpoint")]
        public string SMSEndpoint { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "SMSEndpointType")]
        public string SMSEndpointType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "MappingId")]
        public string MappingId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Type")]
        public string Type { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "NetworkType")]
        public string NetworkType { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CountryName")]
        public string CountryName { get; private set; }

    }
}