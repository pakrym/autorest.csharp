// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobBlock : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "Block");
            writer.WriteStartElement("Name");
            writer.WriteValue(Name);
            writer.WriteEndElement();
            writer.WriteStartElement("Size");
            writer.WriteValue(Size);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        internal static BlobBlock DeserializeBlobBlock(XElement element)
        {
            BlobBlock result = default;
            result = new BlobBlock(); string value = default;
            var name = element.Element("Name");
            if (name != null)
            {
                value = (string)name;
            }
            result.Name = value;
            int value0 = default;
            var size = element.Element("Size");
            if (size != null)
            {
                value0 = (int)size;
            }
            result.Size = value0;
            return result;
        }
    }
}
