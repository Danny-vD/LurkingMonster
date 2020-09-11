using Enums;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace IO
{
    [RequireComponent(typeof(Text))]
    public class JsonText : BetterMonoBehaviour
    {
        [SerializeField]
        private string textType = "PLACEHOLDER";
        
        private Text text;

        private void Awake()
        {
            text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            ReloadText();
        }

        public void ReloadText()
        {
            text.text = GetText(textType);
        }
        
        private static string GetText(string pType)
        {
            return JsonVariables.Instance.GetVariable(pType, LanguageSettings.Language.ToString());
        }
    }
}
