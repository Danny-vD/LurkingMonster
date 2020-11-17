﻿using System;
using Events;
using Singletons;
using TMPro;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextLabels
{
	public class MoneyText : BetterMonoBehaviour
	{
		private TextMeshProUGUI moneyText;

		private void Awake()
		{
			moneyText = GetComponent<TextMeshProUGUI>();
		}

		private void Start()
		{
			SetText();
		}

		private void OnEnable()
		{
			AddListeners();
			SetText();
		}

		private void OnDisable()
		{
			RemoveListeners();
		}

		private void AddListeners()
		{
			EventManager.Instance.AddListener<MoneyChangedEvent>(OnMoneyChanged);
			EventManager.Instance.AddListener<LanguageChangedEvent>(SetText);
		}

		private void RemoveListeners()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<MoneyChangedEvent>(OnMoneyChanged);
			EventManager.Instance.RemoveListener<LanguageChangedEvent>(SetText);
		}

		private void SetText()
		{
			moneyText.text = $"{MoneyManager.Instance.CurrentMoney:N0}"; // Format to use seperator symbols and 0 decimals
		}

		private void OnMoneyChanged(MoneyChangedEvent moneyChangedEvent)
		{
			SetText();
		}
	}
}