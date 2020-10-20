using System;
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Buildings
{
    [Serializable]
    public struct BuildingDataPerBuildingType : IKeyValuePair<BuildingType, List<BuildingTypeData>>
    {
        [SerializeField]
        private BuildingType buildingType;
        
        [SerializeField]
        private List<BuildingTypeData> buildingTypeData;

        public BuildingType Key
        {
            get => buildingType;
            set => buildingType = value;
        }

        public List<BuildingTypeData> Value
        {
            get => buildingTypeData;
            set => buildingTypeData = value;
        }
        
        public bool Equals(IKeyValuePair<BuildingType, List<BuildingTypeData>> other) => Key.Equals(other.Key);
    }
}