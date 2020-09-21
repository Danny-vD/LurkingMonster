using Enums.Grid;
using Events;
using UnityEngine;
using VDFramework.EventSystem;

namespace Grid.Tiles
{
	public class SmallBuildingTile : AbstractBuildingTile
	{
		public override TileType TileType => TileType.SmallBuilding;

		private GameObject canvas;

		protected override void Awake()
		{
			base.Awake();

			canvas = CachedTransform.Find("Canvas").gameObject;
		}
		
		public override void SpawnBuilding()
		{
			base.SpawnBuilding();
			EventManager.Instance.RaiseEvent(new DecreaseMoneyEvent(BuildingData.Price));
		}

		// NOTE: temporary, it's shite, but it works...
		private void OnMouseDown()
		{
			canvas.SetActive(!canvas.activeInHierarchy);
		}

	}
}