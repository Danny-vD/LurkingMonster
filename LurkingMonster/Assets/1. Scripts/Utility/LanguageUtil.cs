using Enums;
using IO;

namespace Utility
{
	public static class LanguageUtil
	{
		/// <summary>
		/// Returns the string for a specific key for the current language
		/// </summary>
		public static string GetJsonString(string variableName)
		{
			return JsonParser.Instance.GetVariable(variableName, LanguageSettings.Language.ToString());
		}

		/// <summary>
		/// Returns the formatted string for a specific key for the current language  
		/// </summary>
		public static string GetJsonString(string variableName, params object[] args)
		{
			return string.Format(GetJsonString(variableName), args);
		}
	}
}