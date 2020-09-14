﻿using Enums;

namespace Structs
{
    public struct HouseData
    {
        private float rent;
        private int weight;
        private int price;

        private SoilType soilType;
        private FoundationType foundation;
        
        public HouseData(float rent, int weight, int price, SoilType soilType, FoundationType foundation)
        {
            this.rent       = rent;
            this.weight     = weight;
            this.price      = price;
            this.soilType   = soilType;
            this.foundation = foundation;
        }


        public float Rent
        {
            get => rent;
            set => rent = value;
        }
        
        public int Weight
        {
            get => weight;
            set => weight = value;
        }

        public SoilType SoilType
        {
            get => soilType;
            set => soilType = value;
        }

        public FoundationType Foundation
        {
            get => foundation;
            set => foundation = value;
        }
        
        public int Price
        {
            get => price;
            set => price = value;
        }
    }
}