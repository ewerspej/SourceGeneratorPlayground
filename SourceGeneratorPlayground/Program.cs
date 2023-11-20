namespace SourceGeneratorPlayground;

static partial class Program
{
    static void Main(string[] args)
    {
        Hello("Generator");
        Hello(BedTypes.PlumBed);

        foreach (var bedType in BedTypes.AllBedTypes)
        {
            Hello(bedType);
        }
    }

    static partial void Hello(string message);
}