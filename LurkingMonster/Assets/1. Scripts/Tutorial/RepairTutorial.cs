using System;
using Events;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.EventSystem;

namespace _1._Scripts.Tutorial
{
	public class RepairTutorial : global::Tutorial.Tutorial
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
		
		public override void StartTutorial(GameObject narrator, GameObject arrow)
		{
			base.StartTutorial(narrator, arrow);
			
			building                                                     = FindObjectOfType<Building>();
			abstractBuildingTile                                         = building.GetComponentInParent<AbstractBuildingTile>();
			//abstractBuildingTile.GetComponent<Renderer>().sharedMaterial = material;

			building.GetComponent<BuildingHealth>().OnSoilRepair       += CheckSoilRepair;
			building.GetComponent<BuildingHealth>().OnFoundationRepair += CheckFoundationRepair;
			building.GetComponent<BuildingHealth>().OnBuildingRepair   += CheckBuildingRepair;
		}

		private void CheckIfRepairs()
		{
			if (!repairSoil && checkSoil)
			{
				return;
			}
			
			if (!repairFoundation && checkFoundation)
			{
				return;
			}
			
			if (!repairBuilding && checkBuilding)
			{
				return;
			}
			
			TutorialManager.Instance.CompletedTutorial();
		}
		
		private void CheckSoilRepair()
		{
			repairSoil = true;
			
			CheckIfRepairs();
		}

		private void CheckFoundationRepair()
		{
			repairFoundation = true;
			
			CheckIfRepairs();
		}

		private void CheckBuildingRepair()
		{
			repairBuilding = true;
			
			CheckIfRepairs();
		}
	}
}