// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobServiceStatistics : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "StorageServiceStats");
            if (GeoReplication != null)
            {
                writer.WriteObjectValue(GeoReplication, "GeoReplication");
            }
            writer.WriteEndElement();
        }
        internal static BlobServiceStatistics DeserializeBlobServiceStatistics(XElement element)
        {
            BlobServiceStatistics result = default;
            result = new BlobServiceStatistics(); BlobGeoReplication? value = default;
            var geoReplication = element.Element("GeoReplication");
            if (geoReplication != null)
            {
                value = BlobGeoReplication.DeserializeBlobGeoReplication(geoReplication);
            }
            result.GeoReplication = value;
            return result;
        }
    }
}
