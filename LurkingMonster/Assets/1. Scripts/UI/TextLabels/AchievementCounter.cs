using Events.Achievements;
using Singletons;
using TMPro;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextLabels
{
	public class AchievementCounter : BetterMonoBehaviour
	{
		private GameObject parent;
		private TextMeshProUGUI text;
		
		private void Start()
		{
			text = GetComponent<TextMeshProUGUI>();
			EventManager.Instance.AddListener<AchievementUnlockedEvent>(ChangeText);
			parent = transform.parent.gameObject;
			
			parent.SetActive(false);
			
			if (UserSettings.SettingsExist)
			{
				ChangeText();
			}
		}

		private void ChangeText()
		{
			text.text = RewardManager.Instance.Counter.ToString();
			parent.SetActive(int.Parse(text.text) != 0);
		}
	}
}