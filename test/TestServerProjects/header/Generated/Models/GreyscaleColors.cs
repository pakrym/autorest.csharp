namespace header.Models.V100
{
    public readonly partial struct GreyscaleColors
    {
        private readonly string _value;

        public GreyscaleColors(string value)
        {
            value = value ?? throw new System.ArgumentNullException(nameof(value));
        }

        private const string WhiteValue;
        private const string BlackValue;
        private const string GREYValue;

        private GreyscaleColors WhiteValue { get; } = new GreyscaleColors(null);
        private GreyscaleColors BlackValue { get; } = new GreyscaleColors(null);
        private GreyscaleColors GREYValue { get; } = new GreyscaleColors(null);
    }
}