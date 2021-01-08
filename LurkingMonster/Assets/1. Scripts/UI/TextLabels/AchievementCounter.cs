using Events.Achievements;
using Singletons;
using TMPro;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextLabels
{
	public class AchievementCounter : BetterMonoBehaviour
	{
		private TextMeshProUGUI text;
		
		private void Start()
		{
			text = GetComponent<TextMeshProUGUI>();
			EventManager.Instance.AddListener<AchievementUnlockedEvent>(ChangeText);
			
			if (UserSettings.SettingsExist)
			{
				ChangeText();
			}
		}

		private void ChangeText()
		{
			text.text = RewardManager.Instance.Counter.ToString();
		}
	}
}