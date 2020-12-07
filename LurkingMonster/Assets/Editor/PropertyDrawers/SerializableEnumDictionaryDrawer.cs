using UnityEditor;
using UnityEngine;
using Utility;
using VDFramework.Extensions;
using static Utility.EditorUtils;

namespace PropertyDrawers
{
	[CustomPropertyDrawer(typeof(SerializableEnumDictionary<,>), true)]
	public class SerializableEnumDictionaryDrawer : PropertyDrawer
	{
		// Constants, for consistent layout
		private const float spacingDictionaryToPairs = 5.0f;
		private const float spacingKeyToValue = 0.0f;
		private const float valueIndent = 10.0f;
		private const float spacingBetweenPairs = 0.0f;
		private const float paddingAtEndOfProperty = 0.0f;

		private const float foldoutHeight = 20.0f;

		// Instance variables, to allow variable size between properties
		private Vector2 origin;
		private float propertySize;
		private float xpos;
		private float ypos;
		private float maxWidth;

		// foldouts for the foldout fields
		private bool foldoutDictionary;
		private bool[] foldouts;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			origin = new Vector2(position.x, position.y);

			propertySize = 0;

			xpos     = origin.x;
			ypos     = origin.y;
			maxWidth = position.width;

			SerializedProperty list = property.FindPropertyRelative("serializedDictionary");

			DrawDictionary(list, property.name);

			propertySize += paddingAtEndOfProperty;
		}

		private void ResizeFoldouts(SerializedProperty list)
		{
			if (foldouts == null || foldouts.Length != list.arraySize)
			{
				foldouts = new bool[list.arraySize];
			}
		}

		private void DrawDictionary(SerializedProperty list, string dictionaryName)
		{
			if (IsFoldOut(ref foldoutDictionary, $"{dictionaryName} (Size: {list.arraySize})"))
			{
				ResizeFoldouts(list);
				
				ypos += spacingDictionaryToPairs;

				DrawKeyValueArray(list, "key", "value", DrawPair);

				// Size = Y pos of end - Y pos of beginning - spacing at end of last pair + any padding
				propertySize = ypos - origin.y - spacingBetweenPairs;
			}
		}

		private void DrawPair(int index, SerializedProperty key, SerializedProperty value)
		{
			int enumIndex = key.enumValueIndex;
			string[] enumNames = key.enumNames;

			if (enumIndex < 0 || enumIndex >= enumNames.Length)
			{
				return;
			}
			
			if (IsFoldOut(ref foldouts[index], enumNames[enumIndex].ReplaceUnderscoreWithSpace()))
			{
				ypos += spacingKeyToValue;

				float xPosition = xpos + valueIndent;
				float height = EditorGUI.GetPropertyHeight(value, true);

				GUIContent label = new GUIContent(value.type);
				Rect rect = new Rect(xPosition, ypos, maxWidth - xPosition, height);

				EditorGUI.PropertyField(rect, value, label, true);
				ypos += height;
			}

			ypos += spacingBetweenPairs;
		}

		/// <summary>
		/// Creates a foldout label
		/// </summary>
		private bool IsFoldOut(ref bool foldout, string label = "")
		{
			Rect rect = new Rect(xpos, ypos, maxWidth - xpos, foldoutHeight);

			foldout =  EditorGUI.Foldout(rect, foldout, label);
			ypos    += foldoutHeight;

			return foldout;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
			base.GetPropertyHeight(property, label) + propertySize;
	}
}