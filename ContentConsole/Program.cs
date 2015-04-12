using System;
using Content.Bll;

namespace ContentConsole
{
    public static class Program
    {
        public static void Main(string[] args)
        {
			var contentAnalyzer = new ContentAnalyzer(new FakeRepository()); // Could also have used Moq.
			
			Console.WriteLine("Story 1");
            Console.WriteLine("Scanned the text:");
			contentAnalyzer.SetContentFilteringStatus(false);
            Console.WriteLine(contentAnalyzer.GetContent());
            Console.WriteLine("Total Number of negative words: " + contentAnalyzer.GetBannedWordCount());
			Console.WriteLine();

			Console.WriteLine("Story 2");
			Console.WriteLine("I haven't implemented an UpdateBannedWords method but the unit tests prove that the list of banned words is not hard coded.");
			Console.WriteLine();
			
			Console.WriteLine("Story 3");
			contentAnalyzer.SetContentFilteringStatus(true);
			Console.WriteLine(contentAnalyzer.GetContent());
			Console.WriteLine();

			Console.WriteLine("Story 4");
			Console.WriteLine("Scanned the text:");
			contentAnalyzer.SetContentFilteringStatus(false);
			Console.WriteLine(contentAnalyzer.GetContent());
			Console.WriteLine("Total Number of negative words: " + contentAnalyzer.GetBannedWordCount());
			Console.WriteLine();

            Console.WriteLine("Press ANY key to exit.");
            Console.ReadKey();
        }
    }

}
