using Enums;
using VDFramework.EventSystem;

namespace Events
{
	public class PowerUpIncreaseEvent : VDEvent
	{
		public readonly PowerUpType Type;

		public PowerUpIncreaseEvent(PowerUpType type)
		{
			this.Type = type;
		}
	}
}