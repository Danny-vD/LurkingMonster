using System.Collections.Generic;
using Grid.Tiles.Buildings;
using Interfaces;
using UnityEngine;
using VDFramework;

namespace Gameplay
{
	[RequireComponent(typeof(AbstractBuildingTile))]
	public class PlotSelectable : BetterMonoBehaviour, ISelectable
	{
		// Have to store the old material for every renderer
		private readonly Dictionary<Renderer, Material> materials = new Dictionary<Renderer, Material>();

		private AbstractBuildingTile tile;

		private void Awake()
		{
			tile = GetComponent<AbstractBuildingTile>();
		}

		public AbstractBuildingTile GetTile()
		{
			return tile;
		}

		public void Select(Material selectMaterial)
		{
			if (tile.HasFoundation)
			{
				// In case of foundation, select the foundation object
				Select(tile.Foundation, selectMaterial);
			}

			if (tile.HasSoil)
			{
				// In case of soil, select the soil object
				Select(tile.Soil, selectMaterial);
			}

			// In case of no soil, a building or debris we just select the tile itself
			Select(CachedGameObject, selectMaterial);
		}

		public void Deselect()
		{
			foreach (KeyValuePair<Renderer, Material> pair in materials)
			{
				pair.Key.sharedMaterial = pair.Value;
			}

			materials.Clear();
		}

		private void Select(GameObject @object, Material selectMaterial)
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
			}
		}
	}
}