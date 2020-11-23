using Events;
using Singletons;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Buildings
{
	public class BuildingBreak : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject crackPopup = null;
		
		public Bar bar;

		private Building building;
		private BuildingHealth buildingHealth;

		public void Awake()
		{
			crackPopup.SetActive(false);
			
			building       = GetComponent<Building>();
			buildingHealth = GetComponent<BuildingHealth>();
		}

		// Start is called before the first frame update
		private void Start()
		{
			bar.SetMax((int) buildingHealth.MaxTotalHealth);
		}

		// Update is called once per frame
		private void Update()
		{
			float damage = Time.deltaTime; // Use 1 external call instead of 3.
			
			buildingHealth.DamageSoil(damage);
			buildingHealth.DamageFoundation(damage);
			buildingHealth.DamageBuilding(damage);

			bar.SetValue((int) buildingHealth.TotalHealth);

			//TODO: make 3 seperate popups instead?
			//When health is less then 25% show cracks
			if (buildingHealth.TotalHealth <= bar.MaxValue / 100 * 25 && !crackPopup.activeInHierarchy)
			{
				crackPopup.SetActive(true);
			}

			if (buildingHealth.TotalHealth <= 0 && !PowerUpManager.Instance.AvoidMonsterFeedActive)
			{
				building.RemoveBuilding();
				EventManager.Instance.RaiseEvent(new BuildingConsumedEvent(building));
				VibrationUtil.Vibrate();
			}
		}

		public void CrackedPopupClicked()
		{
			crackPopup.SetActive(false);
			
			if (!PowerUpManager.Instance.FixProblemsActive)
			{
				buildingHealth.ResetHealth();
				return;
			}

			EventManager.Instance.RaiseEvent(new OpenMarketEvent(building));
		}
	}
}