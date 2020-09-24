using System;
using Enums;
using UnityEngine;
using UnityEngine.Serialization;
using VDFramework.Interfaces;

namespace Structs
{
    [Serializable]
    public struct PrefabPerBuildingType : IKeyValuePair<BuildingType, GameObject>
    {
        [SerializeField]
        private BuildingType buildingType;
        
        [SerializeField]
        private GameObject prefab;

        public BuildingType Key
        {
            get => buildingType;
            set => buildingType = value;
        }

        public GameObject Value
        {
            get => prefab;
            set => prefab = value;
        }

        public bool Equals(IKeyValuePair<BuildingType, GameObject> other) => Key.Equals(other.Key);
    }
}