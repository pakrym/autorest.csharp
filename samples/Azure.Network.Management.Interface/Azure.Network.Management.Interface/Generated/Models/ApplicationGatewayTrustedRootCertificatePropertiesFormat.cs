// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

namespace Azure.Network.Management.Interface.Models
{
    /// <summary> Trusted Root certificates properties of an application gateway. </summary>
    public partial class ApplicationGatewayTrustedRootCertificatePropertiesFormat
    {
        /// <summary> Certificate public data. </summary>
        public string Data { get; set; }
        /// <summary> Secret Id of (base-64 encoded unencrypted pfx) &apos;Secret&apos; or &apos;Certificate&apos; object stored in KeyVault. </summary>
        public string KeyVaultSecretId { get; set; }
        /// <summary> The provisioning state of the trusted root certificate resource. </summary>
        public ProvisioningState? ProvisioningState { get; internal set; }
    }
}