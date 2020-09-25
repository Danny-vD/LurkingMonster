using VDFramework.EventSystem;

namespace Events
{
	public class CrackEvent : VDEvent
	{
		public readonly int RepairCost;
		
		public CrackEvent(int repairCost)
		{
			RepairCost = repairCost;
		}
	}
}