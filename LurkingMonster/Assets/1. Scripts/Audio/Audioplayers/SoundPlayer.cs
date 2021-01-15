using UnityEngine;
using VDFramework;
using EventType = Enums.Audio.EventType;

namespace Audio.Audioplayers
{
	public class SoundPlayer : BetterMonoBehaviour
	{
		[SerializeField]
		private EventType eventType;

		public void SetEventType(EventType @event)
		{
			eventType = @event;
		}
		
		public void Play()
		{
			AudioPlayer.PlayOneShot2D(eventType);
		}
	}
}