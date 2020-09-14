using System.Collections;
using System.Collections.Generic;
using Enums;
using Structs;
using UnityEngine;
using VDFramework;

public class House : BetterMonoBehaviour
{
    private HouseData data;
    
    public void Instantiate(HouseData data)
    {
        this.data = data;
    }
}
