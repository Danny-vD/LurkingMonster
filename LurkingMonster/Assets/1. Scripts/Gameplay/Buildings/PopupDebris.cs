using Events.BuildingEvents;
using Events.MoneyManagement;
using Grid.Tiles.Buildings;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.Buildings
{
	public class PopupDebris : BetterMonoBehaviour
	{
		[SerializeField]
		private Button delete;

		[SerializeField]
		private TextMeshProUGUI priceText;
		
		private void Start()
		{
			SelectedBuildingEvent.Listeners += OnSelectedBuilding;
		}

		private void OnSelectedBuilding(SelectedBuildingEvent selectedBuildingEvent)
		{
			AbstractBuildingTile tile = selectedBuildingEvent.Tile;

			if (tile == null || !tile.HasDebris)
			{
				return;
			}

			transform.GetChild(0).gameObject.SetActive(true);
			SetButton(tile);
		}

		private void SetButton(AbstractBuildingTile tile)
		{
			delete.onClick.RemoveAllListeners();

			int price = tile.DebrisRemovalCost;

			priceText.text = price.ToString();
			
			if (MoneyManager.Instance.PlayerHasEnoughMoney(price))
			{
				delete.onClick.AddListener(OnClick);	
			}
			
			void OnClick()
			{
				EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(price));

				tile.RemoveDebris();	
				
				transform.GetChild(0).gameObject.SetActive(false);
			}
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			SelectedBuildingEvent.Listeners -= OnSelectedBuilding;
		}
	}
}