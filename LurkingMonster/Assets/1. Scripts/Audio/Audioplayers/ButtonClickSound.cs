using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using EventType = Enums.Audio.EventType;

namespace Audio.Audioplayers
{
	[RequireComponent(typeof(Button))]
	public class ButtonClickSound : BetterMonoBehaviour
	{
		[SerializeField]
		private EventType eventType;

		private EventInstance eventInstance;
		
		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(PlaySound);
			eventInstance = AudioPlayer.GetEventInstance(eventType);
		}

		private void PlaySound()
		{
			eventInstance.start();
		}

		private void OnDestroy()
		{
			eventInstance.release();
		}
	}
}