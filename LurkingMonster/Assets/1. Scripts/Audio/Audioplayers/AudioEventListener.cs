using System;
using Enums;
using Enums.Audio;
using Events;
using Events.Achievements;
using Events.BuildingEvents;
using Events.BuildingEvents.RepairEvents;
using Events.MoneyManagement;
using FMOD.Studio;
using Interfaces;
using VDFramework;
using VDFramework.EventSystem;

namespace Audio.Audioplayers
{
	public class AudioEventListener : BetterMonoBehaviour, IListener
	{
		private static EventInstance increaseMoney;
		private static EventInstance decreaseMoney;

		private static EventInstance upgrade;
		private static EventInstance buildingConsumed;

		private static EventInstance buildingRepair;
		private static EventInstance foundationRepair;
		private static EventInstance soilRepair;

		private static EventInstance buildingBuilding;
		private static EventInstance buildingFoundation;
		private static EventInstance buildingSoil;

		private static EventInstance meatPowerup;
		private static EventInstance freezePowerup;
		private static EventInstance kcafPowerup;

		private static EventInstance achievementUnlocked;

		private static EventInstance selectPlot;

		private void Awake()
		{
			increaseMoney = AudioPlayer.GetEventInstance(EventType.SFX_IncreaseMoney);
			decreaseMoney = AudioPlayer.GetEventInstance(EventType.SFX_DecreaseMoney);

			upgrade          = AudioPlayer.GetEventInstance(EventType.SFX_BUILDING_Upgrade);
			buildingConsumed = AudioPlayer.GetEventInstance(EventType.SFX_BUILDING_Consumed);

			buildingRepair   = AudioPlayer.GetEventInstance(EventType.SFX_BUILDING_Repair);
			foundationRepair = AudioPlayer.GetEventInstance(EventType.SFX_BUILDING_Repair); // Only 1 sound for repairing atm
			soilRepair       = AudioPlayer.GetEventInstance(EventType.SFX_BUILDING_Repair);

			buildingBuilding   = AudioPlayer.GetEventInstance(EventType.SFX_BUILDING_Building);
			buildingFoundation = AudioPlayer.GetEventInstance(EventType.SFX_BUILDING_Foundation);
			buildingSoil       = AudioPlayer.GetEventInstance(EventType.SFX_BUILDING_Soil);

			meatPowerup   = AudioPlayer.GetEventInstance(EventType.SFX_POWERUP_Meat);
			freezePowerup = AudioPlayer.GetEventInstance(EventType.SFX_POWERUP_FreezeTime);
			kcafPowerup   = AudioPlayer.GetEventInstance(EventType.SFX_POWERUP_KCAF);
			
			achievementUnlocked = AudioPlayer.GetEventInstance(EventType.SFX_ACHIEVEMENT_Unlocked);

			selectPlot = AudioPlayer.GetEventInstance(EventType.SFX_SelectPlot);
		}

		public void AddListeners()
		{
			EventManager.Instance.AddListener<CollectRentEvent>(IncreaseMoneySound);
			EventManager.Instance.AddListener<DecreaseMoneyEvent>(DecreaseMoneySound);

			EventManager.Instance.AddListener<BuildingUpgradeEvent>(UpgradeSound);
			EventManager.Instance.AddListener<BuildingConsumedEvent>(BuildingConsumedSound);

			EventManager.Instance.AddListener<BuildingRepairEvent>(BuildingRepairSound);
			EventManager.Instance.AddListener<FoundationRepairEvent>(FoundationRepairSound);
			EventManager.Instance.AddListener<SoilRepairEvent>(SoilRepairSound);

			EventManager.Instance.AddListener<BuildingBuiltEvent>(BuildingBuildingSound);
			EventManager.Instance.AddListener<FoundationBuildEvent>(BuildingFoundationSound);
			EventManager.Instance.AddListener<BuyPlotEvent>(BuildingSoilSound);

			EventManager.Instance.AddListener<PowerUpActivateEvent>(PowerupSound);

			EventManager.Instance.AddListener<AchievementUnlockedEvent>(AchievementSound);
			
			EventManager.Instance.AddListener<SelectedBuildingEvent>(SelectPlot);
		}

		public void RemoveListeners()
		{
			EventManager.Instance.RemoveListener<CollectRentEvent>(IncreaseMoneySound);
			EventManager.Instance.RemoveListener<DecreaseMoneyEvent>(DecreaseMoneySound);

			EventManager.Instance.RemoveListener<BuildingUpgradeEvent>(UpgradeSound);
			EventManager.Instance.RemoveListener<BuildingConsumedEvent>(BuildingConsumedSound);

			EventManager.Instance.RemoveListener<BuildingRepairEvent>(BuildingRepairSound);
			EventManager.Instance.RemoveListener<FoundationRepairEvent>(FoundationRepairSound);
			EventManager.Instance.RemoveListener<SoilRepairEvent>(SoilRepairSound);

			EventManager.Instance.RemoveListener<BuildingBuiltEvent>(BuildingBuildingSound);
			EventManager.Instance.RemoveListener<FoundationBuildEvent>(BuildingFoundationSound);
			EventManager.Instance.RemoveListener<BuyPlotEvent>(BuildingSoilSound);

			EventManager.Instance.RemoveListener<PowerUpActivateEvent>(PowerupSound);

			EventManager.Instance.RemoveListener<AchievementUnlockedEvent>(AchievementSound);
			
			EventManager.Instance.RemoveListener<SelectedBuildingEvent>(SelectPlot);
		}

		// Currency sounds
		private static void IncreaseMoneySound()
		{
			increaseMoney.start();
		}

		private static void DecreaseMoneySound()
		{
			decreaseMoney.start();
		}

		// Building event sounds
		private static void UpgradeSound()
		{
			upgrade.start();
		}

		private static void BuildingConsumedSound()
		{
			buildingConsumed.start();
		}

		// Repair sounds
		private static void BuildingRepairSound()
		{
			buildingRepair.start();
		}

		private static void FoundationRepairSound()
		{
			foundationRepair.start();
		}

		private static void SoilRepairSound()
		{
			soilRepair.start();
		}

		// Building sounds
		private static void BuildingBuildingSound()
		{
			buildingBuilding.start();
		}

		private static void BuildingFoundationSound()
		{
			buildingFoundation.start();
		}

		private static void BuildingSoilSound()
		{
			buildingSoil.start();
		}

		// Powerups
		private static void PowerupSound(PowerUpActivateEvent powerUpActivateEvent)
		{
			switch (powerUpActivateEvent.Type)
			{
				case PowerUpType.AvoidMonster:
					meatPowerup.start();
					break;
				case PowerUpType.FixProblems:
					kcafPowerup.start();
					break;
				case PowerUpType.AvoidWeatherEvent:
					freezePowerup.start();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(powerUpActivateEvent.Type), "The powerup type is not valid");
			}
		}

		// Achievements
		private static void AchievementSound()
		{
			achievementUnlocked.start();
		}
		
		// Selecting
		private static void SelectPlot(SelectedBuildingEvent selectedBuildingEvent)
		{
			if (selectedBuildingEvent.Tile != null)
			{
				selectPlot.start();
			}
		}

		private void OnDestroy()
		{
			increaseMoney.release();
			decreaseMoney.release();

			upgrade.release();
			buildingConsumed.release();

			buildingRepair.release();
			foundationRepair.release();
			soilRepair.release();

			buildingBuilding.release();
			buildingFoundation.release();
			buildingSoil.release();

			meatPowerup.release();
			freezePowerup.release();
			kcafPowerup.release();

			achievementUnlocked.release();

			selectPlot.release();
		}
	}
}