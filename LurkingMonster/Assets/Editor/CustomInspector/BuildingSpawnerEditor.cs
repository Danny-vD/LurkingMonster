﻿using Enums;
using Gameplay.Buildings;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomInspector
{
	[CustomEditor(typeof(BuildingSpawner))]
	public class BuildingSpawnerEditor : Editor
	{
		private BuildingSpawner buildingSpawner;

		private bool[] soilDataPerSoilTypeFoldout;
		private bool[] foundationDataPerFoundationTypeFoldout;
		private bool[] buildingDataPerBuildingTypeFoldout;

		private bool soilDataFoldout;
		private bool foundationDataFoldout;
		private bool buildingDataFoldout;

		//////////////////////////////////////////////////
		private SerializedProperty soilData;
		private SerializedProperty foundationData;
		private SerializedProperty buildingTierData;

		private void OnEnable()
		{
			buildingSpawner = (BuildingSpawner) target;
			buildingSpawner.PopulateDictionaries();

			soilData         = serializedObject.FindProperty("soilData");
			foundationData   = serializedObject.FindProperty("foundationData");
			buildingTierData = serializedObject.FindProperty("buildingTierData");

			foundationDataPerFoundationTypeFoldout = new bool[foundationData.arraySize];
			buildingDataPerBuildingTypeFoldout     = new bool[buildingTierData.arraySize];
			soilDataPerSoilTypeFoldout             = new bool[soilData.arraySize];
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			if (IsFoldOut(ref soilDataFoldout, "Soil Data"))
			{
				DrawFoldoutKeyValueArray<SoilType>(soilData, "soilType", "soilTypeData",
					soilDataPerSoilTypeFoldout, new GUIContent("Soil Data"));
			}
			
			if (IsFoldOut(ref foundationDataFoldout, "Foundation Data"))
			{
				DrawFoldoutKeyValueArray<FoundationType>(foundationData, "foundationType", "foundationTypeData",
					foundationDataPerFoundationTypeFoldout,
					new GUIContent("Foundation Data"));
			}

			if (IsFoldOut(ref buildingDataFoldout, "Building Tier Data"))
			{
				DrawFoldoutKeyValueArray<BuildingType>(buildingTierData, "buildingType", "buildingTypeData",
					buildingDataPerBuildingTypeFoldout, new GUIContent("Tier Data"));
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}