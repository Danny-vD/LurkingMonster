using System;
using System.Collections;
using Events;
using Structs;
using UI;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	public class House : BetterMonoBehaviour
	{
		public float waitTimeUntilRent = 5.0f;
		public float timer = 0.0f;

		private HouseData data;
		private GameObject popup;
		
		public void Instantiate(HouseData houseData)
		{
			data = houseData;
			GetComponentInChildren<ButtonCollectRent>().Rent = data.Rent;
		}

		public void Awake()
		{
			popup = CachedTransform.GetChild(0).gameObject;
			popup.SetActive(false);
		}

		public void Update()
		{
			if (!popup.activeInHierarchy)
			{
				timer += Time.deltaTime;

				if (timer > waitTimeUntilRent)
				{
					popup.SetActive(true);
					timer = 0.0f;
				}
			}
		}
	}
}