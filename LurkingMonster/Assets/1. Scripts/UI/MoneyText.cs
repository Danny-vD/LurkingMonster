using Enums;
using Events;
using Singletons;
using UnityEngine.UI;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace UI
{
	public class MoneyText : BetterMonoBehaviour
	{
		private Text moneyText;

		private void Start()
		{
			moneyText = GetComponent<Text>();
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
			moneyText.text =  LanguageUtil.GetJsonString("MONEY") + MoneyManager.Instance.CurrentMoney;
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