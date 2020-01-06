// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class StorageError : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "StorageError");
            if (Message != null)
            {
                writer.WriteStartElement("Message");
                writer.WriteValue(Message);
                writer.WriteEndElement();
            }
            if (Code != null)
            {
                writer.WriteStartElement("Code");
                writer.WriteValue(Code);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static StorageError DeserializeStorageError(XElement element)
        {
            StorageError result = default;
            result = new StorageError(); string? value = default;
            var message = element.Element("Message");
            if (message != null)
            {
                value = (string?)message;
            }
            result.Message = value;
            string? value0 = default;
            var code = element.Element("Code");
            if (code != null)
            {
                value0 = (string?)code;
            }
            result.Code = value0;
            return result;
        }
    }
}
