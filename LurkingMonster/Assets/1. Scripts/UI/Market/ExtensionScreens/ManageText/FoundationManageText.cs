using Enums;
using Grid.Tiles.Buildings;

namespace UI.Market.ExtensionScreens.ManageText
{
	public sealed class FoundationManageText : AbstractMarketManageScreenText<FoundationType>
	{
		protected override FoundationType GetType(AbstractBuildingTile tile) => tile.GetFoundationType();
	}
}