using System;
using System.Collections.Generic;
using Enums.Grid;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Grid
{
	[Serializable]
	public struct PrefabsPerTileType : IKeyValuePair<TileType, List<GameObject>>
	{
		[SerializeField]
		private TileType tileType;

		[SerializeField]
		private List<GameObject> prefabs;

		public TileType Key
		{
			get => tileType;
			set => tileType = value;
		}

		public List<GameObject> Value
		{
			get => prefabs;
			set => prefabs = value;
		}

		public bool Equals(IKeyValuePair<TileType, List<GameObject>> other) => tileType.Equals(other.Key);
	}
}