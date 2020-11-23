using Enums;
using VDFramework.EventSystem;

namespace Events
{
	// TODO: Remove?
	public class PowerUpActivateEvent : VDEvent<PowerUpActivateEvent>
	{
		public readonly PowerUpType Type;

		public PowerUpActivateEvent(PowerUpType type)
		{
			this.Type = type;
		}
	}
}