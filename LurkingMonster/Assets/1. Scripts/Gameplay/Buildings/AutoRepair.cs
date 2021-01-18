using Singletons;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace Gameplay.Buildings
{
	[RequireComponent(typeof(Button))]
	public class AutoRepair : BetterMonoBehaviour
	{
		private Button button;

		private void Awake()
		{
			button = GetComponent<Button>();
		}

		private void Update()
		{
			if (!PowerUpManager.Instance.FixProblemsActive)
			{
				return;
			}

			button.onClick.Invoke();
		}
	}
}