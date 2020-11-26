using Enums.Grid;
using UnityEngine;

namespace Grid.Tiles
{
	public class GrassTile : AbstractTile
	{
		private const float rotationValue = 90; // Transform.Rotate uses degrees
		
		public override TileType TileType => TileType.Grass;

		private void Start()
		{
			CachedTransform.Rotate(CachedTransform.up, rotationValue * Random.Range(0, 4));
		}
	}
}