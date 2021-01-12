using VDFramework.EventSystem;

namespace Events.SoilSamplesManagement
{
	public class DecreaseSoilSamplesEvent : VDEvent
	{
		public readonly int Amount;

		public DecreaseSoilSamplesEvent(int amount)
		{
			Amount = amount;
		}
	}
}