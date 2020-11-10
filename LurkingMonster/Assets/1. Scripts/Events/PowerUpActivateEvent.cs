using Enums;
using VDFramework.EventSystem;

namespace Events
{
	public class PowerUpActivateEvent : VDEvent
	{
		public readonly PowerUpType Type;

		public PowerUpActivateEvent(PowerUpType type)
		{
			this.Type = type;
		}
	}
}