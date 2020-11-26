using Enums;
using Events;
using ScriptableObjects;
using Singletons;
using Structs.Buildings.MeshData;
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
		private MeshRenderer meshRenderer;

		private BuildingType buildingType;

		private void Awake()
		{
			meshFilter   = GetComponent<MeshFilter>();
			meshRenderer = GetComponent<MeshRenderer>();

			building = GetComponent<Building>();

			building.OnInitialize += Initialize;
		}

		private void Initialize(BuildingData buildingData, FoundationTypeData foundationData, SoilTypeData soilData)
		{
			buildingType = building.BuildingType;

			if (!buildingMeshData)
			{
				Debug.LogError("Mesh Tier data is not set!", gameObject);
			}

			maxTier = buildingMeshData.GetMaxTier(buildingType);

			SetMeshToTier(building.CurrentTier);
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

			// TODO: move the payment to the market
			if (payForUpgrade)
			{
				int upgradeCost = building.UpgradeCost;

				if (!MoneyManager.Instance.PlayerHasEnoughMoney(upgradeCost))
				{
					return;
				}

				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(upgradeCost));
			}

			SetMeshToTier(++building.CurrentTier);
		}

		private void SetMeshToTier(int tier)
		{
			TierMeshData meshData = buildingMeshData.GetMeshData(buildingType, tier);
			meshFilter.mesh             = meshData.Mesh;
			meshRenderer.sharedMaterial = meshData.Material;
		}
	}
}