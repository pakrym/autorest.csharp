// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobServiceProperties : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "StorageServiceProperties");
            if (Logging != null)
            {
                writer.WriteObjectValue(Logging, "Logging");
            }
            if (HourMetrics != null)
            {
                writer.WriteObjectValue(HourMetrics, "Metrics");
            }
            if (MinuteMetrics != null)
            {
                writer.WriteObjectValue(MinuteMetrics, "Metrics");
            }
            if (DefaultServiceVersion != null)
            {
                writer.WriteStartElement("DefaultServiceVersion");
                writer.WriteValue(DefaultServiceVersion);
                writer.WriteEndElement();
            }
            if (DeleteRetentionPolicy != null)
            {
                writer.WriteObjectValue(DeleteRetentionPolicy, "RetentionPolicy");
            }
            if (StaticWebsite != null)
            {
                writer.WriteObjectValue(StaticWebsite, "StaticWebsite");
            }
            if (Cors != null)
            {
                writer.WriteStartElement("Cors");
                foreach (var item in Cors)
                {
                    writer.WriteObjectValue(item, "CorsRule");
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static BlobServiceProperties DeserializeBlobServiceProperties(XElement element)
        {
            BlobServiceProperties result = default;
            result = new BlobServiceProperties(); BlobAnalyticsLogging? value = default;
            var logging = element.Element("Logging");
            if (logging != null)
            {
                value = BlobAnalyticsLogging.DeserializeBlobAnalyticsLogging(logging);
            }
            result.Logging = value;
            BlobMetrics? value0 = default;
            var metrics = element.Element("Metrics");
            if (metrics != null)
            {
                value0 = BlobMetrics.DeserializeBlobMetrics(metrics);
            }
            result.HourMetrics = value0;
            BlobMetrics? value1 = default;
            var metrics0 = element.Element("Metrics");
            if (metrics0 != null)
            {
                value1 = BlobMetrics.DeserializeBlobMetrics(metrics0);
            }
            result.MinuteMetrics = value1;
            string? value2 = default;
            var defaultServiceVersion = element.Element("DefaultServiceVersion");
            if (defaultServiceVersion != null)
            {
                value2 = (string?)defaultServiceVersion;
            }
            result.DefaultServiceVersion = value2;
            BlobRetentionPolicy? value3 = default;
            var retentionPolicy = element.Element("RetentionPolicy");
            if (retentionPolicy != null)
            {
                value3 = BlobRetentionPolicy.DeserializeBlobRetentionPolicy(retentionPolicy);
            }
            result.DeleteRetentionPolicy = value3;
            BlobStaticWebsite? value4 = default;
            var staticWebsite = element.Element("StaticWebsite");
            if (staticWebsite != null)
            {
                value4 = BlobStaticWebsite.DeserializeBlobStaticWebsite(staticWebsite);
            }
            result.StaticWebsite = value4;
            var cors = element.Element("Cors");
            if (cors != null)
            {
                result.Cors = new List<BlobCorsRule>();
                foreach (var e in cors.Elements("CorsRule"))
                {
                    BlobCorsRule value5 = default;
                    value5 = BlobCorsRule.DeserializeBlobCorsRule(e);
                    result.Cors.Add(value5);
                }
            }
            return result;
        }
    }
}
