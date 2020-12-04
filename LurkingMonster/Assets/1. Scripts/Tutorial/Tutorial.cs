using System;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utility;
using VDFramework;

namespace _1._Scripts.Tutorial
{
	public class Tutorial : BetterMonoBehaviour
	{
		private string[] jsonKeys;

		[SerializeField]
		private bool moveCamera;

		[SerializeField]
		private Vector3 position;

		[SerializeField]
		private int jsonCount;

		private static int beginIndex = 1;
		
		public int Order;
		
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
			explainText.text = LanguageUtil.GetJsonString(jsonKey);
		}

		protected void SetNextButton(UnityAction onclick)
		{
			next.onClick.RemoveAllListeners();
			next.onClick.AddListener(onclick);
		}
		
		[ContextMenu("Json")]
		private void FillJsonKey()
		{
			if (jsonCount == 0)
			{
				return;
			}
			
			jsonKeys = new string[jsonCount];

			for (int i = 0; i < jsonCount; i++, beginIndex++)
			{
				jsonKeys[i] = $"TUTORIAL_{beginIndex}";
			}
		}
	}
}