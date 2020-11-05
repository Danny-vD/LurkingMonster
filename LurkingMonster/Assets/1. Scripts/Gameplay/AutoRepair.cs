using Singletons;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace Gameplay
{
	public class AutoRepair : BetterMonoBehaviour
	{
		[SerializeField]
		private Button btnRepair = null;
		
		private bool fixProblems;

		private void Update()
		{
			if (!PowerUpManager.Instance.FixProblemsActive)
			{
				return;
			}

			btnRepair.onClick.Invoke();
		}
	}
}