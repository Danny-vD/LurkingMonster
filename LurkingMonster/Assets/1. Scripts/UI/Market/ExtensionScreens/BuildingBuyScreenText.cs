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

			List<BuildingButtonData> buildingButtons = GetComponent<BuildingBuyScreen>().GetBuildingButtons();
			
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
				typeTextWriter = new StringVariableWriter(buttonData.Texts.Type.text);
			}

			buttonData.Texts.Type.text = typeTextWriter.UpdateText(buttonData.Key.ToString().InsertSpaceBeforeCapitals());
			
			// Price
			if (priceTextWriter == null)
			{
				priceTextWriter = new StringVariableWriter(buttonData.Texts.Price.text);
			}

			buttonData.Texts.Price.text = priceTextWriter.UpdateText(data[0].Price);
			
			// Health
			if (healthTextWriter == null)
			{
				healthTextWriter = new StringVariableWriter(buttonData.Texts.Health.text);
			}

			buttonData.Texts.Health.text = healthTextWriter.UpdateText(data[0].MaxHealth);
			
			// Rent
			if (rentTextWriter == null)
			{
				rentTextWriter = new StringVariableWriter(buttonData.Texts.Rent.text);
			}

			buttonData.Texts.Rent.text = rentTextWriter.UpdateText(data[0].Rent);
			
			// Upgrades
			if (upgradeTextWriter == null)
			{
				upgradeTextWriter = new StringVariableWriter(buttonData.Texts.Upgrades.text);
			}

			buttonData.Texts.Upgrades.text = upgradeTextWriter.UpdateText(data.Length);
		}
	}
}