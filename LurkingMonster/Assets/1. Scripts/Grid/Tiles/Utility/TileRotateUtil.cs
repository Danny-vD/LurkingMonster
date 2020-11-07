using System.Collections.Generic;
using System.Linq;
using Enums.Utility;
using ExtentionMethods;
using UnityEngine;

namespace Grid.Tiles.Utility
{
	public static class TileRotateUtil
	{
		/// <summary>
		///  Rotate a corner tile to match the tiles it should connect to, decided by the ending directions
		/// </summary>
		/// <param name="tile">The tile to rotate</param>
		/// <param name="ending1">The transform direction towards one of the endings of the curve</param>
		/// <param name="ending2">The transform direction towards the other ending of the curve</param>
		/// <param name="connectTo">A collection of the tiles that the curve should connect to</param>
		public static void RotateCorner(AbstractTile tile, Direction ending1, Direction ending2,
			IEnumerable<AbstractTile>                connectTo)
		{
			Transform neighbor1 = connectTo.ElementAt(0).CachedTransform;
			Transform neighbor2 = connectTo.ElementAt(1).CachedTransform;

			tile.CachedTransform.DirectionLookAtTransform(ending1, neighbor1);

			// Because it's a 90° corner, the other ending either looks away or towards the other neighbor at this point
			// If it's facing away, flip the tile by making ending1 look at the other neighbor
			if (!tile.CachedTransform.DirectionIsFacingTransform(ending2, neighbor2))
			{
				tile.CachedTransform.DirectionLookAtTransform(ending1, neighbor2);
			}
		}

		public static void RotateTIntersection(AbstractTile tile,
			Direction perpendicularDirection, Direction ending2, Direction ending3, IEnumerable<AbstractTile> connectTo)
		{
			Transform[] connections = connectTo.Select(connection => connection.CachedTransform).ToArray();

			// For every Neighbor rotate perpendicular towards neighbor
			// then check if the abs direction to the other neighbors is the same
			// if that is the case, we are rotated correctly (because it's a T crossing)
			for (int i = 0; i < connections.Length; i++)
			{
				Transform currentDirection = connections[i];
				tile.CachedTransform.DirectionLookAtTransform(perpendicularDirection, currentDirection);

				Transform neighbor2 = connections[(i + 1) % connections.Length];
				Transform neighbor3 = connections[(i + 2) % connections.Length];

				Vector3 neighbor2Delta = Abs((neighbor2.position - tile.CachedTransform.position).normalized);
				Vector3 neighbor3Delta = Abs((neighbor3.position - tile.CachedTransform.position).normalized);

				if (neighbor2Delta == neighbor3Delta)
				{
					return;
				}
			}

			Debug.LogError("Could not rotate intersection correctly", tile);
		}

		private static Vector3 Abs(Vector3 vector3)
		{
			return new Vector3(Mathf.Abs(vector3.x), Mathf.Abs(vector3.y), Mathf.Abs(vector3.z));
		}
	}
}