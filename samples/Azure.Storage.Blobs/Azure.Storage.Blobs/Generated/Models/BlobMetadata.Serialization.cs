// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobMetadata : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "Metadata");
            if (Encrypted != null)
            {
                writer.WriteStartAttribute("Encrypted");
                writer.WriteValue(Encrypted);
                writer.WriteEndAttribute();
            }
            writer.WriteEndElement();
        }
        internal static BlobMetadata DeserializeBlobMetadata(XElement element)
        {
            BlobMetadata result = default;
            result = new BlobMetadata(); var encrypted = element.Attribute("Encrypted");
            if (encrypted != null)
            {
                result.Encrypted = (string?)encrypted;
            }
            return result;
        }
    }
}
