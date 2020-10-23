using Enums;
using Gameplay.Buildings;
using Structs.Buildings;
using UnityEngine;

namespace Grid.Tiles.Building
{
	using ScriptableObjects;

	public abstract class AbstractBuildingTile : AbstractTile
	{
		[SerializeField]
		protected BuildingType buildingType = default;

		[SerializeField]
		protected SoilType soilType = default;

		[SerializeField]
		protected FoundationType foundationType = default;

		public Gameplay.Buildings.Building Building { get; private set; }
		public bool HasFoundation => foundationObject || Building;

		protected BuildingData BuildingData; // The building data of the first tier building

		private BuildingSpawner spawner;
		private GameObject foundationObject;

		protected virtual void Awake()
		{
			spawner      = GetComponentInChildren<BuildingSpawner>();
			BuildingData = spawner.GetBuildingData(buildingType, foundationType, soilType)[0];
		}

		public virtual void SpawnBuilding()
		{
			RemoveFoundation(false);
			Building = spawner.Spawn(buildingType, foundationType, soilType);
		}

		public virtual void SpawnFoundation()
		{
			RemoveFoundation(false);
			foundationObject = spawner.SpawnFoundation(foundationType);
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

		public void SetSoilType(SoilType soil)
		{
			soilType = soil;
		}

		public SoilType GetSoilType()
		{
			return soilType;
		}

		public void SetFoundation(FoundationType foundation)
		{
			foundationType = foundation;
		}

		public FoundationType GetFoundationType()
		{
			return foundationType;
		}
	}
}