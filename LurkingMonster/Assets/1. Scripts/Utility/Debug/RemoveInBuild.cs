using UnityEngine;
using VDFramework;

namespace Utility.Debug
{
	public class RemoveInBuild : BetterMonoBehaviour
	{
		[SerializeField]
		private bool Remove = false;

		private void Awake()
		{
			if (Remove)
			{
				Destroy(CachedGameObject);
			}
		}
	}
}