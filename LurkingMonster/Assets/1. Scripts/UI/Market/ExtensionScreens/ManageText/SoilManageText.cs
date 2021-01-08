using Enums;
using Grid.Tiles.Buildings;

namespace UI.Market.ExtensionScreens.ManageText
{
	public class SoilManageText : AbstractMarketManageScreenText<SoilType>
	{
		protected override SoilType GetType(AbstractBuildingTile tile) => tile.GetSoilType();
	}
}