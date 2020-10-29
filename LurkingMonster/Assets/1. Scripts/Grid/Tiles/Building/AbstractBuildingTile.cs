using Enums;
using Gameplay.Buildings;
using UnityEngine;

namespace Grid.Tiles.Building
{
	using ScriptableObjects;

	public abstract class AbstractBuildingTile : AbstractTile
	{
		[SerializeField]
		protected BuildingType buildingType = default;

		public Gameplay.Buildings.Building Building { get; private set; }
		public bool HasFoundation => foundationObject || Building;

		protected BuildingData FirstTierData; // The building data of the first tier building

		private BuildingSpawner spawner;
		private GameObject foundationObject;

		protected virtual void Awake()
		{
			spawner      = GetComponentInChildren<BuildingSpawner>();
			FirstTierData = spawner.GetBuildingData(buildingType, default, default)[0];
		}

		public virtual void SpawnBuilding()
		{
			RemoveFoundation(false);
			Building = spawner.Spawn(buildingType, FirstTierData.Foundation, FirstTierData.SoilType);
		}

		public virtual void SpawnFoundation()
		{
			RemoveFoundation(false);
			foundationObject = spawner.SpawnFoundation(FirstTierData.Foundation);
		}

		public virtual void RemoveFoundation(bool payForRemoval)
		{
			Destroy(foundationObject);
		}

		public int GetBuildingPrice()
		{
			return FirstTierData.Price;
		}

		public FoundationTypeData GetFoundationData(FoundationType foundation)
		{
			return spawner.GetFoundationData(foundation);
		}

		public void SetBuildingType(BuildingType building)
		{
			buildingType  = building;
			FirstTierData = spawner.GetBuildingData(buildingType, default, FirstTierData.SoilType)[0];
		}

		public BuildingType GetBuildingType()
		{
			return buildingType;
		}

		public void SetSoilType(SoilType soil)
		{
			FirstTierData.SoilType = soil;
		}

		public SoilType GetSoilType()
		{
			return FirstTierData.SoilType;
		}

		public void SetFoundation(FoundationType foundation)
		{
			FirstTierData.Foundation = foundation;
		}

		public FoundationType GetFoundationType()
		{
			return FirstTierData.Foundation;
		}
	}
}