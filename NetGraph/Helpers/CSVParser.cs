using System;
using System.Collections.Generic;

namespace CyConex.Helpers
{
    public static class CSVParser
	{
		public static List<string> ParseCSV(string line, char delimiter, bool trimStartAndEndQuotes)
		{
			char doubleQuotesReplacement = '\uFFFF';
			List<string> retval = new List<string>();
			if (String.IsNullOrEmpty(line))
			{
				//We got empty string it may be value in one column CSV
				retval.Add(String.Empty);
				return retval;
			}
			//Split by delimiters. Special care about double quotes inside
			string[] data = line.Replace("\"\"", doubleQuotesReplacement).Split(delimiter);
			if (data.Length == 1)
			{
				//If no separators in  line
				line = line.Replace(doubleQuotesReplacement, "\"\"");
				if (line != "\"\"")
				{
					line = line.Replace("\"\"", "\"");
				}
				if (trimStartAndEndQuotes)
				{
					line = line.TrimOnce('\"');
				}
				retval.Add(line);
				return retval;
			}
			else
			{
				//Add first value
				string dataValue = data[0].Trim();
				if (dataValue.Trim().StartsWith("\""))
				{
					retval.Add(dataValue.Trim());
				}
				else
				{
					retval.Add(data[0]);
				}
				for (int i = 1; i < data.Length; i++)
				{
					dataValue = data[i].Trim();
					if (retval[retval.Count - 1].StartsWith("\"") && !retval[retval.Count - 1].EndsWith("\""))
					{
						//if previous field start with quote but not end with qoute, add next value to this field
						retval[retval.Count - 1] += delimiter + data[i];
					}
					else
					{
						//If previous field start with quote and end with quote, add new field
						if (dataValue.Trim().StartsWith("\""))
						{
							retval.Add(dataValue.Trim());
						}
						else
						{
							retval.Add(data[i]);
						}
					}
				}
			}
			//Post processing
			for (int i = 0; i < retval.Count; i++)
			{
				//Replace double quotes back but with single quote.
				retval[i] = retval[i].Replace(doubleQuotesReplacement, "\"\"");
				//Trim quotes if need
				if (retval[i].StartsWith("\"") && retval[i].EndsWith("\"") && trimStartAndEndQuotes)
				{
					retval[i] = retval[i].TrimOnce('"');
				}
				if (retval[i] != "\"\"")
				{
					retval[i] = retval[i].Replace("\"\"", "\"");
				}
			}
			return retval;
		}
	}
}
