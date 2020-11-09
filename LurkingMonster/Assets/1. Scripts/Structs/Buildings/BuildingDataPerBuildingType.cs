using System;
using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs.Buildings
{
    [Serializable]
    public struct BuildingTierDataPerBuildingType : IKeyValuePair<BuildingType, List<BuildingTierData>>
    {
        [SerializeField]
        private BuildingType buildingType;
        
        [SerializeField]
        private List<BuildingTierData> buildingTypeData;

        public BuildingType Key
        {
            get => buildingType;
            set => buildingType = value;
        }

        public List<BuildingTierData> Value
        {
            get => buildingTypeData;
            set => buildingTypeData = value;
        }
        
        public bool Equals(IKeyValuePair<BuildingType, List<BuildingTierData>> other) => Key.Equals(other.Key);
    }
}