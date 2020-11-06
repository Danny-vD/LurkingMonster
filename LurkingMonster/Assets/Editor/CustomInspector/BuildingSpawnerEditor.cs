using Enums;
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

		private bool[] prefabPerFoundationTypeFoldout;
		private bool[] foundationDataPerFoundationTypeFoldout;
		private bool[] prefabPerBuildingTypeFoldout;
		private bool[] buildingDataPerBuildingTypeFoldout;
		private bool[] soilDataPerSoilTypeFoldout;

		private bool foundationPrefabsFoldout;
		private bool foundationDataFoldout;
		private bool buildingPrefabsFoldout;
		private bool buildingDataFoldout;
		private bool soilDataFoldout;

		//////////////////////////////////////////////////
		private SerializedProperty foundations;
		private SerializedProperty foundationData;
		private SerializedProperty buildings;
		private SerializedProperty buildingTierData;
		private SerializedProperty soilData;

		private void OnEnable()
		{
			buildingSpawner = (BuildingSpawner) target;
			buildingSpawner.PopulateDictionaries();

			foundations      = serializedObject.FindProperty("foundations");
			foundationData   = serializedObject.FindProperty("foundationData");
			buildings        = serializedObject.FindProperty("buildings");
			buildingTierData = serializedObject.FindProperty("buildingTierData");
			soilData         = serializedObject.FindProperty("soilData");

			prefabPerFoundationTypeFoldout         = new bool[foundations.arraySize];
			foundationDataPerFoundationTypeFoldout = new bool[foundationData.arraySize];
			prefabPerBuildingTypeFoldout           = new bool[buildings.arraySize];
			buildingDataPerBuildingTypeFoldout     = new bool[buildingTierData.arraySize];
			soilDataPerSoilTypeFoldout             = new bool[soilData.arraySize];
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			if (IsFoldOut(ref foundationPrefabsFoldout, "Foundation prefabs"))
			{
				DrawFoldoutKeyValueArray<FoundationType>(foundations, "foundationType", "prefab",
					prefabPerFoundationTypeFoldout,
					new GUIContent("Prefab"));
			}

			if (IsFoldOut(ref foundationDataFoldout, "Foundation Data"))
			{
				DrawFoldoutKeyValueArray<FoundationType>(foundationData, "foundationType", "foundationTypeData",
					foundationDataPerFoundationTypeFoldout,
					new GUIContent("Foundation Data"));
			}

			if (IsFoldOut(ref buildingPrefabsFoldout, "Building prefabs"))
			{
				DrawFoldoutKeyValueArray<BuildingType>(buildings, "buildingType", "prefab",
					prefabPerBuildingTypeFoldout,
					new GUIContent("Prefab"));
			}

			if (IsFoldOut(ref buildingDataFoldout, "Building Tier Data"))
			{
				DrawFoldoutKeyValueArray<BuildingType>(buildingTierData, "buildingType", "buildingTypeData",
					buildingDataPerBuildingTypeFoldout, new GUIContent("Tier Data"));
			}
			
			if (IsFoldOut(ref soilDataFoldout, "Soil Data"))
			{
				DrawFoldoutKeyValueArray<SoilType>(soilData, "soilType", "soilTypeData",
					soilDataPerSoilTypeFoldout, new GUIContent("Soil Data"));
			}
			
			serializedObject.ApplyModifiedProperties();
		}
	}
}