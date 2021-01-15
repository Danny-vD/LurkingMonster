using System;
using System.Linq;
using Enums;
using Events.SoilSamplesManagement;
using Gameplay;
using Grid.Tiles.Buildings;
using Singletons;
using UI.Market.MarketManagers;
using UI.Market.MarketScreens;
using VDFramework.EventSystem;
using VDFramework.Extensions;

namespace UI.Market.ResearchFacility
{
	public class PowerupBuyScreen : AbstractMarketBuyScreen<PowerUpType>
	{
		private PowerUpType selectedPowerup;
		
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, PowerUpType buyType)
		{
			selectedPowerup = buyType;
		}

		protected override PowerUpType[] GetUnlockedTypes() => default(PowerUpType).GetValues().ToArray();

		protected override bool CanAffort(int price) => MoneyManager.Instance.CurrentSoilSamples >= price;

		protected override int GetPrice(AbstractBuildingTile tile) => PowerUpManager.Instance.GetPowerUp(selectedPowerup).Price;

		protected override void BuyButtonClick(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			base.BuyButtonClick(tile, manager);

			switch (selectedPowerup)
			{
				case PowerUpType.AvoidMonster:
					PowerUpManager.Instance.AvoidMonsters++;
					break;
				case PowerUpType.FixProblems:
					PowerUpManager.Instance.FixProblems++;
					break;
				case PowerUpType.AvoidWeatherEvent:
					PowerUpManager.Instance.AvoidWeather++;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		protected override void ReduceMoney(int price)
		{
			EventManager.Instance.RaiseEvent(new DecreaseSoilSamplesEvent(price));
		}
	}
}