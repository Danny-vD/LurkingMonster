using Events;
using Events.BuildingEvents;
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
		private BuildingChangeTexture buildingChangeTexture;

		public void Awake()
		{
			crackPopup.SetActive(false);

			building       = GetComponent<Building>();
			buildingHealth = GetComponent<BuildingHealth>();
			buildingChangeTexture = GetComponent<BuildingChangeTexture>();
		}

		// Update is called once per frame
		private void Update()
		{
			float damage = Time.deltaTime; // Use 1 external call instead of 3.

			buildingHealth.DamageSoil(damage);
			buildingHealth.DamageFoundation(damage);
			buildingHealth.DamageBuilding(damage);

			buildingHealth.SetLowestHealthBar(bar);

			//TODO: make 3 seperate popups instead?
			//When health is less then 25% show cracks
			if (bar.slider.value <= bar.MaxValue / 100 * 25)
			{
				if (!crackPopup.activeInHierarchy)
				{
					crackPopup.SetActive(true);
					buildingChangeTexture.ChangeTexture(building);
				}
			}
			else if (crackPopup.activeInHierarchy)
			{
				// Necessesary in case the player repairs without clicking on popup
				crackPopup.SetActive(false);
			}

			if (bar.slider.value <= 0 && !PowerUpManager.Instance.AvoidMonsterFeedActive)
			{
				building.RemoveBuilding();
				EventManager.Instance.RaiseEvent(new BuildingConsumedEvent(building));
				VibrationUtil.Vibrate();
			}
		}

		public void CrackedPopupClicked()
		{
			crackPopup.SetActive(false);

			if (PowerUpManager.Instance.FixProblemsActive)
			{
				buildingHealth.ResetHealth();
				return;
			}

			EventManager.Instance.RaiseEvent(new OpenMarketEvent(building));
		}
	}
}