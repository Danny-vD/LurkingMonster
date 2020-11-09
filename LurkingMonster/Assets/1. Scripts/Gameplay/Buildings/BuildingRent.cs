using UI.Buttons;
using UI.Buttons.Gameplay;
using UnityEngine;
using VDFramework;

namespace Gameplay.Buildings
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