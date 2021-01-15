using ScriptableObjects;
using UI.Buttons.Gameplay;
using UnityEngine;
using VDFramework;

namespace Gameplay.Buildings
{
	[RequireComponent(typeof(Building))]
	public class BuildingRent : BetterMonoBehaviour
	{
		private float waitTimeUntilRent = 5.0f;

		private float timer = 0.0f;
		private GameObject rentPopup;

		private Building building;
		private BuildingUpgrade buildingUpgrade;

		private void Awake()
		{
			building        = GetComponent<Building>();
			buildingUpgrade = GetComponent<BuildingUpgrade>();

			building.OnInitialize     += Initialize;
			buildingUpgrade.OnUpgrade += UpdateField;
		}

		public void Start()
		{
			ButtonCollectRent buttonCollectRent = GetComponentInChildren<ButtonCollectRent>(true);

			rentPopup = buttonCollectRent.gameObject;
			rentPopup.SetActive(false);
		}

		public void Update()
		{
			// Rent button not active
			if (!rentPopup.activeInHierarchy)
			{
				timer += Time.deltaTime;

				if (timer > waitTimeUntilRent)
				{
					rentPopup.SetActive(true);
					timer = 0.0f;
				}
			}
		}

		private void OnDestroy()
		{
			building.OnInitialize     -= Initialize;
			buildingUpgrade.OnUpgrade -= UpdateField;
		}

		private void UpdateField()
		{
			waitTimeUntilRent = building.Data.SecondsPerRent;
		}

		private void Initialize(BuildingData buildingData, FoundationTypeData foundationType, SoilTypeData soilType)
		{
			waitTimeUntilRent = buildingData.SecondsPerRent;
		}
	}
}