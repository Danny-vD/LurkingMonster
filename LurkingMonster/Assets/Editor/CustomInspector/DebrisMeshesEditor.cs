using Enums;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomInspector
{
	[CustomEditor(typeof(DebrisPrefabs))]
	public class DebrisPrefabsEditor : Editor
	{
		private DebrisPrefabs debrisPrefabs;

		private bool[] debrisPrefabsFoldouts;

		//////////////////////////////////////////////////
		private SerializedProperty debrisPerBuilding;

		private void OnEnable()
		{
			debrisPrefabs = target as DebrisPrefabs;
			debrisPrefabs.PopulateDictionary();

			debrisPerBuilding = serializedObject.FindProperty("debrisPerBuilding");
			
			debrisPrefabsFoldouts = new bool[debrisPerBuilding.arraySize];
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			DrawFoldoutKeyValueArray<BuildingType>(debrisPerBuilding, "key", "value", debrisPrefabsFoldouts, DrawPrefabArray);

			serializedObject.ApplyModifiedProperties();
		}

		private static void DrawPrefabArray(int index, SerializedProperty buildingType, SerializedProperty prefabs)
		{
			prefabs.arraySize = Mathf.Clamp(EditorGUILayout.IntField("Tier Count", prefabs.arraySize), 0, int.MaxValue);
			
			for (int i = 0; i < prefabs.arraySize; i++)
			{
				DrawArrayElement(i, prefabs.GetArrayElementAtIndex(i));
			}
			
			void DrawArrayElement(int elementIndex, SerializedProperty prefab)
			{
				EditorGUILayout.PropertyField(prefab, new GUIContent($"Tier {elementIndex + 1}"));
			}
		}
	}
}