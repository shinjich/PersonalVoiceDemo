public class Program
{
	public static void Main(string[] args)
	{
		Console.WriteLine("Create.");
		PersonalVoiceSample.CreateAsync().Wait();

		Console.WriteLine("Synthesis.");
		PersonalVoiceSample.SynthesisAsync().Wait();

		Console.WriteLine("Destroy.");
		PersonalVoiceSample.DestroyAsync().Wait();
	}
}
