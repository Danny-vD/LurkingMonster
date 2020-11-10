using System;
using Events;
using Singletons;
using TMPro;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextLabels
{
	public class AchievementCounter : BetterMonoBehaviour
	{
		private TMPro.TextMeshProUGUI text;
		
		private void Start()
		{
			text = GetComponent<TextMeshProUGUI>();
			EventManager.Instance.AddListener<AchievementUnlockedEvent>(ChangeText);
		}

		private void ChangeText()
		{
			text.text = RewardManager.Instance.Counter.ToString();
		}
	}
}