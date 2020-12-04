using System;
using _1._Scripts.Tutorial;
using Events;
using Interfaces;
using Singletons;
using UnityEngine;
using UnityEngine.EventSystems;
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
			if (EventSystem.current.IsPointerOverGameObject() || TutorialManager.Instance.IsActive)
			{
				return;	
			}
			
			if (RayCast(out PlotSelectable selectable)) // check if raycast hit a PlotSelectable
			{
				Select(selectable);
				return;
			}

			Deselect(selected);
		}

		private void Select(PlotSelectable selectable)
		{
			// Selected the selected object, so open market
			if (selectable == selected)
			{
				Deselect(selectable);
				EventManager.Instance.RaiseEvent(new OpenMarketEvent(selectable.GetTile()));
				return;
			}

			Deselect(selected); // Deselect the last selected object

			// select the new object
			selected = selectable;
			selectable.Select(selectedMaterial);
		}

		private static void Deselect(ISelectable selectable)
		{
			selectable?.Deselect();

			selected = null;
		}
	}
}