using Singletons;
using UnityEngine;

namespace Utility
{
	public static class VibrationUtil
	{
		public static void Vibrate()
		{
			if (UserSettings.GameData.Vibrate)
			{
				Handheld.Vibrate();
			}
		}
	}
}