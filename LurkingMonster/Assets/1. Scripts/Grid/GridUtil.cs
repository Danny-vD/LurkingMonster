namespace Grid
{
	using System;
	using Tiles;
	using UnityEngine;
	using VDFramework;

	[RequireComponent(typeof(GridCreator))]
	public class GridUtil : BetterMonoBehaviour
	{
		private static AbstractTile[,] grid;
		private static GridData gridData;

		public static AbstractTile[,] Grid => grid;
		public static GridData GridData => gridData;
		
		private void Start()
		{
			gridData = GetComponent<GridData>();
			grid     = GetComponent<GridCreator>().GenerateGrid();
		}
	}
}