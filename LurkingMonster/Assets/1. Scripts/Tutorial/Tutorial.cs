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
		public int Order;
		
		private TextMeshProUGUI explainText;

		private GameObject narrator;

		protected GameObject arrow;

		private Button next;

		[SerializeField]
		private string[] jsonKeys;
		
		private int index;

		public virtual void StartTutorial(GameObject narrator, GameObject arrow)
		{
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
		
		protected void EnableNarrator()
		{
			narrator.SetActive(true);
		}

		protected void DisableNarrator()
		{
			narrator.SetActive(false);
		}

		protected void SetText(string jsonKey)
		{
			explainText.text = LanguageUtil.GetJsonString(jsonKey);
		}

		protected void SetNextButton(UnityAction onclick)
		{
			next.onClick.RemoveAllListeners();
			next.onClick.AddListener(onclick);
		}
	}
}