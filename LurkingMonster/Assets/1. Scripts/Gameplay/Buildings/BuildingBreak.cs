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
		public Bar bar;

		private float buildingWeatherFactor;
		private float foundationWeatherFactor;
		private float soilWeatherFactor;

		//Weather event variables
		private float weatherEventTimeLength;
		private float timerWeatherEvent;
		private bool weatherEvent;

		private Building building;
		private BuildingHealth buildingHealth;

		private GameObject crackPopup;

		public void Awake()
		{
			crackPopup = CachedTransform.GetChild(0).Find("btnCrackHouse").gameObject;
			crackPopup.SetActive(false);

			weatherEvent = false;

			building       = GetComponent<Building>();
			buildingHealth = GetComponent<BuildingHealth>();
		}

		// Start is called before the first frame update
		private void Start()
		{
			bar.SetMax((int) buildingHealth.MaxTotalHealth);
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

			float damage = Time.deltaTime; // Use 1 external call instead of 3.

			if (PowerUpManager.Instance.AvoidWeatherActive)
			{
				buildingHealth.DamageSoil(damage);
				buildingHealth.DamageFoundation(damage);
				buildingHealth.DamageBuilding(damage);
			}
			else
			{
				buildingHealth.DamageBuilding(damage * (buildingWeatherFactor / 100 + 15));
				buildingHealth.DamageFoundation(damage * (foundationWeatherFactor / 100 + 15));
				buildingHealth.DamageSoil(damage * (soilWeatherFactor / 100 + 15));
			}

			bar.SetValue((int) buildingHealth.TotalHealth);

			//TODO: make 3 seperate popups instead?
			//When health is less then 25% show cracks
			if (buildingHealth.TotalHealth <= bar.maxValue / 100 * 25 && !crackPopup.activeInHierarchy)
			{
				crackPopup.SetActive(true);
			}

			if (buildingHealth.TotalHealth <= 0 && !PowerUpManager.Instance.AvoidMonsterFeedActive)
			{
				building.RemoveBuilding(false);
				EventManager.Instance.RaiseEvent(new BuildingConsumedEvent(building));
				VibrationUtil.Vibrate();
			}
		}

		public void OnWeatherEvent(RandomWeatherEvent randomWeatherEvent)
		{
			buildingWeatherFactor   += randomWeatherEvent.WeatherEventData.BuildingTime;
			foundationWeatherFactor += randomWeatherEvent.WeatherEventData.FoundationTime;
			soilWeatherFactor       += randomWeatherEvent.WeatherEventData.SoilTime;

			weatherEventTimeLength = randomWeatherEvent.WeatherEventData.Timer;
			weatherEvent           = true;
		}

		public void CrackedPopupClicked()
		{
			//TODO: change so that it does not immediately repair the house (has to be fixed through market)
			crackPopup.SetActive(false);

			EventManager.Instance.RaiseEvent(new OpenMarketEvent(building));
		}

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