using System;
using Enums;
using Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace IO
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class JsonText : BetterMonoBehaviour
    {
        [SerializeField]
        private string textType = "PLACEHOLDER";
        
        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            ReloadText();
			EventManager.Instance.AddListener<LanguageChangedEvent>(ReloadText);
        }

        public void ReloadText()
        {
            text.text = GetText(textType);
        }
        
        private static string GetText(string jsonKey)
        {
            return LanguageUtil.GetJsonString(jsonKey);
        }

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			EventManager.Instance.RemoveListener<LanguageChangedEvent>(ReloadText);
		}
	}
}
