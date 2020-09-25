using Enums;
using Events;
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
		
		private GameObject popup;
		private BuildingData buildingData;

		public void Awake()
		{
			popup = CachedTransform.GetChild(0).Find("btnCrackHouse").gameObject;
			popup.SetActive(false);
		}
		
		// Start is called before the first frame update
		private void Start()
		{
			AddCrackEventListener();
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
			if (Health <= healthBar.StartingHealth / 100 * 25 && !popup.activeInHierarchy)
			{
				print("test");
				popup.SetActive(true);
			}
			
			if (Health <= 0)
			{
				//Monster should eat the house here!!
			}
		}

		public void CalculateBuildingBreakTime()
		{
			Health =  0.0f;
			Health += Switches.SoilTypeSwitch(buildingData.SoilType);
			Health += Switches.FoundationTypeSwitch(buildingData.Foundation);
			//TODO purposes so we dont have to wait a long time
			Health =  25.0f;
		}
		
		private void AddCrackEventListener()
		{
			EventManager.Instance.AddListener<CrackEvent>(OnHouseRepair);
		}

		private void OnHouseRepair(CrackEvent crackEvent)
		{
			//TODO Have to adjust amount
			EventManager.Instance.RaiseEvent<DecreaseMoneyEvent>(new DecreaseMoneyEvent(15));
			popup.SetActive(false);
			Health = healthBar.StartingHealth;
		}
	}
}