using System;
using Events;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.EventSystem;

namespace _1._Scripts.Tutorial
{
	public class RepairTutorial : Tutorial
	{
		[SerializeField]
		private Material material;
		
		[SerializeField]
		private Button manageScreen;

		private Building building;

		private AbstractBuildingTile abstractBuildingTile;
		
		private bool repairSoil, repairFoundation, repairBuilding;

		[SerializeField]
		private bool checkSoil, checkFoundation, checkBuilding;
		
		public override void StartTutorial(GameObject narrator)
		{
			base.StartTutorial(narrator);
			
			building                                                     = FindObjectOfType<Building>();
			abstractBuildingTile                                         = building.GetComponentInParent<AbstractBuildingTile>();
			abstractBuildingTile.GetComponent<Renderer>().sharedMaterial = material;

			building.GetComponent<BuildingHealth>().OnSoilRepair       += CheckSoilRepair;
			building.GetComponent<BuildingHealth>().OnFoundationRepair += CheckFoundationRepair;
			building.GetComponent<BuildingHealth>().OnBuildingRepair   += CheckBuildingRepair;
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
	}
}