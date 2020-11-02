using System;
using Enums;
using Events;
using Singletons;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	public class AutoRepair : BetterMonoBehaviour
	{
		[SerializeField]
		private Button btnRepair;
		
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