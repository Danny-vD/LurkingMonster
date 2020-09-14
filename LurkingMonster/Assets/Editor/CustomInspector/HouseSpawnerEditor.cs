using Enums;
using Gameplay;
using UnityEditor;
using UnityEngine;
using static Utility.EditorUtils;

namespace CustomInspector
{
	[CustomEditor(typeof(HouseSpawner))]
	public class HouseSpawnerEditor : Editor
	{
		//////////////////////////////////////////////////
		private HouseSpawner houseSpawner;

		private bool[] prefabPerHouseTypeFoldout;
		private bool[] houseDataPerHouseTypeFoldout;

		private bool prefabsFoldout;
		private bool houseDataFoldout;

		//////////////////////////////////////////////////
		private SerializedProperty foundation;
		private SerializedProperty soilType;
		
		private SerializedProperty houses;
		private SerializedProperty houseData;

		private void OnEnable()
		{
			houseSpawner = target as HouseSpawner;

			houseSpawner.PopulateDictionaries();

			foundation = serializedObject.FindProperty("foundation");
			soilType   = serializedObject.FindProperty("soilType");

			houses    = serializedObject.FindProperty("houses");
			houseData = serializedObject.FindProperty("houseData");

			prefabPerHouseTypeFoldout    = new bool[houses.arraySize];
			houseDataPerHouseTypeFoldout = new bool[houseData.arraySize];
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			EditorGUILayout.PropertyField(foundation);
			EditorGUILayout.PropertyField(soilType);
			
			EditorGUILayout.Space();
			DrawSeperatorLine();
			EditorGUILayout.Space();
			
			if (IsFoldOut(ref prefabsFoldout, "House prefabs"))
			{
				DrawFoldoutKeyValueArray<HouseType>(houses, "houseType", "prefab", prefabPerHouseTypeFoldout,
					new GUIContent("Prefab"));
			}

			if (IsFoldOut(ref houseDataFoldout, "House Data"))
			{
				DrawFoldoutKeyValueArray<HouseType>(houseData, "houseType", "houseTypeData",
					houseDataPerHouseTypeFoldout, new GUIContent("HouseType Data"));
			}

			serializedObject.ApplyModifiedProperties();
		}
	}
}