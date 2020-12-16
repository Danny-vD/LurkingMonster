using System;
using Singletons;
using TMPro;
using VDFramework;

namespace UI.TextScrips
{
	public class NameText : BetterMonoBehaviour
	{
		private TextMeshProUGUI text;

		private Action setName;

		private void Awake()
		{
			text = GetComponent<TextMeshProUGUI>();
		}

		private void Update()
		{
			if (string.IsNullOrEmpty(UserSettings.GameData.UserName))
			{
				return;
			}

			text.text = UserSettings.GameData.UserName;
			Destroy(this);
		}
	}
}