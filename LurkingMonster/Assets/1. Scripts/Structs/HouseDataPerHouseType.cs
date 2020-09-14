using System;
using Enums;
using ScriptableObjects;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs
{
    [Serializable]
    public struct HouseDataPerHouseType : IKeyValuePair<HouseType, HouseTypeData>
    {
        [SerializeField]
        private HouseType houseType;
        
        [SerializeField]
        private HouseTypeData houseTypeData;

        public HouseType Key
        {
            get => houseType;
            set => houseType = value;
        }

        public HouseTypeData Value
        {
            get => houseTypeData;
            set => houseTypeData = value;
        }
        
        public bool Equals(IKeyValuePair<HouseType, HouseTypeData> other) => Key.Equals(other.Key);
    }
}