using System;
using Events;
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

		//Weather event variables
		private float weatherEventTimeLength;
		private float timerWeatherEvent;
		private bool weatherEvent;
		
		public Bar bar;

		private Building building;
		
		private GameObject crackPopup;

		public void Awake()
		{
			crackPopup = CachedTransform.GetChild(0).Find("btnCrackHouse").gameObject;
			crackPopup.SetActive(false);
			weatherEvent = false;
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
			if (weatherEvent)
			{
				timerWeatherEvent += Time.deltaTime;
				
				if (weatherEventTimeLength <= timerWeatherEvent)
				{
					SpeedPercentage   = 0;
					weatherEvent      = false;
					timerWeatherEvent = 0;
				}
			}
			
			TotalHealth -= Time.deltaTime * (SpeedPercentage / 100 + 1); 
			
			bar.SetValue((int) TotalHealth);
			
			//When health is less then 25% show cracks
			if (TotalHealth <= bar.maxValue / 100 * 25 && !crackPopup.activeInHierarchy)
			{
				crackPopup.SetActive(true);
			}
			
			if (TotalHealth <= 0)
			{
				building.RemoveBuilding(false); // TODO: Should spawn a 'destroyed building' asset instead
				EventManager.Instance.RaiseEvent(new BuildingConsumedEvent());
				VibrationUtil.Vibrate();
			}
		}

		public void OnWeatherEvent(RandomWeatherEvent randomWeatherEvent)
		{
			SpeedPercentage        += randomWeatherEvent.WeatherEventData.BuildingTime;
			SpeedPercentage        += randomWeatherEvent.WeatherEventData.SoilTime;
			weatherEventTimeLength =  randomWeatherEvent.WeatherEventData.Timer;
			weatherEvent           =  true;
		}

		public void CalculateBuildingBreakTime()
		{
			TotalHealth =  0.0f;
			SoilHealth += Switches.SoilTypeSwitch(building.Data.SoilType);
			FoundationHealth += Switches.FoundationTypeSwitch(building.Data.Foundation);
			
			//TODO for test purposes so we dont have to wait a long time
			TotalHealth = (SoilHealth + FoundationHealth);
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