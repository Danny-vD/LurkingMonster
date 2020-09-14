using Structs;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "HouseType Data")]
    public class HouseTypeData : ScriptableObject
    {
        [SerializeField]
        private float rent = 10;

        [SerializeField]
        private int weight = 100;

        [SerializeField]
        private int price = 10;

        public HouseData GetStruct()
        {
            return new HouseData(rent, weight, price, default, default);
        }
    }
}