using UnityEngine;
using UnityEngine.EventSystems;

namespace Utility
{
	/// <summary>
	/// A utility class made to get consistent behaviour on desktop and mobile
	/// </summary>
	public static class PointerUtil
	{
		public static bool IsPointerOverUIElement()
		{
			return EventSystem.current.IsPointerOverGameObject() || IsAnyFingerOverUIElement();
		}

		private static bool IsAnyFingerOverUIElement()
		{
			int length = Input.touchCount;

			for (int i = 0; i < length; i++)
			{
				if (EventSystem.current.IsPointerOverGameObject(i))
				{
					return true;
				}
			}

			return false;
		}
	}
}