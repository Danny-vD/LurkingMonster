using Events;
using ScriptableObjects;
using Singletons;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Buildings
{
	public class BuildingBreak : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject crackPopup = null;
		
		public Bar bar;

		private float buildingWeatherFactor;
		private float foundationWeatherFactor;
		private float soilWeatherFactor;
		
		private Building building;
		private BuildingHealth buildingHealth;
		private WeatherEventManager weatherEventManager;

		public void Awake()
		{
			crackPopup.SetActive(false);
			
			building       = GetComponent<Building>();
			buildingHealth = GetComponent<BuildingHealth>();
			
			EventManager.Instance.AddListener<RandomWeatherEvent>(OnWeatherEvent);
		}

		// Start is called before the first frame update
		private void Start()
		{
			bar.SetMax((int) buildingHealth.MaxTotalHealth);
		}

		// Update is called once per frame
		private void Update()
		{
			float damage = Time.deltaTime; // Use 1 external call instead of 3.
			
			if (PowerUpManager.Instance.AvoidWeatherActive || !WeatherEventActive())
			{
				buildingHealth.DamageSoil(damage);
				buildingHealth.DamageFoundation(damage);
				buildingHealth.DamageBuilding(damage);
			}
			else
			{
				buildingHealth.DamageBuilding(damage * (buildingWeatherFactor / 100 + 1));
				buildingHealth.DamageFoundation(damage * (foundationWeatherFactor / 100 + 1));
				buildingHealth.DamageSoil(damage * (soilWeatherFactor / 100 + 1));
			}
			
			bar.SetValue((int) buildingHealth.TotalHealth);

			//TODO: make 3 seperate popups instead?
			//When health is less then 25% show cracks
			if (buildingHealth.TotalHealth <= bar.MaxValue / 100 * 25 && !crackPopup.activeInHierarchy)
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

		private bool WeatherEventActive()
		{
			if (weatherEventManager && weatherEventManager.WeatherEventActive)
			{
				return true;
			}

			return false;
		}

		public void OnWeatherEvent(RandomWeatherEvent randomWeatherEvent)
		{
			WeatherEventData data = randomWeatherEvent.weatherEventManager.WeatherEventData;
			
			buildingWeatherFactor   = data.BuildingTime;
			foundationWeatherFactor = data.FoundationTime;
			soilWeatherFactor       = data.SoilTime;
			
			weatherEventManager = randomWeatherEvent.weatherEventManager;
		}

		public void CrackedPopupClicked()
		{
			crackPopup.SetActive(false);
			
			if (!PowerUpManager.Instance.FixProblemsActive)
			{
				buildingHealth.ResetHealth();
				return;
			}

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