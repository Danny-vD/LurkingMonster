using System.Collections.Generic;
using Enums;
using Singletons;
using TMPro;
using Tutorials;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

// ReSharper disable MissingLinebreak

namespace UI.Buttons
{
	public class Inventory : BetterMonoBehaviour
	{
		[SerializeField] private Transform popupMonster;
		[SerializeField] private Transform popupTime;
		[SerializeField] private Transform popupKcaf;
		
		[SerializeField] private Button inventory;
		
		[SerializeField] private Image background;
		[SerializeField] private Image boxImage;
		
		[SerializeField] private Button meat;
		[SerializeField] private Button time;
		[SerializeField] private Button kcaf;
		
		[SerializeField, Header("0: Disabled, 1: Enabled")] private Sprite[] inventorySprite = new Sprite[0];
		[SerializeField] private Sprite[] meatSprite = new Sprite[0];
		[SerializeField] private Sprite[] timeSprite = new Sprite[0];
		[SerializeField] private Sprite[] kcafSprite = new Sprite[0];
	

		private Transform lastPopup;
		
		private bool isActive;
		
		private void Start()
		{
			isActive = false;
			
			inventory.onClick.AddListener(ToggleInventory);
			meat.onClick.AddListener(OpenMeatPopUp);
			time.onClick.AddListener(OpenTimePopUp);
			kcaf.onClick.AddListener(OpenKcafPopUp);
		}

		private void OpenMeatPopUp()
		{
			OpenPopup(popupMonster, PowerUpManager.Instance.AvoidMonsters, PowerUpType.AvoidMonster);
		}

		private void OpenTimePopUp()
		{
			OpenPopup(popupTime, PowerUpManager.Instance.AvoidWeather, PowerUpType.AvoidWeatherEvent);
		}
		
		private void OpenKcafPopUp()
		{
			OpenPopup(popupKcaf, PowerUpManager.Instance.FixProblems, PowerUpType.FixProblems);
		}
		
		private void OpenPopup(Transform transform, int counter, PowerUpType type)
		{
			if (lastPopup)
			{
				lastPopup.gameObject.SetActive(false);
			}
			
			if (PowerUpManager.Instance.CheckIfAnPowerUpIsActive())
			{
				return;
			}

			lastPopup = transform;
			
			transform.gameObject.SetActive(true);
			Button activate = transform.GetComponentInChildren<Button>();
			TextMeshProUGUI textCounter = transform.Find("Text_counter").GetComponent<TextMeshProUGUI>();
			textCounter.text = counter.ToString();

			activate.onClick.AddListener(Activate);

			void Activate()
			{
				ActivatePowerUp(transform, type);
				transform.gameObject.SetActive(false);
			}
		}

		private void ActivatePowerUp(Transform btn, PowerUpType type)
		{
			btn.GetChild(0).gameObject.SetActive(false);

			PowerUpManager.Instance.ActivatePowerUp(type);
			ToggleInventory();
			EnablePowerUps();
		}
		
		private void ToggleInventory()
		{
			if (TutorialManager.IsInitialized && TutorialManager.Instance.IsActive)
			{
				return;
			}
			
			if (isActive)
			{
				boxImage.sprite = inventorySprite[0];
				DisablePopups();
			}
			else
			{
				EnablePowerUps();
				boxImage.sprite = inventorySprite[1];
			}
			
			isActive = !isActive;
			background.gameObject.SetActive(isActive);
		}

		private void DisablePopups()
		{
			popupKcaf.gameObject.SetActive(false);
			popupMonster.gameObject.SetActive(false);
			popupTime.gameObject.SetActive(false);
		}

		private void EnablePowerUps()
		{
			ChangeSpritesPowerUps(PowerUpManager.Instance.AvoidMonsters, meat, meatSprite);
			ChangeSpritesPowerUps(PowerUpManager.Instance.AvoidWeather, time, timeSprite);
			ChangeSpritesPowerUps(PowerUpManager.Instance.FixProblems, kcaf, kcafSprite);
		}

		private static void ChangeSpritesPowerUps(int powerUpCounter, Selectable btn, IReadOnlyList<Sprite> sprites)
		{
			if (powerUpCounter > 0)
			{
				btn.image.sprite  = sprites[1];
				btn.interactable  = true;
			}
			else
			{
				btn.image.sprite = sprites[0];
				btn.interactable = false;
			}
		}
	}
}