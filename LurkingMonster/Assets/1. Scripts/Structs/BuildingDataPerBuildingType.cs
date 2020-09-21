using System;
using Enums;
using ScriptableObjects;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs
{
    [Serializable]
    public struct BuildingDataPerBuildingType : IKeyValuePair<BuildingType, BuildingTypeData>
    {
        [SerializeField]
        private BuildingType buildingType;
        
        [SerializeField]
        private BuildingTypeData buildingTypeData;

        public BuildingType Key
        {
            get => buildingType;
            set => buildingType = value;
        }

        public BuildingTypeData Value
        {
            get => buildingTypeData;
            set => buildingTypeData = value;
        }
        
        public bool Equals(IKeyValuePair<BuildingType, BuildingTypeData> other) => Key.Equals(other.Key);
    }
}