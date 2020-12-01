using System;
using Events;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using UnityEngine;
using VDFramework.EventSystem;

namespace _1._Scripts.Tutorial
{
	public class RepairBuildingTutorial : Tutorial
	{
		[SerializeField]
		private Material material;
		
		[SerializeField]
		private string[] jsonKeys;
		
		private int index;
		
		private Building building;

		private AbstractBuildingTile abstractBuildingTile;
		
		private bool repairSoil;
		private bool repairFoundation;
		private bool repairBuilding;
		
		public override void StartTutorial(GameObject narrator)
		{
			base.StartTutorial(narrator);
			
			building                                                     = FindObjectOfType<Building>();
			abstractBuildingTile                                         = building.GetComponentInParent<AbstractBuildingTile>();
			abstractBuildingTile.GetComponent<Renderer>().sharedMaterial = material;

			building.GetComponent<BuildingHealth>().OnSoilRepair       += CheckSoilRepair;
			building.GetComponent<BuildingHealth>().OnFoundationRepair += CheckFoundationRepair;
			building.GetComponent<BuildingHealth>().OnBuildingRepair   += CheckBuildingRepair;
			
			OpenMarketEvent.ParameterlessListeners += ShowText;
		}

		private void ShowText()
		{
			EnableNarrator();

			if (index == jsonKeys.Length)
			{
				DisableNarrator();
				TutorialManager.Instance.CompletedTutorial();
				return;
			}
			
			SetText(jsonKeys[index]);
			index++;
		}
		
		private void CheckAllRepairs()
		{
			if (repairSoil && repairFoundation && repairBuilding)
			{
				TutorialManager.Instance.CompletedTutorial();
			}
		}
		
		private void CheckSoilRepair()
		{
			repairSoil = true;
			
			CheckAllRepairs();
		}

		private void CheckFoundationRepair()
		{
			repairFoundation = true;
			
			CheckAllRepairs();
		}

		private void CheckBuildingRepair()
		{
			repairBuilding = true;
			
			CheckAllRepairs();
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			OpenMarketEvent.ParameterlessListeners -= ShowText;
		}
	}
}