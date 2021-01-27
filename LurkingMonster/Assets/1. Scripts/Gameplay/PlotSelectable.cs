using System;
using System.Collections.Generic;
using Grid.Tiles;
using Grid.Tiles.Buildings;
using Interfaces;
using UnityEngine;
using VDFramework;

namespace Gameplay
{
	[RequireComponent(typeof(AbstractTile))]
	public class PlotSelectable : BetterMonoBehaviour, ISelectable
	{
		// Have to store the old material for every renderer
		private readonly Dictionary<Renderer, Material> materials = new Dictionary<Renderer, Material>();

		private AbstractTile tile;

		private Action<Material> select;

		private void Awake()
		{
			tile = GetComponent<AbstractTile>();

			switch (tile)
			{
				case AbstractBuildingTile buildingTile:
					select = SelectBuilding;
					break;
				default:
					select = SelectTile;
					break;
			}
		}

		/// <summary>
		/// Select this selectable
		/// </summary>
		public void Select(Material selectMaterial)
		{
			@select(selectMaterial);
		}

		public AbstractTile GetTile()
		{
			return tile;
		}

		private void SelectBuilding(Material selectMaterial)
		{
			AbstractBuildingTile buildingTile = (AbstractBuildingTile) tile;
			
			// In case of no soil, a building or debris we just select the tile itself
			if (buildingTile.HasBuilding || buildingTile.HasDebris || !buildingTile.HasSoil)
			{
				SelectObject(CachedGameObject, selectMaterial);
				return;
			}

			// In case of soil or foundation, select the soil object
			SelectObject(buildingTile.Soil, selectMaterial);
		}

		private void SelectTile(Material selectMaterial)
		{
			SelectObject(gameObject, selectMaterial);
		}

		public void Deselect()
		{
			foreach (KeyValuePair<Renderer, Material> pair in materials)
			{
				pair.Key.sharedMaterial = pair.Value;
			}

			materials.Clear();
		}

		private void SelectObject(GameObject @object, Material selectMaterial)
		{
			// Select the current object if it has a renderer, else select all children
			Renderer meshRenderer = @object.GetComponent<Renderer>();

			if (meshRenderer)
			{
				materials.Add(meshRenderer, meshRenderer.sharedMaterial);
				meshRenderer.sharedMaterial = selectMaterial;
				return;
			}

			Renderer[] meshRenderers = @object.GetComponentsInChildren<Renderer>();

			foreach (Renderer childRenderer in meshRenderers)
			{
				materials.Add(childRenderer, childRenderer.sharedMaterial);
				childRenderer.sharedMaterial = selectMaterial;
			}
		}
	}
}