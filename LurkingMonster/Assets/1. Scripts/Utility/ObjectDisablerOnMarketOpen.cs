using Events;
using VDFramework;
using VDFramework.EventSystem;

namespace Utility
{
	public class ObjectDisablerOnMarketOpen : BetterMonoBehaviour
	{
		private void Start()
		{
			EventManager.Instance.AddListener<OpenMarketEvent>(Disable);
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			EventManager.Instance.RemoveListener<OpenMarketEvent>(Disable);
		}

		private void Disable()
		{
			CachedGameObject.SetActive(false);
		}
	}
}