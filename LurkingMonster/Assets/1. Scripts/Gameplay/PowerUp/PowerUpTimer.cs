using System;
using Enums;
using Events;
using Singletons;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	public class PowerUpTimer : BetterMonoBehaviour
	{
		private float timer;
		private Action powerUpActive;

		public void Initiate(float timer, Action powerUpActive)
		{
			this.timer         = timer;
			this.powerUpActive = powerUpActive;
		}

		private void Update()
		{
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				timer         = 0;
				powerUpActive.Invoke();
				Destroy(gameObject);
			}
		}
	}
}