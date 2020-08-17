using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace server.Util
{
	internal static class StringHelper
	{
		private static readonly Random Random = new Random();

		public static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[Random.Next(s.Length)]).ToArray());
		}

		public static string ReplaceX(this string text, string regex, string replacement)
		{
			return Regex.Replace(text, regex, replacement);
		}
	}
}
