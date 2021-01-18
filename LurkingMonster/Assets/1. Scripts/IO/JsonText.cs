using Events;
using TMPro;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace IO
{
    [RequireComponent(typeof(TMP_Text))]
    public class JsonText : BetterMonoBehaviour
    {
        [SerializeField]
        private string textType = "PLACEHOLDER";
        
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
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
