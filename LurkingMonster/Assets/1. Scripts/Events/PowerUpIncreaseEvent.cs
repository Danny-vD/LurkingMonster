using Enums;
using VDFramework.EventSystem;

namespace Events
{
	public class PowerUpIncreaseEvent : VDEvent<PowerUpIncreaseEvent>
	{
		public readonly PowerUpType Type;

		public PowerUpIncreaseEvent(PowerUpType type)
		{
			Type = type;
		}
	}
}