using System;
using Enums;
using UnityEngine;

namespace Structs
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

		// Don't serialize the enums because they are decided by the tile
		private SoilType soilType;
		private FoundationType foundation;

		public BuildingData(int rent, int weight, int price, SoilType soilType, FoundationType foundation)
		{
			this.rent       = rent;
			this.weight     = weight;
			this.price      = price;
			this.soilType   = soilType;
			this.foundation = foundation;
		}

		public int Rent
		{
			get => rent;
			set => rent = value;
		}

		public int Weight
		{
			get => weight;
			set => weight = value;
		}

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

		public int Price
		{
			get => price;
			set => price = value;
		}
	}
}