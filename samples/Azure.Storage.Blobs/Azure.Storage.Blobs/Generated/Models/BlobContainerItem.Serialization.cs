// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobContainerItem : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "Container");
            writer.WriteStartElement("Name");
            writer.WriteValue(Name);
            writer.WriteEndElement();
            writer.WriteObjectValue(Properties, "Properties");
            writer.WriteEndElement();
        }
        internal static BlobContainerItem DeserializeBlobContainerItem(XElement element)
        {
            BlobContainerItem result = default;
            result = new BlobContainerItem(); string value = default;
            var name = element.Element("Name");
            if (name != null)
            {
                value = (string)name;
            }
            result.Name = value;
            BlobContainerProperties value0 = default;
            var properties = element.Element("Properties");
            if (properties != null)
            {
                value0 = BlobContainerProperties.DeserializeBlobContainerProperties(properties);
            }
            result.Properties = value0;
            return result;
        }
    }
}
