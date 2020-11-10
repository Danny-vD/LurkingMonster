using System;
using Enums;
using ScriptableObjects;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Buildings
{
	[Serializable]
	public struct FoundationDataPerFoundationType : IKeyValuePair<FoundationType, FoundationTypeData>
	{
		[SerializeField]
		private FoundationType foundationType;
        
		[SerializeField]
		private FoundationTypeData foundationTypeData;

		public FoundationType Key
		{
			get => foundationType;
			set => foundationType = value;
		}

		public FoundationTypeData Value
		{
			get => foundationTypeData;
			set => foundationTypeData = value;
		}
        
		public bool Equals(IKeyValuePair<FoundationType, FoundationTypeData> other) => Key.Equals(other.Key);
	}
}