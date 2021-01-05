using Events;
using TMPro;
using UnityEngine;
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

		public void ReloadText(params object[] variables)
		{
			text.text = GetText(textType, variables);
		}
        
        private static string GetText(string jsonKey)
        {
            return LanguageUtil.GetJsonString(jsonKey);
        }

		private static string GetText(string jsonKey, params object[] variables)
		{
			return LanguageUtil.GetJsonString(jsonKey, variables);
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
