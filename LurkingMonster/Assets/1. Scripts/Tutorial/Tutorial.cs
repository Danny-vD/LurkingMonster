using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;
using VDFramework;

namespace Tutorial
{
	public class Tutorial : BetterMonoBehaviour
	{
		public int Order;
		
		private string[] jsonKeys;

		[SerializeField]
		private bool moveCamera;

		[SerializeField]
		private Vector3 position;

		[SerializeField]
		private int jsonCount;

		private static int beginIndex = 1;

		private TextMeshProUGUI explainText;

		private GameObject narrator;

		protected GameObject arrow;

		private Button next;

		private int index;

		private Camera playerCamera;

		private void Awake()
		{
			playerCamera = Camera.main;
		}

		public virtual void StartTutorial(GameObject narrator, GameObject arrow)
		{
			FillJsonKey();

			if (moveCamera)
			{
				playerCamera.transform.position = position;
			}
			
			this.narrator = narrator;
			this.arrow    = arrow;
			explainText   = narrator.GetComponentInChildren<TextMeshProUGUI>();
			next          = narrator.GetComponentInChildren<Button>();
			index         = 0;
		}

		protected virtual void ReachEndOfText()
		{
		}
		
		protected void ShowNextText()
		{
			EnableNarrator();

			if (index == jsonKeys.Length)
			{
				DisableNarrator();
				ReachEndOfText();
				return;
			}
			
			SetText(jsonKeys[index]);
			index++;
		}

		private void EnableNarrator()
		{
			narrator.SetActive(true);
		}

		private void DisableNarrator()
		{
			narrator.SetActive(false);
		}

		private void SetText(string jsonKey)
		{
			explainText.text = LanguageUtil.GetJsonString(jsonKey, UserSettings.GameData.UserName, UserSettings.GameData.CityName);
		}

		protected void SetNextButton(UnityAction onclick)
		{
			next.onClick.RemoveAllListeners();
			next.onClick.AddListener(onclick);
		}
		
		[ContextMenu("Json")]
		private void FillJsonKey()
		{
			jsonKeys = new string[jsonCount];
			
			if (jsonCount == 0)
			{
				return;
			}

			for (int i = 0; i < jsonCount; i++, beginIndex++)
			{
				jsonKeys[i] = $"TUTORIAL_{beginIndex}";
			}
		}
	}
}