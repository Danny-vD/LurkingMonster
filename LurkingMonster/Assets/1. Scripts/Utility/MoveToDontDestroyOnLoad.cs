using VDFramework;

namespace Utility
{
	public class MoveToDontDestroyOnLoad : BetterMonoBehaviour
	{
		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		}
	}
}