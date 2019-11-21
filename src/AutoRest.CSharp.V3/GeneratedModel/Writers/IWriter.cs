namespace AutoRest.CSharp.V3
{
    public interface IWriter<in T>
    {
        void Write(T model, WriterContext context);
    }
}