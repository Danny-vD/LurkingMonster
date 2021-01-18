using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using EventType = Enums.Audio.EventType;

namespace Audio.Audioplayers
{
	[RequireComponent(typeof(Button))]
	public class ClickSoundPlayer : BetterMonoBehaviour
	{
		private static EventInstance clickSound;
		private static bool initialized = false;

		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(ClickSound);

			if (!initialized)
			{
				Initialize();
			}
		}

		private static void Initialize()
		{
			initialized = true;
			clickSound  = AudioPlayer.GetEventInstance(EventType.SFX_Click);
		}

		private static void ClickSound()
		{
			clickSound.start();
		}
	}
}
