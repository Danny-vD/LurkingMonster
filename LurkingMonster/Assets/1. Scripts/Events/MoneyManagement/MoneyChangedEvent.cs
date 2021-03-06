﻿using VDFramework.EventSystem;

namespace Events.MoneyManagement
{
	public class MoneyChangedEvent : VDEvent<MoneyChangedEvent>
	{
		public readonly int CurrentMoney;
		public readonly int DeltaMoney;

		public MoneyChangedEvent(int currentMoney, int deltaMoney)
		{
			CurrentMoney = currentMoney;
			DeltaMoney = deltaMoney;
		}
	}
}