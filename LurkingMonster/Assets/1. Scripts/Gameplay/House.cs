using System;
using System.Collections;
using Events;
using Structs;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	public class House : BetterMonoBehaviour
	{
		private HouseData data;
		public float waitTimeUntilRent = 5.0f;
		public float timer = 0.0f;

		public void Instantiate(HouseData houseData)
		{
			data = houseData;
		}

		public void Update()
		{
			timer += Time.deltaTime;

			if (timer > waitTimeUntilRent)
			{
				EventManager.Instance.RaiseEvent(new CollectRentEvent(15));
				timer = 0.0f;
			}
		}
	}
}