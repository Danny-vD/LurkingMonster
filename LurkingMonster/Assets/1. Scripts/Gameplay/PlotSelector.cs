using System;
using Events.BuildingEvents;
using Events.OpenMarketEvents;
using Grid.Tiles;
using Grid.Tiles.Buildings;
using Grid.Tiles.SpecialBuildings;
using Interfaces;
using Singletons;
using Tutorials;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
{
	[RequireComponent(typeof(Camera))]
	public class PlotSelector : BetterMonoBehaviour
	{
		private static PlotSelectable selected = null;

		private static Action selectMethod;

		[SerializeField]
		private Material selectedMaterial = null;

		private Camera playerCamera;

		private void Awake()
		{
			playerCamera = GetComponent<Camera>();

			selectMethod = SystemInfo.deviceType == DeviceType.Handheld ? (Action) TouchSelect : MouseSelect;
		}

		private void Update()
		{
			if (TimeManager.Instance.IsPaused())
			{
				return;
			}

			selectMethod();
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

		/// <summary>
		/// Performs a raycast from the camera to the mouse
		/// </summary>
		/// <param name="selectable">The PlotSelectable that is hit by the raycast</param>
		/// <returns>True or False depending on whether we hit a PlotSelectable</returns>
		private bool RayCast(out PlotSelectable selectable)
		{
			Ray mouseRay = playerCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(mouseRay, out RaycastHit hitinfo))
			{
				GameObject hitObject = hitinfo.collider.gameObject;
				selectable = hitObject.GetComponent<PlotSelectable>();

				if (selectable != null)
				{
					return true;
				}
			}

			selectable = null;
			return false;
		}

		private void HandleSelection()
		{
			if (PointerUtil.IsPointerOverUIElement() || TutorialManager.IsActive)
			{
				return;
			}

			if (RayCast(out PlotSelectable selectable)) // check if raycast hit a PlotSelectable
			{
				Select(selectable);
				return;
			}

			EventManager.Instance.RaiseEvent(new SelectedBuildingTileEvent(null, false));
			Deselect(selected);
		}

		private void Select(PlotSelectable selectable)
		{
			AbstractTile abstractTile = selectable.GetTile();

			if (!(abstractTile is AbstractBuildingTile))
			{
				// we did not select a building, so tell our listeners
				EventManager.Instance.RaiseEvent(new SelectedBuildingTileEvent(null, false));
			}

			switch (abstractTile) // Handle special actions if we selected an already selected tile
			{
				case AbstractBuildingTile buildingTile when SelectedBuilding(selectable, buildingTile):
				case ResearchFacilityTile researchFacilityTile when SelectedResearchFacilityTile(selectable):
					return;
			}

			Deselect(selected); // Deselect the last selected object

			// select the new object
			selected = selectable;
			selectable.Select(selectedMaterial);
		}

		private static bool SelectedBuilding(PlotSelectable selectable, AbstractBuildingTile abstractBuildingTile)
		{
			// Selected the selected object, so open market
			if (selectable == selected)
			{
				Deselect(selectable);

				if (abstractBuildingTile.HasDebris)
				{
					EventManager.Instance.RaiseEvent(new SelectedBuildingTileEvent(abstractBuildingTile, true));
					return true;
				}

				EventManager.Instance.RaiseEvent(new OpenMarketEvent(abstractBuildingTile));
				return true;
			}

			// Send the tile if it has a building, else send null so that the listeners know you selected something that has no building
			EventManager.Instance.RaiseEvent(new SelectedBuildingTileEvent(abstractBuildingTile.HasBuilding ? abstractBuildingTile : null, true));

			return false;
		}

		private static bool SelectedResearchFacilityTile(PlotSelectable selectable)
		{
			// Selected the selected object, so open research facility
			if (selectable == selected)
			{
				Deselect(selectable);

				EventManager.Instance.RaiseEvent(new OpenResearchFacilityEvent());
				return true;
			}

			return false;
		}

		private static void Deselect(ISelectable selectable)
		{
			selectable?.Deselect();

			selected = null;
		}
	}
}