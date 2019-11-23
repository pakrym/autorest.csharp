// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace url.Models.V100
{
    public readonly partial struct UriColor
    {
        private const string RedColorValue = "red color";
        private const string GreenColorValue = "green color";
        private const string BlueColorValue = "blue color";

        public static UriColor RedColor { get; } = new UriColor(RedColorValue);
        public static UriColor GreenColor { get; } = new UriColor(GreenColorValue);
        public static UriColor BlueColor { get; } = new UriColor(BlueColorValue);
    }
}
