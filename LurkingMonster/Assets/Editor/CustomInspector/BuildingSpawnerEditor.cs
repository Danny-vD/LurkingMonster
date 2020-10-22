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

		private bool foundationPrefabsFoldout;
		private bool foundationDataFoldout;
		private bool buildingPrefabsFoldout;
		private bool buildingDataFoldout;

		//////////////////////////////////////////////////
		private SerializedProperty foundations;
		private SerializedProperty foundationData;
		private SerializedProperty buildings;
		private SerializedProperty buildingData;

		private void OnEnable()
		{
			buildingSpawner = target as BuildingSpawner;
			buildingSpawner.PopulateDictionaries();

			foundations    = serializedObject.FindProperty("foundations");
			foundationData = serializedObject.FindProperty("foundationData");
			buildings      = serializedObject.FindProperty("buildings");
			buildingData   = serializedObject.FindProperty("buildingData");

			prefabPerFoundationTypeFoldout         = new bool[foundations.arraySize];
			foundationDataPerFoundationTypeFoldout = new bool[foundationData.arraySize];
			prefabPerBuildingTypeFoldout           = new bool[buildings.arraySize];
			buildingDataPerBuildingTypeFoldout     = new bool[buildingData.arraySize];
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

			if (IsFoldOut(ref buildingDataFoldout, "Building Data"))
			{
				DrawFoldoutKeyValueArray<BuildingType>(buildingData, "buildingType", "buildingTypeData",
					buildingDataPerBuildingTypeFoldout, new GUIContent("Tier Data"));
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}