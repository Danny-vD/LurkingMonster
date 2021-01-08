using Gameplay.Buildings;
using UnityEngine;
using VDFramework.Extensions;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Building Data/BuildingTier Data")]
	public class BuildingTierData : ScriptableObject
	{
		[SerializeField]
		private GameObject[] Prefabs = new GameObject[0];

		[Space]
		[SerializeField, Tooltip("The percentage of the price that will be collected as rent")]
		private int rentPercentage = 30;

		[SerializeField]
		private int weight = 100;

		[SerializeField]
		private int price = 10;
		
		[SerializeField]
		private int repairPrice = 50;
		
		[SerializeField]
		private int destructionCost = 50;

		[SerializeField, Tooltip("The cost of removing the debris")]
		private int cleanupCosts = 500;

		[SerializeField]
		private float maxHealth = 100;

		public BuildingData GetStruct()
		{
			return new BuildingData(GetPricePercentage(rentPercentage), weight, price, repairPrice, destructionCost, cleanupCosts, default, default, maxHealth);
		}

		public GameObject GetPrefab()
		{
			return Prefabs.GetRandomItem();
		}
		
		
		private int GetPricePercentage(int percentage)
		{
			return (int) (percentage / 100.0f * price);
		}
	}
}