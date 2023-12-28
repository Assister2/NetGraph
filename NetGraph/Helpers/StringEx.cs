namespace CyConex.Helpers
{
    internal static class StringExt
	{
		internal static string Replace(this string value, string oldValue, char newValue)
		{
			return value.Replace(oldValue, newValue.ToString());
		}
		internal static string Replace(this string value, char oldValue, string newValue)
		{
			return value.Replace(oldValue.ToString(), newValue);
		}

		internal static string TrimOnce(this string value, char trimChar)
		{
			if (value.StartsWith(trimChar.ToString()))
			{
				value = value.Substring(1);
			}
			if (value.EndsWith(trimChar.ToString()))
			{
				value = value.Substring(0, value.Length - 1);
			}
			return value;
		}
	}
}
