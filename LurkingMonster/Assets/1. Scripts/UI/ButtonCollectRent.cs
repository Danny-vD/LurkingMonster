using Events;
using VDFramework;
using VDFramework.EventSystem;

namespace UI
{
	public class ButtonCollectRent : BetterMonoBehaviour
	{
		public float Rent { get; set; }

		public void CollectRent()
		{
			EventManager.Instance.RaiseEvent(new CollectRentEvent(15));
		}
	}
}