// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobGeoReplication : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "GeoReplication");
            writer.WriteStartElement("Status");
            writer.WriteValue(Status.ToSerialString());
            writer.WriteEndElement();
            if (LastSyncedOn != null)
            {
                writer.WriteStartElement("LastSyncTime");
                writer.WriteValue(LastSyncedOn.Value, "R");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static BlobGeoReplication DeserializeBlobGeoReplication(XElement element)
        {
            BlobGeoReplication result = default;
            result = new BlobGeoReplication(); BlobGeoReplicationStatus value = default;
            var status = element.Element("Status");
            if (status != null)
            {
                value = status.Value.ToBlobGeoReplicationStatus();
            }
            result.Status = value;
            DateTimeOffset? value0 = default;
            var lastSyncTime = element.Element("LastSyncTime");
            if (lastSyncTime != null)
            {
                value0 = lastSyncTime.GetDateTimeOffsetValue("R");
            }
            result.LastSyncedOn = value0;
            return result;
        }
    }
}
