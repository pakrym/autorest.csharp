// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class DataLakeStorageErrorError : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "DataLakeStorageError-error");
            if (Code != null)
            {
                writer.WriteStartElement("Code");
                writer.WriteValue(Code);
                writer.WriteEndElement();
            }
            if (Message != null)
            {
                writer.WriteStartElement("Message");
                writer.WriteValue(Message);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static DataLakeStorageErrorError DeserializeDataLakeStorageErrorError(XElement element)
        {
            DataLakeStorageErrorError result = default;
            result = new DataLakeStorageErrorError(); string? value = default;
            var code = element.Element("Code");
            if (code != null)
            {
                value = (string?)code;
            }
            result.Code = value;
            string? value0 = default;
            var message = element.Element("Message");
            if (message != null)
            {
                value0 = (string?)message;
            }
            result.Message = value0;
            return result;
        }
    }
}
