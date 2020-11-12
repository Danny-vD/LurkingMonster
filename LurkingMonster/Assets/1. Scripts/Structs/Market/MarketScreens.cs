using System;
using System.Collections.Generic;
using System.Linq;
using UI.Market.MarketScreens;
using UnityEngine;

namespace Structs.Market
{
	[Serializable]
	public struct MarketScreens
	{
		[SerializeField]
		private AbstractMarketScreen mainScreen;

		[Space(20), SerializeField]
		private AbstractMarketScreen buildingBuyScreen;

		[SerializeField]
		private AbstractMarketScreen buildingManageScreen;

		[Space(20), SerializeField]
		private AbstractMarketScreen foundationBuyScreen;

		[SerializeField]
		private AbstractMarketScreen foundationManageScreen;

		[Space(20), SerializeField]
		private AbstractMarketScreen soilBuyScreen;

		[SerializeField]
		private AbstractMarketScreen soilManageScreen;

		private List<AbstractMarketScreen> screens;

		public AbstractMarketScreen MainScreen => mainScreen;

		public AbstractMarketScreen BuildingBuyScreen => buildingBuyScreen;

		public AbstractMarketScreen BuildingManageScreen => buildingManageScreen;

		public AbstractMarketScreen FoundationBuyScreen => foundationBuyScreen;

		public AbstractMarketScreen FoundationManageScreen => foundationManageScreen;

		public AbstractMarketScreen SoilBuyScreen => soilBuyScreen;

		public AbstractMarketScreen SoilManageScreen => soilManageScreen;

		public List<AbstractMarketScreen> Screens => screens ?? InitializeScreens();

		private List<AbstractMarketScreen> InitializeScreens()
		{
			screens = new List<AbstractMarketScreen>()
			{
				mainScreen, buildingBuyScreen, buildingManageScreen, foundationBuyScreen,
				foundationManageScreen, soilBuyScreen, soilManageScreen
			};

			// HACK: temporary get rid of the nulls
			screens = screens.Where(screen => screen != null).ToList();

			return screens;
		}
	}
}