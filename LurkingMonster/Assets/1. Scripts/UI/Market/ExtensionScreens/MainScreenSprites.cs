using System.Collections.Generic;
using Enums;
using Grid.Tiles.Buildings;
using Structs.Market;
using UI.Market.MarketScreens;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Market.ExtensionScreens
{
	[RequireComponent(typeof(MainScreen))]
	public class MainScreenSprites : AbstractMarketExtension
	{
		private enum SpriteType
		{
			Building,
			BuildingTile,
			Foundation,
			Soil,
		}

		[SerializeField]
		private SerializableEnumDictionary<SpriteType, Image> images;

		[SerializeField]
		private SerializableEnumDictionary<SpriteType, Sprite> blockedSprites;

		[SerializeField]
		private SerializableEnumDictionary<SpriteType, Sprite> emptySprites;

		[SerializeField]
		private SerializableEnumDictionary<BuildingType, SerializableDictionary<int, Sprite>> buildingSprites;
		
		[SerializeField]
		private SerializableEnumDictionary<BuildingType, SerializableDictionary<int, Sprite>> brokenBuildingSprites;

		[SerializeField]
		private Sprite buildingTileSprite;

		[SerializeField]
		private SerializableEnumDictionary<FoundationType, Sprite> foundationSprites;

		[SerializeField]
		private SerializableEnumDictionary<SoilType, Sprite> soilSprites;

		protected override void ActivateExtension(AbstractBuildingTile tile, MarketManager manager)
		{
			SetSprites(tile, GetComponent<MainScreen>().GetScreenData(tile));
		}

		private void SetSprites(AbstractBuildingTile tile, MainScreenData screenData)
		{
			if (screenData.HasDebris)
			{
				foreach (KeyValuePair<SpriteType, Image> pair in images)
				{
					if (pair.Key == SpriteType.BuildingTile)
					{
						SetSprite(pair.Key, brokenBuildingSprites[screenData.BuildingType][tile.DestroyedBuildingTier]);
					}
					
					SetSprite(pair.Key, blockedSprites[pair.Key]);
				}

				return;
			}

			if (screenData.HasBuilding)
			{
				SetSprite(SpriteType.Building, buildingSprites[screenData.BuildingType][tile.Building.CurrentTier]);
				SetSprite(SpriteType.BuildingTile, buildingTileSprite);
				SetSprite(SpriteType.Foundation, foundationSprites[screenData.FoundationType]);
				SetSprite(SpriteType.Soil, soilSprites[screenData.SoilType]);
				return;
			}

			if (screenData.HasFoundation)
			{
				SetSprite(SpriteType.Building, emptySprites[SpriteType.Building]);
				SetSprite(SpriteType.BuildingTile, emptySprites[SpriteType.BuildingTile]);
				SetSprite(SpriteType.Foundation, foundationSprites[screenData.FoundationType]);
				SetSprite(SpriteType.Soil, soilSprites[screenData.SoilType]);
				return;
			}

			if (screenData.HasSoil)
			{
				SetSprite(SpriteType.Building, blockedSprites[SpriteType.Building]);
				SetSprite(SpriteType.BuildingTile, blockedSprites[SpriteType.BuildingTile]);
				SetSprite(SpriteType.Foundation, emptySprites[SpriteType.Foundation]);
				SetSprite(SpriteType.Soil, soilSprites[screenData.SoilType]);
				return;
			}

			foreach (KeyValuePair<SpriteType, Image> pair in images)
			{
				SetSprite(pair.Key, blockedSprites[pair.Key]);
			}
		}

		private void SetSprite(SpriteType spriteType, Sprite sprite)
		{
			Image image = images[spriteType];

			image.enabled = sprite != null;

			image.sprite = sprite;
		}
	}
}