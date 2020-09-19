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
		public float WaitTimeUntilRent = 5.0f;
		public float Timer = 0.0f;

		private HouseData data;
		private GameObject popup;

		public void Instantiate(HouseData houseData)
		{
			data                                             = houseData;
			ButtonCollectRent buttonCollectRent = transform.Find("Canvas").GetComponentInChildren<ButtonCollectRent>();
			buttonCollectRent.Rent = data.Rent;
		}

		public void Awake()
		{
			popup = CachedTransform.GetChild(0).gameObject;
			popup.SetActive(false);
		}

		public void Update()
		{
			//Rent button not active
			if (!popup.activeInHierarchy)
			{
				Timer += Time.deltaTime;

				if (Timer > WaitTimeUntilRent)
				{
					popup.SetActive(true);
					Timer = 0.0f;
				}
			}
		}

		public HouseData Data
		{
			get => data;
			set => data = value;
		}
	}
}