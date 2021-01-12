using Events.OpenMarketEvents;
using VDFramework;
using VDFramework.EventSystem;

namespace Utility
{
	public class ObjectDisablerOnMarketOpen : BetterMonoBehaviour
	{
		private void Start()
		{
			EventManager.Instance.AddListener<OpenMarketEvent>(Disable);
			EventManager.Instance.AddListener<OpenResearchFacilityEvent>(Disable);
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			EventManager.Instance.RemoveListener<OpenMarketEvent>(Disable);
			EventManager.Instance.RemoveListener<OpenResearchFacilityEvent>(Disable);
		}

		private void Disable()
		{
			CachedGameObject.SetActive(false);
		}
	}
}