using UnityEngine;
using VDFramework;

namespace Utility
{
	public class URLOpener : BetterMonoBehaviour
	{
		public string ProcessPath = "https://www.kcaf.nl/";
		
		public static void OpenURL(string path)
		{
			Application.OpenURL(path);
		}

		public void OpenURL()
		{
			OpenURL(ProcessPath);
		}
	}
}