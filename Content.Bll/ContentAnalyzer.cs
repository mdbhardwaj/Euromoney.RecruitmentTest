using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content.Bll
{
	public class ContentAnalyzer
	{
		private IContentRepository _repository;
		private bool _isFilteringEnabled;
		private readonly string _content;

		public ContentAnalyzer(IContentRepository repository)
		{
			_repository = repository;
			_isFilteringEnabled = true;
			_content = _repository.GetContent();
		}

		public void SetContentFilteringStatus(bool isEnabled)
		{
			// TODO: Only certain roles should be allowed to set this flag.
			_isFilteringEnabled = isEnabled;
		}

		public int GetBannedWordCount()
		{
			int bannedWordCount = 0;
			foreach (var bannedWord in _repository.GetBannedWords())
			{
				// Assumption: Does not check for whole words. for e.g. Swineflu would be reported as containing 1 banned word.
				// ToLower may not be the most efficient way to compare. But since we have good test coverage, the implementation can be changed anytime.
				if (_content.ToLower().Contains(bannedWord)) 
				{
					bannedWordCount++;
				}
			}
			return bannedWordCount;
		}

		public string GetContent()
		{
			if (_isFilteringEnabled)
			{
				var filteredContent = new StringBuilder(_content);
				foreach (var bannedWord in _repository.GetBannedWords())
				{
					// TODO: Case sensitive search.
					filteredContent.Replace(bannedWord, Sanitize(bannedWord));
				}
				return filteredContent.ToString();
			}
			else
			{
				return _content;
			}
		}

		private static string Sanitize(string input)
		{
			// TODO: StringBuilder might be more efficient.
			// Assumption: There will be atleast 3 characters in a bad word.
			var result = string.Empty;

			for (int i = 0; i < input.Length; i++)
			{
				if (i == 0 || i == input.Length - 1)
				{
					result += input[i];
				}
				else
				{
					result += "#";
				}
			}

			return result;
		}
	}
}
