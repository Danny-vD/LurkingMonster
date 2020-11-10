using Enums.Grid;
using Random = UnityEngine.Random;

namespace Grid.Tiles
{
	public class TreeTile : AbstractTile
	{
		private const float rotationValue = 90; // Transform.Rotate uses degrees
		
		public override TileType TileType => TileType.Tree;

		private void Start()
		{
			CachedTransform.Rotate(CachedTransform.up, rotationValue * Random.Range(0, 4));
		}
	}
}