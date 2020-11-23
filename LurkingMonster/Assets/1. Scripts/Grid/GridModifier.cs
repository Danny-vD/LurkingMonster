using System.Collections.Generic;
using Enums.Grid;
using UnityEngine;
using VDFramework;

namespace Grid
{
	public class GridModifier : BetterMonoBehaviour
	{
		private GridData data = null;
		private GridCreator gridCreator = null;

		public Vector2Int[] SelectedPositions = new Vector2Int[0];
		
		[SerializeField]
		private TileType newType = default;
		
		private void Awake()
		{
			data = GetComponent<GridData>();
			gridCreator = GetComponent<GridCreator>();
		}

		public void ModifyTiles(bool regenerateGrid)
		{
			if (!data)
			{
				data = GetComponent<GridData>();
			}
			
			foreach (Vector2Int position in SelectedPositions)
			{
				data.ChangeTile(position, newType);
			}

			if (regenerateGrid)
			{
				RegenerateGrid();
			}
		}
		
		private void RegenerateGrid()
		{
			if (!gridCreator)
			{
				gridCreator = GetComponent<GridCreator>();
			}
			
			gridCreator.GenerateGrid();
		}
	}
}