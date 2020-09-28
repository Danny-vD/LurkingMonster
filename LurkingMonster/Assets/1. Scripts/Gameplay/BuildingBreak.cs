using Enums;
using Events;
using Singletons;
using Structs;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	public class BuildingBreak : BetterMonoBehaviour
	{
		public float Health;
		public float StartingHealth;
		public HealthBar healthBar;
		
		private GameObject crackPopup;
		private BuildingData buildingData;

		public void Awake()
		{
			crackPopup = CachedTransform.GetChild(0).Find("btnCrackHouse").gameObject;
			crackPopup.SetActive(false);
		}
		
		// Start is called before the first frame update
		private void Start()
		{
			Building building = GetComponent<Building>();
			buildingData           =  building.Data;
			CalculateBuildingBreakTime();
			healthBar.SetMaxHealth((int) Health);
		}

		// Update is called once per frame
		private void Update()
		{
			Health -= Time.deltaTime;
			
			healthBar.SetHealth((int) Health);
			
			//When health is less then 25% show cracks
			if (Health <= healthBar.StartingHealth / 100 * 25 && !crackPopup.activeInHierarchy)
			{
				print("test");
				crackPopup.SetActive(true);
			}
			
			if (Health <= 0)
			{
				print("Destroy");
				Destroy(gameObject);
			}
		}

		public void CalculateBuildingBreakTime()
		{
			Health =  0.0f;
			Health += Switches.SoilTypeSwitch(buildingData.SoilType);
			Health += Switches.FoundationTypeSwitch(buildingData.Foundation);
			//TODO for test purposes so we dont have to wait a long time
			Health =  25.0f;
		}

		public void OnHouseRepair()
		{
			//TODO Have to adjust amount
			if (MoneyManager.Instance.PlayerHasEnoughMoney(15))
			{
				EventManager.Instance.RaiseEvent<DecreaseMoneyEvent>(new DecreaseMoneyEvent(15));
				
				crackPopup.SetActive(false);
				Health = healthBar.StartingHealth;
			}
			else
			{
				MassageManager.Instance.ShowMessage("Not enough money!", Color.red);
			}
		}
	}
}