using Structs;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "BuildingType Data")]
    public class BuildingTypeData : ScriptableObject
    {
        [SerializeField]
        private int rent = 10;

        [SerializeField]
        private int weight = 100;

        [SerializeField]
        private int price = 10;

        public BuildingData GetStruct()
        {
            return new BuildingData(rent, weight, price, default, default);
        }
    }
}