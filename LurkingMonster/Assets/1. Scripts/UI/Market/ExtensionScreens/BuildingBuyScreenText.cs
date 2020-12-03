using System.Collections.Generic;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using Structs.Market;
using UI.Market.MarketScreens.BuildingScreens;
using UnityEngine;
using VDFramework.Extensions;
using VDFramework.Utility;

namespace UI.Market.ExtensionScreens
{
	[RequireComponent(typeof(BuildingBuyScreen))]
	public class BuildingBuyScreenText : AbstractMarketExtension
	{
		private bool hasSetText;

		private StringVariableWriter typeTextWriter;
		private StringVariableWriter priceTextWriter;
		private StringVariableWriter healthTextWriter;
		private StringVariableWriter rentTextWriter;
		private StringVariableWriter upgradeTextWriter;
		
		protected override void ActivateExtension(AbstractBuildingTile tile, MarketManager manager)
		{
			if (hasSetText)
			{
				return;
			}

			hasSetText = true;

			List<BuildingButtonData> buildingButtons = GetComponent<BuildingBuyScreen>().GetbuyButtonData();
			
			foreach (BuildingButtonData buttonData in buildingButtons)
			{
				SetText(buttonData, tile);
			}
		}

		private void SetText(BuildingButtonData buttonData, AbstractBuildingTile tile)
		{
			BuildingData[] data = tile.GetBuildingData(buttonData.Key);
			
			// Type
			if (typeTextWriter == null)
			{
				typeTextWriter = new StringVariableWriter(buttonData.Text.Type.text);
			}

			buttonData.Text.Type.text = typeTextWriter.UpdateText(buttonData.Key.ToString().InsertSpaceBeforeCapitals());
			
			// Price
			if (priceTextWriter == null)
			{
				priceTextWriter = new StringVariableWriter(buttonData.Text.Price.text);
			}

			buttonData.Text.Price.text = priceTextWriter.UpdateText(data[0].Price);
			
			// Health
			if (healthTextWriter == null)
			{
				healthTextWriter = new StringVariableWriter(buttonData.Text.Health.text);
			}

			buttonData.Text.Health.text = healthTextWriter.UpdateText(data[0].MaxHealth);
			
			// Rent
			if (rentTextWriter == null)
			{
				rentTextWriter = new StringVariableWriter(buttonData.Text.Rent.text);
			}

			buttonData.Text.Rent.text = rentTextWriter.UpdateText(data[0].Rent);
			
			// Upgrades
			if (upgradeTextWriter == null)
			{
				upgradeTextWriter = new StringVariableWriter(buttonData.Text.Upgrades.text);
			}

			buttonData.Text.Upgrades.text = upgradeTextWriter.UpdateText(data.Length);
		}
	}
}