// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobItem : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "Blob");
            writer.WriteStartElement("Name");
            writer.WriteValue(Name);
            writer.WriteEndElement();
            writer.WriteStartElement("Deleted");
            writer.WriteValue(Deleted);
            writer.WriteEndElement();
            if (Snapshot != null)
            {
                writer.WriteStartElement("Snapshot");
                writer.WriteValue(Snapshot);
                writer.WriteEndElement();
            }
            writer.WriteObjectValue(Properties, "Properties");
            if (Metadata != null)
            {
                writer.WriteObjectValue(Metadata, "Metadata");
            }
            writer.WriteEndElement();
        }
        internal static BlobItem DeserializeBlobItem(XElement element)
        {
            BlobItem result = default;
            result = new BlobItem(); string value = default;
            var name = element.Element("Name");
            if (name != null)
            {
                value = (string)name;
            }
            result.Name = value;
            bool value0 = default;
            var deleted = element.Element("Deleted");
            if (deleted != null)
            {
                value0 = (bool)deleted;
            }
            result.Deleted = value0;
            string? value1 = default;
            var snapshot = element.Element("Snapshot");
            if (snapshot != null)
            {
                value1 = (string?)snapshot;
            }
            result.Snapshot = value1;
            BlobItemProperties value2 = default;
            var properties = element.Element("Properties");
            if (properties != null)
            {
                value2 = BlobItemProperties.DeserializeBlobItemProperties(properties);
            }
            result.Properties = value2;
            BlobMetadata? value3 = default;
            var metadata = element.Element("Metadata");
            if (metadata != null)
            {
                value3 = BlobMetadata.DeserializeBlobMetadata(metadata);
            }
            result.Metadata = value3;
            return result;
        }
    }
}
