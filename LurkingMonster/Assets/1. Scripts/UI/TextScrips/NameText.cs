using System;
using Events;
using Singletons;
using TMPro;
using Utility;
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

			SetDefaultName();
			AddListener();
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
		
		private void AddListener()
		{
			LanguageChangedEvent.ParameterlessListeners += SetDefaultName;
		}

		private void RemoveListener()
		{
			LanguageChangedEvent.ParameterlessListeners -= SetDefaultName;
		}

		private void SetDefaultName()
		{
			text.text = LanguageUtil.GetJsonString("DEFAULT_NAME");
		}
		
		private void OnDestroy()
		{
			RemoveListener();
		}
	}
}