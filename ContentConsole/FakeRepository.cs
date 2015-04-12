using Content.Bll;
using System;
using System.Collections.Generic;

namespace ContentConsole
{
	public class FakeRepository : IContentRepository
	{
		public IEnumerable<string> GetBannedWords()
		{
			return new List<string> { "swine", "bad", "nasty", "horrible" };
		}

		public string GetContent()
		{
			return "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";
		}
	}
}
