using Structs;
using UI;
using UnityEngine;
using VDFramework;

namespace Gameplay
{
	public class Building : BetterMonoBehaviour
	{
		public float WaitTimeUntilRent = 5.0f;
		
		private float timer = 0.0f;
		
		private BuildingData data;
		private GameObject popup;

		public void Instantiate(BuildingData buildingData)
		{
			data = buildingData;

			ButtonCollectRent buttonCollectRent = GetComponentInChildren<ButtonCollectRent>(true);
			buttonCollectRent.Rent = data.Rent;
		}

		public void Awake()
		{
			popup = CachedTransform.GetChild(0).gameObject;
			popup.SetActive(false);
		}

		public void Update()
		{
			// Rent button not active
			if (!popup.activeInHierarchy)
			{
				timer += Time.deltaTime;

				if (timer > WaitTimeUntilRent)
				{
					popup.SetActive(true);
					timer = 0.0f;
				}
			}
		}

		public BuildingData Data
		{
			get => data;
			set => data = value;
		}
	}
}