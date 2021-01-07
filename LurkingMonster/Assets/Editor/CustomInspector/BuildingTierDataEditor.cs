using Enums;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomInspector
{
	[CustomEditor(typeof(BuildingMeshData))]
	public class BuildingMeshDataEditor : Editor
	{
		private BuildingMeshData buildingMeshData;

		private bool[] buildingTierMeshPerBuildingTypesFoldouts;

		//////////////////////////////////////////////////
		private SerializedProperty buildingTierMeshPerBuildingTypes;

		private void OnEnable()
		{
			buildingMeshData = (BuildingMeshData) target;
			buildingMeshData.PopulateDictionaries();

			buildingTierMeshPerBuildingTypes = serializedObject.FindProperty("buildingTierMeshPerBuildingTypes");

			buildingTierMeshPerBuildingTypesFoldouts = new bool[buildingTierMeshPerBuildingTypes.arraySize];
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			DrawFoldoutKeyValueArray<BuildingType>(buildingTierMeshPerBuildingTypes, "key", "value",
				buildingTierMeshPerBuildingTypesFoldouts, DrawMeshArray);

			serializedObject.ApplyModifiedProperties();
		}

		private static void DrawMeshArray(int index, SerializedProperty buildingType, SerializedProperty meshes)
		{
			meshes.arraySize = Mathf.Clamp(EditorGUILayout.IntField("Tier Count", meshes.arraySize), 0, int.MaxValue);
			
			for (int i = 0; i < meshes.arraySize; i++)
			{
				DrawArrayElement(i, meshes.GetArrayElementAtIndex(i));
			}
			
			void DrawArrayElement(int elementIndex, SerializedProperty mesh)
			{
				EditorGUILayout.PropertyField(mesh, new GUIContent($"Tier {elementIndex + 1}"));
			}
		}
	}
}