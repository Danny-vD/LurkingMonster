using Grid.Tiles.Buildings;
using UI.Market.MarketScreens;
using UnityEngine;
using VDFramework;

namespace UI.Market.ExtensionScreens
{
	[RequireComponent(typeof(AbstractMarketScreen))]
	public abstract class AbstractMarketExtension : BetterMonoBehaviour
	{
		[SerializeField, Header("Optionally: which screen to extend")]
		private AbstractMarketScreen extension;

		private void Awake()
		{
			extension = (extension != null) ? extension : GetComponent<AbstractMarketScreen>();

			extension.Extensions += ActivateExtension;
		}

		protected abstract void ActivateExtension(AbstractBuildingTile tile, MarketManager manager);
	}
}