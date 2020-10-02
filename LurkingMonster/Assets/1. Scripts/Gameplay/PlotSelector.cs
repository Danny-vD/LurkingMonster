using System;
using Events;
using Grid.Tiles;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	[RequireComponent(typeof(UnityEngine.Camera))]
	public class PlotSelector : BetterMonoBehaviour
	{
		private static AbstractBuildingTile selectedTile = null;
		private static Material originalMaterial = null;

		private static Action selectMethod;

		[SerializeField]
		private Material selectedMaterial = null;

		private UnityEngine.Camera playerCamera;

		private Ray RAY;

		private void Awake()
		{
			playerCamera = GetComponent<UnityEngine.Camera>();

			selectMethod = SystemInfo.deviceType == DeviceType.Handheld ? (Action) TouchSelect : MouseSelect;
		}

		private void Update()
		{
			selectMethod();

			Debug.DrawLine(RAY.origin, RAY.origin + RAY.direction * 10, Color.cyan);
		}

		/// <summary>
		/// Performs a raycast from the camera to the mouse
		/// </summary>
		/// <param name="buildingTile">The building tile that is hit by the raycast</param>
		/// <returns>True or False depending on whether we hit a building tile</returns>
		private bool RayCast(out AbstractBuildingTile buildingTile)
		{
			Ray mouseRay = playerCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(mouseRay, out RaycastHit hitinfo))
			{
				GameObject hitObject = hitinfo.collider.gameObject;
				buildingTile = hitObject.GetComponent<AbstractBuildingTile>();

				if (buildingTile)
				{
					return true;
				}
			}

			buildingTile = null;
			return false;
		}

		private void TouchSelect()
		{
			if (Input.touchCount != 1) // You can only select with 1 finger
			{
				return;
			}

			Touch touch = Input.GetTouch(0);

			if (touch.phase != TouchPhase.Began) // You can only select when you tap down
			{
				return;
			}

			HandleSelection();
		}

		private void MouseSelect()
		{
			if (Input.GetMouseButtonDown(0))
			{
				HandleSelection();
			}
		}

		private void HandleSelection()
		{
			if (!RayCast(out AbstractBuildingTile buildingTile)) // return if raycast did not hit a building
			{
				Deselect(selectedTile);
				return;
			}

			Select(buildingTile);
		}

		private void Select(AbstractBuildingTile tile)
		{
			// Selected the selected tile, so open market
			if (tile == selectedTile)
			{
				EventManager.Instance.RaiseEvent(new OpenMarketEvent(tile, tile.Building));
				Deselect(tile);
				return;
			}

			Deselect(selectedTile);

			selectedTile     = tile;
			originalMaterial = tile.GetComponent<Renderer>().sharedMaterial;

			ChangePlotMaterial(tile, true);
		}

		private void Deselect(AbstractBuildingTile tile)
		{
			selectedTile = null;

			if (tile == null) // It's not guaranteed to be non-null
			{
				return;
			}

			ChangePlotMaterial(tile, false);
			originalMaterial = null;
		}

		private void ChangePlotMaterial(AbstractBuildingTile tile, bool isSelected)
		{
			tile.GetComponent<Renderer>().sharedMaterial = isSelected ? selectedMaterial : originalMaterial;
		}
	}
}