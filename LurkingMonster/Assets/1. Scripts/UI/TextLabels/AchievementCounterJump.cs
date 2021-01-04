using System;
using System.Collections;
using System.Collections.Generic;
using Events.Achievements;
using Events.MoneyManagement;
using Singletons;
using UI.Bounce;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextLabels
{
	public class AchievementCounterJump : AbstractBounce<AchievementUnlockedEvent>
	{
		protected override void Start()
		{
		}

		private void Update()
		{
			if (RewardManager.Instance.Counter > 0 && gameObject.activeInHierarchy && !isRunning)
			{
				StartBounce();
				print("Test");
			}
		}
	}
}