using System;
using System.Collections.Generic;
using Content.Bll;
using NUnit.Framework;
using Moq;

namespace ContentConsole.Test.Unit
{
	[TestFixture]
	public class ContentAnalyzerTest
	{
		[TestCase("weather is bad in manchester", 1, Description = "Banned word in middle")]
		[TestCase("nasty weather in manchester", 1, Description = "Banned word in the beginning")]
		[TestCase("manchester weather is horrible", 1, Description = "Banned word at the end")]
		[TestCase("weather is BAD in manchester", 1, Description = "Case insensitive search")]
		[TestCase("weather is good in manchester", 0, Description = "No banned word")]
		public void BannedWordCountTest(string content, int expectedCount)
		{
			var mockRepository = new Mock<IContentRepository>();
			mockRepository.Setup(x => x.GetBannedWords()).Returns(new List<string> { "swine", "nasty", "bad", "horrible" });
			mockRepository.Setup(x => x.GetContent()).Returns(content);
			var contentAnalyser = new ContentAnalyzer(mockRepository.Object);
			int actualCount = contentAnalyser.GetBannedWordCount();
			Assert.AreEqual(expectedCount, actualCount);
		}

		[Test]
		public void BannedWordListNotHardCodedTest()
		{
			const string content = "weather in manchester is bad and horrible.";

			// The same content returns 2 banned words in the first case and 
			// 1 in the second case when we change the master list of banned words.
			var mockRepository1 = new Mock<IContentRepository>();
			mockRepository1.Setup(x => x.GetBannedWords()).Returns(new List<string> { "bad", "horrible" });
			mockRepository1.Setup(x => x.GetContent()).Returns(content);
			var contentAnalyser1 = new ContentAnalyzer(mockRepository1.Object);
			int actualCount1 = contentAnalyser1.GetBannedWordCount();
			Assert.AreEqual(2, actualCount1);

			var mockRepository2 = new Mock<IContentRepository>();
			mockRepository2.Setup(x => x.GetBannedWords()).Returns(new List<string> { "horrible" });
			mockRepository2.Setup(x => x.GetContent()).Returns(content);
			var contentAnalyser2 = new ContentAnalyzer(mockRepository2.Object);
			int actualCount2 = contentAnalyser2.GetBannedWordCount();
			Assert.AreEqual(1, actualCount2);
		}

		[Test]
		public void DisableFilteringTest()
		{
			string content = "bad weather";
			var mockRepository = new Mock<IContentRepository>();
			mockRepository.Setup(x => x.GetBannedWords()).Returns(new List<string> { "bad", "horrible" });
			mockRepository.Setup(x => x.GetContent()).Returns(content);
			var contentAnalyser = new ContentAnalyzer(mockRepository.Object);
			Assert.AreEqual("b#d weather", contentAnalyser.GetContent());

			// After filtering is disabled.
			contentAnalyser.SetContentFilteringStatus(false);
			Assert.AreEqual(content, contentAnalyser.GetContent());
		}
	}
}
