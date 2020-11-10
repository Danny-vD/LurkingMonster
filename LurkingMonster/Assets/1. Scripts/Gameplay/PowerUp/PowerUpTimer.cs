using System;
using Enums;
using Events;
using Singletons;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	public class PowerUpTimer : BetterMonoBehaviour
	{
		[SerializeField]
		private Sprite meatSprite;

		[SerializeField]
		private Sprite weatherSprite;

		[SerializeField]
		private Sprite KcafSprite;

		[SerializeField]
		private Image icon;

		private Image circleTimer;

		private float maxTimer;
		private float timer;
		private Action powerUpActive;

		private void Awake()
		{
			circleTimer = GetComponent<Image>();
		}

		public void StartTimer(float timer, Action powerUpActive, PowerUpType powerUpType)
		{
			print(gameObject);
			gameObject.SetActive(true);
			circleTimer.fillAmount = 1;
			maxTimer               = timer;
			this.timer             = maxTimer;
			this.powerUpActive     = powerUpActive;

			switch (powerUpType)
			{
				case PowerUpType.AvoidMonster:
					icon.sprite = meatSprite;
					break;
				case PowerUpType.FixProblems:
					icon.sprite = KcafSprite;
					break;
				case PowerUpType.AvoidWeatherEvent:
					icon.sprite = weatherSprite;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(powerUpType), powerUpType, null);
			}
		}

		private void Update()
		{
			timer -= Time.deltaTime;

			circleTimer.fillAmount = Mathf.InverseLerp(0, maxTimer, timer);

			if (timer >= 0)
			{
				return;
			}
			
			timer = 0;
			powerUpActive.Invoke();
			gameObject.SetActive(false);
		}
	}
}