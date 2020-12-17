using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework;

namespace UI
{
	public class InformationPopup : BetterMonoBehaviour
	{
		[SerializeField]
		private Sprite sprite;

		[SerializeField]
		private GameObject popup;
		
		[SerializeField]
		private string jsonTitle;

		[SerializeField]
		private string jsonExplain;

		[SerializeField]
		private Image icon;
		
		private TextMeshProUGUI title;
		
		private TextMeshProUGUI explain;
		
		private TextMeshProUGUI[] text;
		
		private Button button;
		
		private void Start()
		{
			TextMeshProUGUI[] temp = popup.GetComponentsInChildren<TextMeshProUGUI>();
			
			title  = temp[0];
			explain  = temp[1];
			
			button = GetComponent<Button>();
			button.onClick.AddListener(GenerateContent);	
		}

		private void GenerateContent()
		{
			popup.SetActive(true);
			icon.sprite = sprite;

			title.text = LanguageUtil.GetJsonString(jsonTitle);
			explain.text = LanguageUtil.GetJsonString(jsonExplain);
		}
	}
}