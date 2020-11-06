using System;
using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(menuName = "Building Data/SoilType Data")]
	public class SoilTypeData : ScriptableObject
	{
		public int buildCost = 200;
		
		public int removeCost = 100;

		public float maxHealth = 50;

		public Material[] materials = new Material[0];
	}
}