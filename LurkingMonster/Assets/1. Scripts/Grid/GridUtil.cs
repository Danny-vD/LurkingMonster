using Grid.Tiles;
using UnityEngine;
using VDFramework;

namespace Grid
{
	[RequireComponent(typeof(GridCreator))]
	public class GridUtil : BetterMonoBehaviour
	{
		private static AbstractTile[,] grid;
		private static GridData gridData;

		public static AbstractTile[,] Grid => grid ?? new AbstractTile[0, 0];

		public static GridData GridData => gridData;

		private void Start()
		{
			gridData = GetComponent<GridData>();
			grid     = GetComponent<GridCreator>().GenerateGrid();
		}
	}
}