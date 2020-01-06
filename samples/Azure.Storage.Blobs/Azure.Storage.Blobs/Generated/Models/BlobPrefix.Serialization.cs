// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobPrefix : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "BlobPrefix");
            writer.WriteStartElement("Name");
            writer.WriteValue(Name);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        internal static BlobPrefix DeserializeBlobPrefix(XElement element)
        {
            BlobPrefix result = default;
            result = new BlobPrefix(); string value = default;
            var name = element.Element("Name");
            if (name != null)
            {
                value = (string)name;
            }
            result.Name = value;
            return result;
        }
    }
}
