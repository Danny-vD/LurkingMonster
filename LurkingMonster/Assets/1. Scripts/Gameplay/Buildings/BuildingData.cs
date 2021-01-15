using System;
using Enums;
using UnityEngine;

namespace Gameplay.Buildings
{
	/// <summary>
	/// Building Tier Data, but then with the Soil and Foundation type added
	/// </summary>
	[Serializable]
	public class BuildingData
	{
		[SerializeField]
		private int rent;

		[SerializeField]
		private float secondsPerRent;

		[SerializeField]
		private int weight;

		[SerializeField]
		private int price;

		[SerializeField]
		private int repairCost;

		[SerializeField]
		private int destructionCost;

		[SerializeField, Tooltip("The cost of removing the debris")]
		private int cleanupCosts;

		[SerializeField]
		private float maxHealth;

		public BuildingData(int rent, float secondsPerRent, int weight, int price, int repairCost, int destructionCost, int cleanupCosts,
			SoilType            soilType,
			FoundationType      foundation, float maxHealth)
		{
			this.rent           = rent;
			this.secondsPerRent = secondsPerRent;
			this.weight         = weight;
			this.price          = price;

			this.repairCost      = repairCost;
			this.destructionCost = destructionCost;
			this.cleanupCosts    = cleanupCosts;
			this.maxHealth       = maxHealth;

			SoilType   = soilType;
			Foundation = foundation;
		}

		public int Rent => rent;

		public float SecondsPerRent => secondsPerRent;

		public int Weight => weight;

		public int Price => price;

		public int RepairCost => repairCost;

		public int DestructionCost => destructionCost;

		public int CleanupCosts => cleanupCosts;

		public float MaxHealth => maxHealth;

		public SoilType SoilType { get; set; }

		public FoundationType Foundation { get; set; }
	}
}