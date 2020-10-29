using Events;
using Singletons;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Buildings
{
	public class BuildingBreak : BetterMonoBehaviour // TODO: add a getCurrentHealth and GetMaxHealth for Foundation and Building
	{
		[SerializeField]
		private GameObject brokenBuildingPrefab;
		
		public Bar bar;

		public float SoilHealth;
		public float FoundationHealth;
		public float TotalHealth;
		public float StartingHealth;
		public float SpeedPercentage;

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
				Instantiate(brokenBuildingPrefab, CachedTransform.position, CachedTransform.rotation);
				EventManager.Instance.RaiseEvent(new BuildingConsumedEvent());
				VibrationUtil.Vibrate();
			}
		}

		public void CalculateBuildingBreakTime()
		{
			TotalHealth      =  0.0f;
			SoilHealth       += GetMaximumSoilHealth();
			FoundationHealth += GetMaximumFoundationHealth();
			
			//TODO for test purposes so we dont have to wait a long time
			TotalHealth = (SoilHealth + FoundationHealth) / 100;
		}

		public float GetCurrentFoundationHealth()
		{
			return FoundationHealth;
		}

		public float GetMaximumFoundationHealth()
		{
			return Switches.FoundationTypeSwitch(building.Data.Foundation);
		}

		public float GetCurrentSoilHealth()
		{
			return SoilHealth;
		}

		public float GetMaximumSoilHealth()
		{
			return Switches.SoilTypeSwitch(building.Data.SoilType);
		}

		public void OnWeatherEvent(RandomWeatherEvent randomWeatherEvent)
		{
			SpeedPercentage        += randomWeatherEvent.WeatherEventData.BuildingTime;
			SpeedPercentage        += randomWeatherEvent.WeatherEventData.SoilTime;
			weatherEventTimeLength =  randomWeatherEvent.WeatherEventData.Timer;
			weatherEvent           =  true;
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