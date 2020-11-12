using System;
using System.Collections.Generic;
using Enums;
using Events;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Buttons
{
	public class Inventory : BetterMonoBehaviour
	{
		[SerializeField]
		private Button inventory;

		[SerializeField]
		private Image background;

		[SerializeField]
		private Image boxImage;
		
		[SerializeField]
		private Button meat;
		
		[SerializeField]
		private Button time;
		
		[SerializeField]
		private Button kcaf;

		[SerializeField, Header("0: Disabled, 1: Enabled")]
		private Sprite[] inventorySprite = new Sprite[0];
		
		[SerializeField]
		private Sprite[] meatSprite = new Sprite[0];
		
		[SerializeField]
		private Sprite[] timeSprite = new Sprite[0];
		
		[SerializeField]
		private Sprite[] kcafSprite = new Sprite[0];

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
			OpenPopup(meat.transform, PowerUpManager.Instance.AvoidMonsters, PowerUpType.AvoidMonster);
		}

		private void OpenTimePopUp()
		{
			OpenPopup(time.transform, PowerUpManager.Instance.AvoidWeather, PowerUpType.AvoidWeatherEvent);
		}
		
		private void OpenKcafPopUp()
		{
			OpenPopup(kcaf.transform, PowerUpManager.Instance.FixProblems, PowerUpType.FixProblems);
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
			
			Transform popup = transform.GetChild(0);

			lastPopup = popup;
			
			popup.gameObject.SetActive(true);
			Button activate = popup.GetComponentInChildren<Button>();
			TextMeshProUGUI textCounter = popup.Find("Text_counter").GetComponentInChildren<TextMeshProUGUI>();
			textCounter.text = counter.ToString();
			
			activate.onClick.AddListener(Activate);

			void Activate()
			{
				ActivatePowerUp(transform, type);
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
			if (isActive)
			{
				boxImage.sprite = inventorySprite[0];
			}
			else
			{
				EnablePowerUps();
				boxImage.sprite = inventorySprite[1];
			}
			
			isActive = !isActive;
			background.gameObject.SetActive(isActive);
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