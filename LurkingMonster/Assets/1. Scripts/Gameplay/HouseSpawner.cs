using System;
using System.Collections.Generic;
using System.Linq;
using Enums;
using ScriptableObjects;
using Structs;
using UnityEngine;
using VDFramework;
using VDFramework.Utility;

namespace Gameplay
{
    public class HouseSpawner : BetterMonoBehaviour
    {
        [SerializeField]
        private HouseType spawnType = HouseType.Normal;
        
        [SerializeField]
        private List<PrefabPerHouseType> houses = new List<PrefabPerHouseType>();

        [SerializeField]
        private List<HouseDataPerHouseType> houseData = new List<HouseDataPerHouseType>();

        public void Spawn(HouseType type)
        {
            GameObject prefab = houses.First(pair => pair.Key.Equals(type)).Value;
            GameObject instance = Instantiate(prefab);

            // House house = instance.GetComponent<House>();
            // house.Instantiate(GetData(type));
        }

        private void GetData(HouseType type)
        {
            HouseTypeData houseTypeData = houseData.First(pair => pair.Key.Equals(type)).Value;

            return; //houseTypeData.GetStruct();
        }

        private void OnMouseDown()
        {
            Spawn(spawnType);
        }


#if UNITY_EDITOR
        public void PopulateDictionaries()
        {
            EnumDictionaryUtil.PopulateEnumDictionary<PrefabPerHouseType, HouseType, GameObject>(houses);
            EnumDictionaryUtil.PopulateEnumDictionary<HouseDataPerHouseType, HouseType, HouseTypeData>(houseData);
        }
#endif
    }
}