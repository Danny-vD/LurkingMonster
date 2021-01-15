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

		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(PlaySound);
		}

		private void PlaySound()
		{
			AudioPlayer.PlayOneShot2D(eventType);
		}
	}
}