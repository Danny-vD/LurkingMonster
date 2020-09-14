using System;
using Enums.Grid;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Grid
{
	[Serializable]
	public struct TileTypePerPosition : IKeyValuePair<Vector2Int, TileType>
	{
		[SerializeField]
		private Vector2Int key;

		[SerializeField]
		private TileType value;

		public Vector2Int Key
		{
			get => key;
			set => key = value;
		}

		public TileType Value
		{
			get => value;
			set => this.value = value;
		}

		public bool Equals(IKeyValuePair<Vector2Int, TileType> other) => key.Equals(other.Key);
	}
}