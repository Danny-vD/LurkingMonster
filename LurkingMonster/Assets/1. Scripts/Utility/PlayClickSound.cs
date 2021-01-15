using Audio;
using UnityEngine;
using VDFramework;
using EventType = Enums.Audio.EventType;

namespace Utility
{
	public class PlayClickSound : BetterMonoBehaviour
	{
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				AudioPlayer.PlayOneShot2D(EventType.SFX_CLICK);
			}
		}
	}
}