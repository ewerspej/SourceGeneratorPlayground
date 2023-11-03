namespace SourceGeneratorPlayground;

static partial class Program
{
    static void Main(string[] args)
    {
        Hello("Generator");
    }

    static partial void Hello(string message);
}