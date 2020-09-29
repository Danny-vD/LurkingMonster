using System;
using Enums;
using UnityEngine;

namespace Structs.Buildings
{
	[Serializable]
	public struct BuildingData
	{
		[SerializeField]
		private int rent;

		[SerializeField]
		private int weight;

		[SerializeField]
		private int price;

		// Don't serialize the enums because they are decided by the player
		private SoilType soilType;
		private FoundationType foundation;

		public BuildingData(int rent, int weight, int price, SoilType soilType, FoundationType foundation)
		{
			this.rent         = rent;
			this.weight       = weight;
			this.price        = price;
			this.soilType     = soilType;
			this.foundation   = foundation;
		}

		public int Rent => rent;

		public int Weight => weight;

		public int Price => price;
		
		public SoilType SoilType
		{
			get => soilType;
			set => soilType = value;
		}

		public FoundationType Foundation
		{
			get => foundation;
			set => foundation = value;
		}
	}
}