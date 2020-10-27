using System;
using Enums;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Buildings
{
	[Serializable]
	public struct PrefabPerFoundationType : IKeyValuePair<FoundationType, GameObject>
	{
		[SerializeField]
		private FoundationType foundationType;
        
		[SerializeField]
		private GameObject prefab;

		public FoundationType Key
		{
			get => foundationType;
			set => foundationType = value;
		}

		public GameObject Value
		{
			get => prefab;
			set => prefab = value;
		}

		public bool Equals(IKeyValuePair<FoundationType, GameObject> other) => Key.Equals(other.Key);
	}
}