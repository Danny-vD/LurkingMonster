using System;
using Enums;
using ScriptableObjects;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Buildings
{
	[Serializable]
	public struct SoilDataPerSoilType : IKeyValuePair<SoilType, SoilTypeData>
	{
		[SerializeField]
		private SoilType soilType;

		[SerializeField]
		private SoilTypeData soilTypeData;

		public SoilType Key
		{
			get => soilType;
			set => soilType = value;
		}

		public SoilTypeData Value
		{
			get => soilTypeData;
			set => soilTypeData = value;
		}

		public bool Equals(IKeyValuePair<SoilType, SoilTypeData> other) => Key.Equals(other.Key);
	}
}