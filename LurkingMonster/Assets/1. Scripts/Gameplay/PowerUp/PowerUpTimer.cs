using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

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

		/// <summary>
		/// Function that starts the power up timer
		/// </summary>
		public void StartTimer(float timer, Action powerUpActive, PowerUpType powerUpType)
		{
			transform.parent.gameObject.SetActive(true);
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
			transform.parent.gameObject.SetActive(false);
		}

		public float Timer
		{
			get => timer;
			set => timer = value;
		}
	}
}