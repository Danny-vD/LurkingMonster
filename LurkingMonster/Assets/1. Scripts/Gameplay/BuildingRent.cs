using UI.Buttons;
using UnityEngine;
using VDFramework;

namespace Gameplay
{
	[RequireComponent(typeof(Building))]
	public class BuildingRent : BetterMonoBehaviour
	{
		[SerializeField]
		private float waitTimeUntilRent = 5.0f;
		
		private float timer = 0.0f;
		private GameObject rentPopup;

		public void Start()
		{
			ButtonCollectRent buttonCollectRent = GetComponentInChildren<ButtonCollectRent>(true);
			buttonCollectRent.Rent = GetComponent<Building>().Data.Rent;
			
			rentPopup = buttonCollectRent.gameObject;
			rentPopup.SetActive(false);
		}

		public void Update()
		{
			// Rent button not active
			if (!rentPopup.activeInHierarchy)
			{
				timer += Time.deltaTime;

				if (timer > waitTimeUntilRent)
				{
					rentPopup.SetActive(true);
					timer = 0.0f;
				}
			}
		}
	}
}