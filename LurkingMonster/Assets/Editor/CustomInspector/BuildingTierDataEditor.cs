using System;
using Enums;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomInspector
{
	[CustomEditor(typeof(BuildingTierData))]
	public class BuildingTierDataEditor : Editor
	{
		private BuildingTierData buildingTierData;

		private bool[] buildingTierMeshPerBuildingTypesFoldouts;

		//////////////////////////////////////////////////
		private SerializedProperty buildingTierMeshPerBuildingTypes;

		private void OnEnable()
		{
			buildingTierData = target as BuildingTierData;
			buildingTierData.PopulateDictionaries();

			buildingTierMeshPerBuildingTypes = serializedObject.FindProperty("buildingTierMeshPerBuildingTypes");

			buildingTierMeshPerBuildingTypesFoldouts = new bool[buildingTierMeshPerBuildingTypes.arraySize];
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			DrawFoldoutKeyValueArray<BuildingType>(buildingTierMeshPerBuildingTypes, "key", "value",
				buildingTierMeshPerBuildingTypesFoldouts, new GUIContent("Meshes"));

			serializedObject.ApplyModifiedProperties();
		}
	}
}