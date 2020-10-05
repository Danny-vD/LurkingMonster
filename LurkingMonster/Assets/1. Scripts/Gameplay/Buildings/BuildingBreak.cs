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
		public float Health;
		public float StartingHealth;
		public HealthBar healthBar;

		private Building building;
		
		private GameObject crackPopup;

		public void Awake()
		{
			crackPopup = CachedTransform.GetChild(0).Find("btnCrackHouse").gameObject;
			crackPopup.SetActive(false);
		}
		
		// Start is called before the first frame update
		private void Start()
		{
			building = GetComponent<Building>();
			
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
				crackPopup.SetActive(true);
			}
			
			if (Health <= 0)
			{
				building.RemoveBuilding();
				EventManager.Instance.RaiseEvent(new BuildingConsumedEvent());
			}
		}

		public void CalculateBuildingBreakTime()
		{
			Health =  0.0f;
			Health += Switches.SoilTypeSwitch(building.Data.SoilType);
			Health += Switches.FoundationTypeSwitch(building.Data.Foundation);
			//TODO for test purposes so we dont have to wait a long time
			Health =  25.0f;
		}

		public void OnHouseRepair()
		{
			//TODO Have to adjust amount
			if (MoneyManager.Instance.PlayerHasEnoughMoney(15))
			{
				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(15));
				
				crackPopup.SetActive(false);
				Health = healthBar.StartingHealth;
				EventManager.Instance.RaiseEvent(new BuildingSavedEvent());
			}
			else
			{
				MassageManager.Instance.ShowMessageGameUI("Not enough money!", Color.red);
			}
		}
	}
}