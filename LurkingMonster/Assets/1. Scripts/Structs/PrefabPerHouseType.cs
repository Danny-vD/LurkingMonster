using System;
using Enums;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs
{
    [Serializable]
    public struct PrefabPerHouseType : IKeyValuePair<HouseType, GameObject>
    {
        [SerializeField]
        private HouseType houseType;
        
        [SerializeField]
        private GameObject prefab;

        public HouseType Key
        {
            get => houseType;
            set => houseType = value;
        }

        public GameObject Value
        {
            get => prefab;
            set => prefab = value;
        }

        public bool Equals(IKeyValuePair<HouseType, GameObject> other) => Key.Equals(other.Key);
    }
}