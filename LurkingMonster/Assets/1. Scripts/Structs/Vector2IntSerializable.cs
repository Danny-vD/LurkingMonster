using System;
using UnityEngine;

namespace Structs
{
	[Serializable]
	public struct Vector2IntSerializable
	{
		public int x;
		public int y;

		public Vector2IntSerializable(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
		
		public static implicit operator Vector2Int(Vector2IntSerializable vector2Int)
		{
			return new Vector2Int(vector2Int.x, vector2Int.y);
		}
		
		public static implicit operator Vector2IntSerializable(Vector2Int vector2Int)
		{
			return new Vector2IntSerializable(vector2Int.x, vector2Int.y);
		}
	}
}