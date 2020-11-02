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

		[SerializeField]
		private int destructionCost;

		[SerializeField, Tooltip("The cost of removing the debris")]
		private int cleanupCosts;

		public BuildingData(int rent, int weight, int price, int destructionCost, int cleanupCosts, SoilType soilType, FoundationType foundation)
		{
			this.rent         = rent;
			this.weight       = weight;
			this.price        = price;

			this.destructionCost = destructionCost;
			this.cleanupCosts    = cleanupCosts;

			SoilType     = soilType;
			Foundation   = foundation;
		}

		public int Rent => rent;
		
		public int Weight => weight;
		
		public int Price => price;

		public int DestructionCost => destructionCost;

		public int CleanupCosts => cleanupCosts;
		
		public SoilType SoilType { get; set; }

		public FoundationType Foundation { get; set; }
	}
}