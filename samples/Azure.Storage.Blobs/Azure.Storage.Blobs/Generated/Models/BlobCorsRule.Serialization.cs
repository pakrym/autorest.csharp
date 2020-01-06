// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobCorsRule : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "CorsRule");
            writer.WriteStartElement("AllowedOrigins");
            writer.WriteValue(AllowedOrigins);
            writer.WriteEndElement();
            writer.WriteStartElement("AllowedMethods");
            writer.WriteValue(AllowedMethods);
            writer.WriteEndElement();
            writer.WriteStartElement("AllowedHeaders");
            writer.WriteValue(AllowedHeaders);
            writer.WriteEndElement();
            writer.WriteStartElement("ExposedHeaders");
            writer.WriteValue(ExposedHeaders);
            writer.WriteEndElement();
            writer.WriteStartElement("MaxAgeInSeconds");
            writer.WriteValue(MaxAgeInSeconds);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        internal static BlobCorsRule DeserializeBlobCorsRule(XElement element)
        {
            BlobCorsRule result = default;
            result = new BlobCorsRule(); string value = default;
            var allowedOrigins = element.Element("AllowedOrigins");
            if (allowedOrigins != null)
            {
                value = (string)allowedOrigins;
            }
            result.AllowedOrigins = value;
            string value0 = default;
            var allowedMethods = element.Element("AllowedMethods");
            if (allowedMethods != null)
            {
                value0 = (string)allowedMethods;
            }
            result.AllowedMethods = value0;
            string value1 = default;
            var allowedHeaders = element.Element("AllowedHeaders");
            if (allowedHeaders != null)
            {
                value1 = (string)allowedHeaders;
            }
            result.AllowedHeaders = value1;
            string value2 = default;
            var exposedHeaders = element.Element("ExposedHeaders");
            if (exposedHeaders != null)
            {
                value2 = (string)exposedHeaders;
            }
            result.ExposedHeaders = value2;
            int value3 = default;
            var maxAgeInSeconds = element.Element("MaxAgeInSeconds");
            if (maxAgeInSeconds != null)
            {
                value3 = (int)maxAgeInSeconds;
            }
            result.MaxAgeInSeconds = value3;
            return result;
        }
    }
}
