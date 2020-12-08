using System;
using Enums;
using Gameplay.Buildings;
using UnityEngine;
using VDFramework;

namespace Animations
{
	[RequireComponent(typeof(Animator))]
	public class AnimateCrackPopup : BetterMonoBehaviour
	{
		private BuildingHealth buildingHealth;
		private Animator animator;
		private static readonly int building = Animator.StringToHash("Building");
		private static readonly int foundation = Animator.StringToHash("Foundation");
		private static readonly int soil = Animator.StringToHash("Soil");
		private static readonly int repair = Animator.StringToHash("Repair");

		private void Awake()
		{
			animator       = GetComponent<Animator>();
			buildingHealth = GetComponentInParent<BuildingHealth>();

			buildingHealth.OnBuildingRepair += TriggerRepairPopup;
			buildingHealth.OnFoundationRepair += TriggerRepairPopup;
			buildingHealth.OnSoilRepair += TriggerRepairPopup;
		}

		public void SetTrigger(BreakType breakType)
		{
			switch (breakType)
			{
				case BreakType.Building:
					TriggerPopup(building);
					break;
				case BreakType.Foundation:
					TriggerPopup(foundation);
					break;
				case BreakType.Soil:
					TriggerPopup(soil);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(breakType), breakType, null);
			}
		}

		private void SetActive(bool active)
		{
			CachedGameObject.SetActive(active);
		} 
		
		private void TriggerPopup(int trigger)
		{
			SetActive(true);

			animator.SetTrigger(trigger);
		}

		private void TriggerRepairPopup()
		{
			SetActive(false);

			animator.SetTrigger(repair);	
		}
	}
}