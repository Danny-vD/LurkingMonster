using Events;
using VDFramework;
using VDFramework.EventSystem;

namespace UI
{
	public class ButtonCrackHouse : BetterMonoBehaviour
	{
		public int RepairCost { get; set; }
		
		public void RepairCostHouse()
		{
			EventManager.Instance.RaiseEvent(new CrackEvent(RepairCost));
		}
	}
}