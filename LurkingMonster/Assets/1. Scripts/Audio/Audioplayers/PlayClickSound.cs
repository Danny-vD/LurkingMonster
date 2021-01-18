using FMOD.Studio;
using UnityEngine;
using VDFramework;
using EventType = Enums.Audio.EventType;

namespace Audio.Audioplayers
{
	public class PlayClickSound : BetterMonoBehaviour
	{
		private EventInstance click;
		
		private void Start()
		{
			click = AudioPlayer.GetEventInstance(EventType.SFX_Click);
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				click.start();
			}
		}

		private void OnDestroy()
		{
			click.release();
		}
	}
}