using System.Linq;
using Enums;
using Events.SoilSamplesManagement;
using Grid.Tiles.Buildings;
using UI.Market.MarketScreens;
using VDFramework.EventSystem;
using VDFramework.Extensions;

namespace UI.Market.ResearchFacility
{
	public class PowerupBuyScreen : AbstractMarketBuyScreen<PowerUpType>
	{
		protected override void OnSelectBuyButton(AbstractBuildingTile tile, PowerUpType buyType)
		{
		}

		protected override PowerUpType[] GetUnlockedTypes() => default(PowerUpType).GetValues().ToArray();

		//TODO: hookup with moneyManager
		protected override bool CanAffort(int price) => true;

		protected override int GetPrice(AbstractBuildingTile tile) => 1000;

		protected override void ReduceMoney(int price)
		{
			EventManager.Instance.RaiseEvent(new DecreaseSoilSamplesEvent(price));
		}
	}
}