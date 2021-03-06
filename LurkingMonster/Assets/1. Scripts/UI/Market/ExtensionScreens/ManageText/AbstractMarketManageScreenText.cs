﻿using System;
using Grid.Tiles.Buildings;
using TMPro;
using UI.Market.MarketManagers;
using UnityEngine;
using Utility;

namespace UI.Market.ExtensionScreens.ManageText
{
	public abstract class AbstractMarketManageScreenText<TType> : AbstractMarketExtension
		where TType : struct, Enum
	{
		[SerializeField]
		private TextMeshProUGUI informationText;
		
		[SerializeField]
		private SerializableEnumDictionary<TType, string> jsonKeys;

		protected override void ActivateExtension(AbstractBuildingTile tile, AbstractMarketManager manager)
		{
			informationText.text = LanguageUtil.GetJsonString(jsonKeys[GetType(tile)]);
		}
		
		protected abstract TType GetType(AbstractBuildingTile tile);
	}
}