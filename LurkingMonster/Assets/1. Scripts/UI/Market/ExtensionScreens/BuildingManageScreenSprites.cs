using Enums;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using UI.Market.MarketManagers;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Market.ExtensionScreens
{
	public class BuildingManageScreenSprites : AbstractMarketExtension
	{
		[SerializeField]
		private Image currentBuildingSprite;
		
		[SerializeField]
		private Image nextBuildingSprite;

		[SerializeField]
		private SerializableEnumDictionary<BuildingType, SerializableDictionary<int, Sprite>> sprites;
		
		protected override void ActivateExtension(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			SetSpriteToCurrent(currentBuildingSprite, tile.Building);

			BuildingUpgrade buildingUpgrade = tile.Building.GetComponent<BuildingUpgrade>();
			
			if (buildingUpgrade.CanUpgrade())
			{
				SetSpriteToNext(nextBuildingSprite, tile.Building);
				return;
			}
			
			SetSpriteToCurrent(nextBuildingSprite, tile.Building);
		}

		private void SetSpriteToCurrent(Image image, Building building)
		{
			image.sprite = sprites[building.BuildingType][building.CurrentTier];
		}

		private void SetSpriteToNext(Image image, Building building)
		{
			image.sprite = sprites[building.BuildingType][building.CurrentTier + 1];
		}
	}
}
