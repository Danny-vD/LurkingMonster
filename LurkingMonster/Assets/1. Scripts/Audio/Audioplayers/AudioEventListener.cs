using System;
using Enums;
using Enums.Audio;
using Events;
using Events.Achievements;
using Events.BuildingEvents;
using Events.BuildingEvents.RepairEvents;
using Events.MoneyManagement;
using Interfaces;
using VDFramework;
using VDFramework.EventSystem;

namespace Audio.Audioplayers
{
	public class AudioEventListener : BetterMonoBehaviour, IListener
	{
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
			
			EventManager.Instance.AddListener<SelectedBuildingTileEvent>(SelectPlot);
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
			
			EventManager.Instance.RemoveListener<SelectedBuildingTileEvent>(SelectPlot);
		}

		// Currency sounds
		private static void IncreaseMoneySound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_IncreaseMoney);
		}

		private static void DecreaseMoneySound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_DecreaseMoney);
		}

		// Building event sounds
		private static void UpgradeSound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_BUILDING_Upgrade);
		}

		private static void BuildingConsumedSound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_BUILDING_Consumed);
		}

		// Repair sounds
		private static void BuildingRepairSound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_BUILDING_Repair);
		}

		private static void FoundationRepairSound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_BUILDING_Repair);
		}

		private static void SoilRepairSound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_BUILDING_Repair);
		}

		// Building sounds
		private static void BuildingBuildingSound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_BUILDING_Building);
		}

		private static void BuildingFoundationSound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_BUILDING_Foundation);
		}

		private static void BuildingSoilSound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_BUILDING_Soil);
		}

		// Powerups
		private static void PowerupSound(PowerUpActivateEvent powerUpActivateEvent)
		{
			switch (powerUpActivateEvent.Type)
			{
				case PowerUpType.AvoidMonster:
					AudioPlayer.PlayOneShot2D(EventType.SFX_POWERUP_Meat);
					break;
				case PowerUpType.FixProblems:
					AudioPlayer.PlayOneShot2D(EventType.SFX_POWERUP_KCAF);
					break;
				case PowerUpType.AvoidWeatherEvent:
					AudioPlayer.PlayOneShot2D(EventType.SFX_POWERUP_FreezeTime);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(powerUpActivateEvent.Type), "The powerup type is not valid");
			}
		}

		// Achievements
		private static void AchievementSound()
		{
			AudioPlayer.PlayOneShot2D(EventType.SFX_ACHIEVEMENT_Unlocked);
		}
		
		// Selecting
		private static void SelectPlot(SelectedBuildingTileEvent selectedBuildingEvent)
		{
			if (selectedBuildingEvent.SelectedBuildingTile)
			{
				AudioPlayer.PlayOneShot2D(EventType.SFX_SelectPlot);
			}
		}
	}
}