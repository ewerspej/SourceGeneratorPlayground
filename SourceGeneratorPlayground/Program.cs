namespace SourceGeneratorPlayground;

static partial class Program
{
    static void Main(string[] args)
    {
        Hello("Generator");
        Hello(BedTypes.SharkBed);
    }

    static partial void Hello(string message);
}