using System;
using Enums;
using UnityEngine;

namespace Gameplay.Buildings
{
	[Serializable]
	public class BuildingData
	{
		[SerializeField]
		private int rent;

		[SerializeField]
		private int weight;

		[SerializeField]
		private int price;

		public BuildingData(int rent, int weight, int price, SoilType soilType, FoundationType foundation)
		{
			this.rent         = rent;
			this.weight       = weight;
			this.price        = price;
			this.SoilType     = soilType;
			this.Foundation   = foundation;
		}

		public int Rent => rent;
		
		public int Weight => weight;
		
		public int Price => price;
		
		public SoilType SoilType { get; set; }

		public FoundationType Foundation { get; set; }
	}
}