// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobSignedIdentifier : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "SignedIdentifier");
            writer.WriteStartElement("Id");
            writer.WriteValue(Id);
            writer.WriteEndElement();
            writer.WriteObjectValue(AccessPolicy, "AccessPolicy");
            writer.WriteEndElement();
        }
        internal static BlobSignedIdentifier DeserializeBlobSignedIdentifier(XElement element)
        {
            BlobSignedIdentifier result = default;
            result = new BlobSignedIdentifier(); string value = default;
            var id = element.Element("Id");
            if (id != null)
            {
                value = (string)id;
            }
            result.Id = value;
            BlobAccessPolicy value0 = default;
            var accessPolicy = element.Element("AccessPolicy");
            if (accessPolicy != null)
            {
                value0 = BlobAccessPolicy.DeserializeBlobAccessPolicy(accessPolicy);
            }
            result.AccessPolicy = value0;
            return result;
        }
    }
}
