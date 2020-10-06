using Enums;
using IO;

namespace Utility
{
	public static class LanguageUtil
	{
		public static string GetJsonString(string variableName)
		{
			return JsonVariables.Instance.GetVariable(variableName, LanguageSettings.Language.ToString());
		}
	}
}