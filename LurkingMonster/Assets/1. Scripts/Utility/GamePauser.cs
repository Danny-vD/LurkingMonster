using Singletons;
using UnityEngine;

namespace Utility
{
	public class GamePauser : MonoBehaviour
	{
		private void OnEnable()
		{
			TimeManager.Instance.Pause();
		}

		private void OnDisable()
		{
			TimeManager.Instance.UnPause();
		}
	}
}
