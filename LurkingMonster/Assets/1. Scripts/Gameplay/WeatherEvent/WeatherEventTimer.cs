using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework;

namespace _1._Scripts.Gameplay.WeatherEvent
{
	public class WeatherEventTimer : BetterMonoBehaviour
	{
		[SerializeField]
		private SerializableEnumDictionary<WeatherEventType, Sprite> weatherSprites;

		[SerializeField]
		private Image icon;

		private Image circleTimer;

		private float maxTimer;
		private float timer;

		private Action timerEnd;

		
		private void Awake()
		{
			circleTimer = GetComponent<Image>();
		}

		public void StartTimer(float timer, Action timerEnd, WeatherEventType weatherEventType)
		{
			gameObject.SetActive(true);
			circleTimer.fillAmount = 1;

			maxTimer      = timer;
			this.timer    = maxTimer;
			this.timerEnd = timerEnd;

			icon.sprite = weatherSprites[weatherEventType];
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
			timerEnd.Invoke();
			gameObject.SetActive(false);
		}

		public float Timer
		{
			get => timer;
			set => timer = value;
		}
	}
}