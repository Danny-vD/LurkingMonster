using Enums;
using VDFramework.EventSystem;

namespace Events
{
	public class PowerUpDisableEvent : VDEvent
	{
		public readonly PowerUpType Type;

		public PowerUpDisableEvent(PowerUpType type)
		{
			this.Type = type;
		}
	}
}