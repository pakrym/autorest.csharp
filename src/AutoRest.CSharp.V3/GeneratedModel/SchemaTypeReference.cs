using AutoRest.CSharp.V3.Pipeline.Generated;

namespace AutoRest.CSharp.V3
{
    public class SchemaTypeReference : TypeReference
    {
        internal Schema Schema { get; }

        internal SchemaTypeReference(Schema schema)
        {
            Schema = schema;
        }
    }
}