using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VDFramework.EventSystem;

public class CollectRentEvent : VDEvent
{
	public float rent;

	public CollectRentEvent(float rent) => this.rent = rent;
}
