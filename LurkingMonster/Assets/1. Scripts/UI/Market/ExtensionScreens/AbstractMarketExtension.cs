using Grid.Tiles.Buildings;
using UI.Market.MarketScreens;
using UnityEngine;
using VDFramework;

namespace UI.Market.ExtensionScreens
{
	[RequireComponent(typeof(AbstractMarketScreen))]
	public abstract class AbstractMarketExtension : BetterMonoBehaviour
	{
		private void Awake()
		{
			GetComponent<AbstractMarketScreen>().Extensions += ActivateExtension;
		}
		
		protected abstract void ActivateExtension(AbstractBuildingTile tile, MarketManager manager);
	}
}