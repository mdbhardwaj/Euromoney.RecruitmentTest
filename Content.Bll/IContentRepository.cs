using System;
using System.Collections.Generic;

namespace Content.Bll
{
	public interface IContentRepository
	{
		// Gets the list of banned words defined in the database.
		IEnumerable<string> GetBannedWords();

		// Gets the content from the database. This method was added after implementing story 4 - disable content filtering. Before this I was passing content as an 
		// input parameter to GetContent method. But that did not make sense once we added the concept of disabling filtering.
		string GetContent();

		// TODO: Implement a method to update the banned words in the database.
	}
}
