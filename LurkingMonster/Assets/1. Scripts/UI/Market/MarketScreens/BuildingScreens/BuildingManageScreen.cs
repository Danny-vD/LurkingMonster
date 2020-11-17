using System.Collections.Generic;
using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Market.MarketScreens.BuildingScreens
{
	public class BuildingManageScreen : AbstractMarketScreen
	{
		[SerializeField]
		private List<Button> btnUpgrade;

		[SerializeField]
		private Button btnDemolish;
	
		[SerializeField]
		private Button btnRepair;
		
		public override void SetUI(AbstractBuildingTile tile, MarketManager manager)
		{
			
		}
	}
}