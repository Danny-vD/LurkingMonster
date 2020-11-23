using Enums;
using VDFramework.EventSystem;

namespace Events
{
	// TODO: Remove?
	public class PowerUpDisableEvent : VDEvent<PowerUpDisableEvent>
	{
		public readonly PowerUpType Type;

		public PowerUpDisableEvent(PowerUpType type)
		{
			this.Type = type;
		}
	}
}