using System;
using Events;
using Singletons;
using TMPro;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextScrips
{
	public class NameText : BetterMonoBehaviour
	{
		private TextMeshProUGUI text;
		
		private void Awake()
		{
			text = GetComponent<TextMeshProUGUI>();

			SetDefaultName();
			AddListener();
		}

		private void AddListener()
		{
			LanguageChangedEvent.ParameterlessListeners += SetDefaultName;
			EventManager.Instance.AddListener<InputChangedEvent>(SetName); 
		}

		private void SetName()
		{
			text.text = UserSettings.GameData.UserName;
		}
		
		private void RemoveListener()
		{
			LanguageChangedEvent.ParameterlessListeners -= SetDefaultName;
			EventManager.Instance.RemoveListener<InputChangedEvent>(SetName);
		}

		private void SetDefaultName()
		{
			text.text = LanguageUtil.GetJsonString("DEFAULT_NAME");
		}
		
		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			RemoveListener();
		}
	}
}