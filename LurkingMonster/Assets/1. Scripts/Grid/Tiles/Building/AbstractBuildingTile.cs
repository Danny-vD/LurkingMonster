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

		protected BuildingData BuildingData; // The building data of the first tier building

		private BuildingSpawner spawner;
		private GameObject foundationObject;

		protected virtual void Awake()
		{
			spawner      = GetComponentInChildren<BuildingSpawner>();
			BuildingData = spawner.GetBuildingData(buildingType, default, default)[0];
		}

		public virtual void SpawnBuilding()
		{
			RemoveFoundation(false);
			Building = spawner.Spawn(buildingType, BuildingData.Foundation, BuildingData.SoilType);
		}

		public virtual void SpawnFoundation()
		{
			RemoveFoundation(false);
			foundationObject = spawner.SpawnFoundation(BuildingData.Foundation);
		}

		public virtual void RemoveFoundation(bool payForRemoval)
		{
			Destroy(foundationObject);
		}

		public int GetBuildingPrice()
		{
			return BuildingData.Price;
		}

		public FoundationTypeData GetFoundationData(FoundationType foundation)
		{
			return spawner.GetFoundationData(foundation);
		}

		public void SetBuildingType(BuildingType house)
		{
			buildingType = house;
		}

		public BuildingType GetBuildingType()
		{
			return buildingType;
		}

		public void SetSoilType(SoilType soil)
		{
			BuildingData.SoilType = soil;
		}

		public SoilType GetSoilType()
		{
			return BuildingData.SoilType;
		}

		public void SetFoundation(FoundationType foundation)
		{
			BuildingData.Foundation = foundation;
		}

		public FoundationType GetFoundationType()
		{
			return BuildingData.Foundation;
		}
	}
}