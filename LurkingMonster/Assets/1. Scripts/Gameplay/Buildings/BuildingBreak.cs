using System;
using Enums;
using Events;
using Grid;
using Singletons;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Buildings
{
	public class BuildingBreak : BetterMonoBehaviour
	{
		public Bar bar;

		private float buildingHealth;
		private float soilHealth;
		private float foundationHealth;

		private float buildingWeatherFactor;
		private float foundationWeatherFactor;
		private float soilWeatherFactor;

		//Weather event variables
		private float weatherEventTimeLength;
		private float timerWeatherEvent;
		private bool weatherEvent;

		private Building building;

		private GameObject crackPopup;

		public void Awake()
		{
			crackPopup = CachedTransform.GetChild(0).Find("btnCrackHouse").gameObject;
			crackPopup.SetActive(false);
			weatherEvent = false;
			building     = GetComponent<Building>();
		}

		// Start is called before the first frame update
		private void Start()
		{
			if (!UserSettings.SettingsExist)
			{
				ResetHealth();
			}
			
			float maxTotalHealth = GetMaximumFoundationHealth() + GetMaximumSoilHealth() + GetMaxBuildingHealth();
			
			bar.SetMax((int) maxTotalHealth);
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
					buildingWeatherFactor   = 0;
					foundationWeatherFactor = 0;
					soilWeatherFactor       = 0;
					weatherEvent            = false;
					timerWeatherEvent       = 0;
				}
			}

			if (PowerUpManager.Instance.AvoidWeatherActive)
			{
				buildingHealth   -= Time.deltaTime;
				foundationHealth -= Time.deltaTime;
				soilHealth       -= Time.deltaTime;
			}
			else
			{
				//TODO CHANGE STUFF
				buildingHealth   -= Time.deltaTime * (buildingWeatherFactor / 100 + 15);
				foundationHealth -= Time.deltaTime * (foundationWeatherFactor / 100 + 15);
				soilHealth       -= Time.deltaTime * (soilWeatherFactor / 100 + 15);
			}

			bar.SetValue((int) TotalHealth);

			//When health is less then 25% show cracks
			if (TotalHealth <= bar.maxValue / 100 * 25 && !crackPopup.activeInHierarchy)
			{
				crackPopup.SetActive(true);
			}

			if (TotalHealth <= 0 && !PowerUpManager.Instance.AvoidMonsterFeedActive)
			{
				building.RemoveBuilding(false);
				EventManager.Instance.RaiseEvent(new BuildingConsumedEvent(building));
				VibrationUtil.Vibrate();
			}
		}

		private void ResetHealth()
		{
			buildingHealth   = GetMaxBuildingHealth();
			soilHealth       = GetMaximumSoilHealth();
			foundationHealth = GetMaximumFoundationHealth();
		}

		public void OnWeatherEvent(RandomWeatherEvent randomWeatherEvent)
		{
			buildingWeatherFactor   += randomWeatherEvent.WeatherEventData.BuildingTime;
			foundationWeatherFactor += randomWeatherEvent.WeatherEventData.FoundationTime;
			soilWeatherFactor       += randomWeatherEvent.WeatherEventData.SoilTime;
			weatherEventTimeLength  =  randomWeatherEvent.WeatherEventData.Timer;
			weatherEvent            =  true;
		}

		public float GetMaxBuildingHealth()
		{
			return building.Data.MaxHealth;
		}

		public void CrackedPopupClicked()
		{
			//TODO remove
			ResetHealth();
			crackPopup.SetActive(false);
			
			EventManager.Instance.RaiseEvent(new OpenMarketEvent(building));
		}

		public float GetMaximumFoundationHealth()
		{
			return Switches.FoundationTypeSwitch(building.Data.Foundation);
		}

		public float GetMaximumSoilHealth()
		{
			return Switches.SoilTypeSwitch(building.Data.SoilType);
		}

		public float BuildingHealth
		{
			get => buildingHealth;
			set => buildingHealth = value;
		}

		public float SoilHealth
		{
			get => soilHealth;
			set => soilHealth = value;
		}

		public float FoundationHealth
		{
			get => foundationHealth;
			set => foundationHealth = value;
		}

		public float TotalHealth => buildingHealth + soilHealth + foundationHealth;

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			EventManager.Instance.RemoveListener<RandomWeatherEvent>(OnWeatherEvent);
		}
	}
}