using Enums;
using Events;
using ScriptableObjects;
using Singletons;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Buildings
{
	[RequireComponent(typeof(Building))]
	public class BuildingUpgrade : BetterMonoBehaviour
	{
		[SerializeField]
		private BuildingMeshData buildingMeshData = null;

		private int maxTier;

		private Building building;
		private MeshFilter meshFilter;
		private BuildingType buildingType;

		private void Awake()
		{
			meshFilter = GetComponent<MeshFilter>();

			building     = GetComponent<Building>();
			buildingType = building.BuildingType;

			if (!buildingMeshData)
			{
				Debug.LogError("Mesh Tier data is not set!", gameObject);
			}

			maxTier         = buildingMeshData.GetMaxTier(buildingType);
			meshFilter.mesh = buildingMeshData.GetMesh(buildingType, building.CurrentTier);
		}

		public bool CanUpgrade()
		{
			return building.CurrentTier < maxTier;
		}

		public void Upgrade(bool payForUpgrade)
		{
			if (!CanUpgrade())
			{
				return;
			}

			if (payForUpgrade)
			{
				int upgradeCost = building.UpgradeCost;

				if (!MoneyManager.Instance.PlayerHasEnoughMoney(upgradeCost))
				{
					return;
				}

				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(upgradeCost));
			}

			meshFilter.mesh = buildingMeshData.GetMesh(buildingType, ++building.CurrentTier);
		}
	}
}