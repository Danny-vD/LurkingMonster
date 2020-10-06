﻿using Events;
using Singletons;
using UnityEngine;
using UnityEngine.Serialization;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Buildings
{
	public class BuildingBreak : BetterMonoBehaviour
	{
		public float SoilHealth;
		public float FoundationHealth;
		public float TotalHealth;
		public float StartingHealth;
		public float SpeedPercentage;
		
		public Bar bar;

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
			bar.SetMax((int) TotalHealth);
			EventManager.Instance.AddListener<RandomWeatherEvent>(OnWeatherEvent);
		}

		// Update is called once per frame
		private void Update()
		{
			//TODO reset speedpercentage when weather event is done
			TotalHealth -= Time.deltaTime * (SpeedPercentage / 100 + 1); 
			
			bar.SetValue((int) TotalHealth);
			
			//When health is less then 25% show cracks
			if (TotalHealth <= bar.maxValue / 100 * 25 && !crackPopup.activeInHierarchy)
			{
				crackPopup.SetActive(true);
				print("Health below 25%");
			}
			
			if (TotalHealth <= 0)
			{
				building.RemoveBuilding();
				EventManager.Instance.RaiseEvent(new BuildingConsumedEvent());
			}
		}

		public void OnWeatherEvent(RandomWeatherEvent randomWeatherEvent)
		{
			SpeedPercentage += randomWeatherEvent.WeatherEventData.BuildingTime;
			SpeedPercentage += randomWeatherEvent.WeatherEventData.SoilTime;
			
			print(SpeedPercentage);
		}

		public void CalculateBuildingBreakTime()
		{
			TotalHealth =  0.0f;
			SoilHealth += Switches.SoilTypeSwitch(building.Data.SoilType);
			FoundationHealth += Switches.FoundationTypeSwitch(building.Data.Foundation);
			
			//TODO for test purposes so we dont have to wait a long time
			TotalHealth = (SoilHealth + FoundationHealth) / 20;
		}

		public void OnHouseRepair()
		{
			//TODO Have to adjust amount
			if (MoneyManager.Instance.PlayerHasEnoughMoney(15))
			{
				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(15));
				
				crackPopup.SetActive(false);
				TotalHealth = bar.maxValue;
				EventManager.Instance.RaiseEvent(new BuildingSavedEvent());
			}
			else
			{
				MessageManager.Instance.ShowMessageGameUI("Not enough money!", Color.red);
			}
		}
	}
}