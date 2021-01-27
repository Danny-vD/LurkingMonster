using Enums.Grid;
using UnityEngine;
using VDFramework;

namespace Grid
{
	/// <summary>
	/// A class specifically made to be used in the insector to modify the grid
	/// </summary>
	public class GridModifier : BetterMonoBehaviour
	{
		[SerializeField]
		private TileType newType = default;
		
		public Vector2Int[] SelectedPositions = new Vector2Int[0];
		
		private GridData data = null;
		private GridCreator gridCreator = null;

		private void Awake()
		{
			data = GetComponent<GridData>();
			gridCreator = GetComponent<GridCreator>();
		}

		/// <summary>
		/// Change all selected tiles to the new tileType
		/// </summary>
		/// <param name="regenerateGrid">Regenerate the grid after setting the new tiles</param>
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