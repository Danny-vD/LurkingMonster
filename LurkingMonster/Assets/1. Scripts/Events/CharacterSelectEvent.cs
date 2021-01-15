using UnityEngine.UI;
using VDFramework.EventSystem;

namespace Events
{
	public class CharacterSelectEvent : VDEvent
	{
		public Image character;

		public CharacterSelectEvent(Image character)
		{
			this.character = character;
		}
	}
}