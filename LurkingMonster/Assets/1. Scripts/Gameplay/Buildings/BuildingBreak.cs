using Animations;
using Enums;
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
		private AnimateCrackPopup animateCrackPopup;

		public Bar bar;

		private Building building;
		private BuildingHealth buildingHealth;
		private BuildingChangeTexture buildingChangeTexture;

		public void Awake()
		{
			//todo
			//popups.SetActive(false);
			building              = GetComponent<Building>();
			buildingHealth        = GetComponent<BuildingHealth>();
			buildingChangeTexture = GetComponent<BuildingChangeTexture>();
			animateCrackPopup     = GetComponentInChildren<AnimateCrackPopup>();
		}

		// Update is called once per frame
		private void Update()
		{
			float damage = Time.deltaTime; // Use 1 external call instead of 3.

			buildingHealth.DamageSoil(damage);
			buildingHealth.DamageFoundation(damage);
			buildingHealth.DamageBuilding(damage);

			BreakType breakType = buildingHealth.SetLowestHealthBar(bar);
			
			ShowPopup(breakType);
			
			if (bar.slider.value <= 0 && !PowerUpManager.Instance.AvoidMonsterFeedActive)
			{
				building.RemoveBuilding();
				EventManager.Instance.RaiseEvent(new BuildingConsumedEvent(building));
				VibrationUtil.Vibrate();
			}
		}

		private void ShowPopup(BreakType breakType)
		{
			//When health is less then 25% show cracks
			if (bar.slider.value <= bar.MaxValue / 100 * 25)
			{
				if (!animateCrackPopup.gameObject.activeInHierarchy)
				{
					animateCrackPopup.SetTrigger(breakType);
					buildingChangeTexture.ChangeTexture(building);
				}
			}
			else if (animateCrackPopup.gameObject.activeInHierarchy)
			{
				// Necessesary in case the player repairs without clicking on popup
				animateCrackPopup.gameObject.SetActive(false);
			}
		}

		public void CrackedPopupClicked()
		{
			if (PowerUpManager.Instance.FixProblemsActive)
			{
				buildingHealth.ResetHealth();
				return;
			}

			EventManager.Instance.RaiseEvent(new OpenMarketEvent(building));
		}
	}
}