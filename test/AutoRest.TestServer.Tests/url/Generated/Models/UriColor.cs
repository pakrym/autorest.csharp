// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.ComponentModel;

namespace url.Models.V100
{
    public readonly partial struct UriColor : IEquatable<UriColor>
    {
        private readonly string? _value;

        public UriColor(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public static bool operator ==(UriColor left, UriColor right) => left.Equals(right);
        public static bool operator !=(UriColor left, UriColor right) => !left.Equals(right);
        public static implicit operator UriColor(string value) => new UriColor(value);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object? obj) => obj is UriColor other && Equals(other);
        public bool Equals(UriColor other) => string.Equals(_value, other._value, StringComparison.Ordinal);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        public override string? ToString() => _value;
    }
}
