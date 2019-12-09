namespace body_string.Models.V100
{
    public readonly partial struct Colors
    {
        private readonly string _value;

        public Colors(string value)
        {
            value = value ?? throw new System.ArgumentNullException(nameof(value));
        }

        private const string RedColorValue;
        private const string GreenColorValue;
        private const string BlueColorValue;

        private Colors RedColorValue { get; } = new Colors(null);
        private Colors GreenColorValue { get; } = new Colors(null);
        private Colors BlueColorValue { get; } = new Colors(null);
    }
}