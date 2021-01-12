using VDFramework;
using VDFramework.EventSystem;

namespace Utility.ListenerAdders
{
	public class AddAllListeners : BetterMonoBehaviour
	{
		private ListenerAdder[] listenerAdders;
		
		private void Start()
		{
			listenerAdders = FindObjectsOfType<ListenerAdder>(true);
			
			foreach (ListenerAdder listener in listenerAdders)
			{
				listener.AddListeners();	
			}
		}

		private void OnDestroy()
		{
			if (EventManager.IsInitialized)
			{
				foreach (ListenerAdder listener in listenerAdders)
				{
					listener.RemoveListeners();	
				}
			}
		}
	}
}