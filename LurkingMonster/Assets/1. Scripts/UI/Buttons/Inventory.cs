using System;
using Enums;
using Events;
using Singletons;
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
		private Button meat;
		
		[SerializeField]
		private Button time;
		
		[SerializeField]
		private Button kcaf;

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
			OpenPopup(meat.transform, ActivateMeatPowerUp);
		}

		private void OpenTimePopUp()
		{
			OpenPopup(time.transform, ActivateTimePowerUp);
		}
		
		private void OpenKcafPopUp()
		{
			OpenPopup(kcaf.transform, ActivateKcafPowerUp);
		}
		
		private void OpenPopup(Transform transform, UnityAction activateAction)
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
			
			activate.onClick.AddListener(activateAction);
		}
		
		private void ActivateMeatPowerUp()
		{
			meat.transform.GetChild(0).gameObject.SetActive(false);

			PowerUpManager.Instance.ActivatePowerUp(PowerUpType.AvoidMonster);
			
			ToggleInventory();
			EnablePowerUps();
		}
		
		private void ActivateTimePowerUp()
		{
			time.transform.GetChild(0).gameObject.SetActive(false);

			PowerUpManager.Instance.ActivatePowerUp(PowerUpType.AvoidWeatherEvent);
			
			ToggleInventory();
			EnablePowerUps();
		}
		
		private void ActivateKcafPowerUp()
		{
			kcaf.transform.GetChild(0).gameObject.SetActive(false);

			PowerUpManager.Instance.ActivatePowerUp(PowerUpType.FixProblems);
			
			ToggleInventory();
			EnablePowerUps();
		}

		private void ToggleInventory()
		{
			if (isActive)
			{
				isActive = false;
				background.gameObject.SetActive(isActive);
			}
			else
			{
				EnablePowerUps();
				isActive = true;
				background.gameObject.SetActive(isActive);
			}
		}

		private void EnablePowerUps()
		{
			if (PowerUpManager.Instance.AvoidMonsters > 0)
			{
				meat.image.sprite = meatSprite[1];
				meat.interactable = true;
			}
			else
			{
				meat.image.sprite = meatSprite[0];
				meat.interactable = false;
			}
			
			if (PowerUpManager.Instance.AvoidWeather > 0)
			{
				time.image.sprite = timeSprite[1];
				time.interactable = true;
			}
			else
			{
				time.image.sprite = timeSprite[0];
				time.interactable = false;
			}
			
			if (PowerUpManager.Instance.FixProblems > 0)
			{
				kcaf.image.sprite = kcafSprite[1];
				kcaf.interactable = true;
			}
			else
			{
				kcaf.image.sprite = kcafSprite[0];
				kcaf.interactable = false;
			}
		}
	}
}