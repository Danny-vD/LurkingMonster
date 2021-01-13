using VDFramework.EventSystem;

namespace Events.SoilSamplesManagement
{
	public class IncreaseSoilSamplesEvent : VDEvent
	{
		public readonly int Amount;

		public IncreaseSoilSamplesEvent(int amount)
		{
			Amount = amount;
		}
	}
}