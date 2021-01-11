using Events.MoneyManagement;
using UI.TextLabels;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Singletons
{
	public class RentPopupManager : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject rentPopupPrefab;

		[SerializeField]
		private Transform rentPopupParent;

		private void Start()
		{
			EventManager.Instance.AddListener<CollectRentEvent>(DisplayRentPopup);
		}

		private void DisplayRentPopup(CollectRentEvent rent)
		{
			GameObject rentPopup = Instantiate(rentPopupPrefab, rentPopupParent);
			rentPopup.GetComponent<MoneyJump>().SetUp(rent.Rent);
		}
	}
}