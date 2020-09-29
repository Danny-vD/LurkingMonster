using Events;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Buttons
{
	public class ButtonCollectRent : BetterMonoBehaviour
	{
		public int Rent { get; set; }

		public void CollectRent()
		{
			EventManager.Instance.RaiseEvent(new CollectRentEvent(Rent));
		}
	}
}