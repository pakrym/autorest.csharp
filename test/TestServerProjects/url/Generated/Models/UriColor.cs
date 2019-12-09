namespace url.Models.V100
{
    public readonly partial struct UriColor : System.IEquatable<UriColor>
    {
        private readonly string _value;

        public UriColor(string value)
        {
            _value = value ?? throw new System.ArgumentNullException(nameof(value));
        }

        private const string RedColorValue = "red color";
        private const string GreenColorValue = "green color";
        private const string BlueColorValue = "blue color";

        public static UriColor RedColor { get; } = new UriColor(RedColorValue);
        public static UriColor GreenColor { get; } = new UriColor(GreenColorValue);
        public static UriColor BlueColor { get; } = new UriColor(BlueColorValue);

        public override bool Equals(object obj)
        {
            return obj is UriColor other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _value?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}