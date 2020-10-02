using Enums;
using Gameplay.Buildings;
using Singletons;
using Structs.Buildings;
using UnityEngine;

namespace Grid.Tiles
{
	public abstract class AbstractBuildingTile : AbstractTile
	{
		[SerializeField]
		protected BuildingType buildingType = default;

		[SerializeField]
		protected SoilType soilType = default;

		[SerializeField]
		protected FoundationType foundationType = default;

		public Building Building { get; private set; }
		
		protected BuildingData BuildingData; // The building data of the first tier building

		private BuildingSpawner spawner;

		protected virtual void Awake()
		{
			spawner      = GetComponentInChildren<BuildingSpawner>();
			BuildingData = spawner.GetBuildingData(buildingType, foundationType, soilType)[0];
		}

		public virtual void SpawnBuilding()
		{
			if (MoneyManager.Instance.PlayerHasEnoughMoney(GetBuildingPrice()))
			{
				Building = spawner.Spawn(buildingType, foundationType, soilType);
			}
		}

		public int GetBuildingPrice()
		{
			return BuildingData.Price;
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