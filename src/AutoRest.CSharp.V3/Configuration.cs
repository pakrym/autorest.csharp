using AutoRest.CSharp.V3.JsonRpc;

namespace AutoRest.CSharp.V3
{
    internal class Configuration
    {
        public string OutputPath { get; set; }
        public string Namespace { get; set; }
        public string Title { get; set; }

#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public Configuration() { }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        // Creates and populates a configuration
        public static Configuration Create(AutoRestInterface autoRest) =>
            new Configuration
            {
                OutputPath = autoRest.GetValue<string?>("output-folder").GetAwaiter().GetResult() ?? "Generated",
                Namespace = autoRest.GetValue<string?>("namespace").GetAwaiter().GetResult() ?? "Sample",
                Title = autoRest.GetValue<string?>("title").GetAwaiter().GetResult() ?? "Sample"
            };
    }
}
