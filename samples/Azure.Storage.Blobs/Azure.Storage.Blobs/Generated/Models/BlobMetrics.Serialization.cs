// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobMetrics : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "Metrics");
            if (Version != null)
            {
                writer.WriteStartElement("Version");
                writer.WriteValue(Version);
                writer.WriteEndElement();
            }
            writer.WriteStartElement("Enabled");
            writer.WriteValue(Enabled);
            writer.WriteEndElement();
            if (RetentionPolicy != null)
            {
                writer.WriteObjectValue(RetentionPolicy, "RetentionPolicy");
            }
            if (IncludeApis != null)
            {
                writer.WriteStartElement("IncludeAPIs");
                writer.WriteValue(IncludeApis.Value);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static BlobMetrics DeserializeBlobMetrics(XElement element)
        {
            BlobMetrics result = default;
            result = new BlobMetrics(); string? value = default;
            var version = element.Element("Version");
            if (version != null)
            {
                value = (string?)version;
            }
            result.Version = value;
            bool value0 = default;
            var enabled = element.Element("Enabled");
            if (enabled != null)
            {
                value0 = (bool)enabled;
            }
            result.Enabled = value0;
            BlobRetentionPolicy? value1 = default;
            var retentionPolicy = element.Element("RetentionPolicy");
            if (retentionPolicy != null)
            {
                value1 = BlobRetentionPolicy.DeserializeBlobRetentionPolicy(retentionPolicy);
            }
            result.RetentionPolicy = value1;
            bool? value2 = default;
            var includeAPIs = element.Element("IncludeAPIs");
            if (includeAPIs != null)
            {
                value2 = (bool?)includeAPIs;
            }
            result.IncludeApis = value2;
            return result;
        }
    }
}
