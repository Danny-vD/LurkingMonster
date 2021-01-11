using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;
using VDFramework;

namespace Tutorials
{
	public class Tutorial : BetterMonoBehaviour
	{
		public int Order;
		
		[SerializeField]
		private int jsonCount;

		[SerializeField]
		private bool moveCamera;

		[SerializeField]
		private Vector3 position;

		private static int beginIndex = 1;

		private string[] jsonKeys;

		private TextMeshProUGUI explainText;

		protected GameObject Arrow;

		private Button next;

		private int index;

		private Camera playerCamera;

		protected TutorialManager manager;

		protected int keyCount => jsonCount;
		
		private void Awake()
		{
			playerCamera = Camera.main;
		}

		public virtual void StartTutorial(GameObject arrow, TutorialManager manager)
		{
			FillJsonKey();
			
			if (moveCamera)
			{
				playerCamera.transform.position = position;
			}
			
			Arrow        = arrow;
			this.manager = manager;
			
			GameObject narrator = manager.Narrator;
			explainText = narrator.GetComponentInChildren<TextMeshProUGUI>();
			next        = narrator.GetComponentInChildren<Button>();
			index       = 0;
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
			manager.EnableNarrator();
		}

		private void DisableNarrator()
		{
			manager.DisableNarrator();
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