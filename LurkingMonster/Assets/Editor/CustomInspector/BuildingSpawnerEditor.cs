using Enums;
using Gameplay;
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

		private bool[] prefabPerBuildingTypeFoldout;
		private bool[] buildingDataPerBuildingTypeFoldout;

		private bool prefabsFoldout;
		private bool buildingDataFoldout;

		//////////////////////////////////////////////////
		private SerializedProperty buildings;
		private SerializedProperty buildingData;

		private void OnEnable()
		{
			buildingSpawner = target as BuildingSpawner;
			buildingSpawner.PopulateDictionaries();

			buildings = serializedObject.FindProperty("buildings");
			buildingData = serializedObject.FindProperty("buildingData");

			prefabPerBuildingTypeFoldout    = new bool[buildings.arraySize];
			buildingDataPerBuildingTypeFoldout = new bool[buildingData.arraySize];
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();
			
			if (IsFoldOut(ref prefabsFoldout, "Building prefabs"))
			{
				DrawFoldoutKeyValueArray<BuildingType>(buildings, "buildingType", "prefab", prefabPerBuildingTypeFoldout,
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