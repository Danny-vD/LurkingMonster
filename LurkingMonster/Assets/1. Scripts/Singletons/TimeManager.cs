using UnityEngine;
using VDFramework.Singleton;

namespace Singletons
{
	public class TimeManager : Singleton<TimeManager>
	{
		private bool isPaused;
		
		public void Pause()
		{
			Time.timeScale = 0;
			isPaused       = true;
		}
		
		public void UnPause()
		{
			Time.timeScale = 1;
			isPaused       = false;
		}

		public bool IsPaused()
		{
			return isPaused;
		}

		protected override void OnDestroy()
		{
			UnPause();
			base.OnDestroy();
		}
	}
}