using Events;
using Events.MoneyManagement;
using Gameplay.Buildings;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Buttons.Gameplay
{
	public class ButtonCollectRent : BetterMonoBehaviour
	{
		private Building building;

		private void Awake()
		{
			building = GetComponentInParent<Building>();
		}

		public void CollectRent()
		{
			EventManager.Instance.RaiseEvent(new CollectRentEvent(building.Data.Rent));
		}
	}
}