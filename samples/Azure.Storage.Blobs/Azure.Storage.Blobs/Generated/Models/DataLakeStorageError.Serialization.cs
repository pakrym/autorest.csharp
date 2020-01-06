// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class DataLakeStorageError : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "DataLakeStorageError");
            if (Error != null)
            {
                writer.WriteObjectValue(Error, "error");
            }
            writer.WriteEndElement();
        }
        internal static DataLakeStorageError DeserializeDataLakeStorageError(XElement element)
        {
            DataLakeStorageError result = default;
            result = new DataLakeStorageError(); DataLakeStorageErrorError? value = default;
            var error = element.Element("error");
            if (error != null)
            {
                value = DataLakeStorageErrorError.DeserializeDataLakeStorageErrorError(error);
            }
            result.Error = value;
            return result;
        }
    }
}
