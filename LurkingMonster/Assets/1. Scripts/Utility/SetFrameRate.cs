using UnityEngine;
using VDFramework;

namespace Utility
{
	public class SetFrameRate : BetterMonoBehaviour
	{
		[SerializeField]
		private int targetFrameRate = 60;
		
		private void Awake()
		{
			Application.targetFrameRate = targetFrameRate;
		}
	}
}
