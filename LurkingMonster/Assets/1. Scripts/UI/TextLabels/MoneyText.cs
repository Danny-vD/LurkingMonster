using Enums;
using Events;
using Singletons;
using TMPro;
using UnityEngine.UI;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextLabels
{
	public class MoneyText : BetterMonoBehaviour
	{
		private TextMeshProUGUI moneyText;

		private void Start()
		{
			moneyText = GetComponent<TextMeshProUGUI>();
			AddMoneyListener();

			SetText();
		}

		private void AddMoneyListener()
		{
			EventManager.Instance.AddListener<MoneyChangedEvent>(OnMoneyChanged);
			EventManager.Instance.AddListener<LanguageChangedEvent>(SetText);
		}

		private void SetText()
		{
			moneyText.text = MoneyManager.Instance.CurrentMoney.ToString();
		}

		private void OnMoneyChanged(MoneyChangedEvent moneyChangedEvent)
		{
			SetText();
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<MoneyChangedEvent>(OnMoneyChanged);
		}
	}
}