using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Utility;
using VDFramework.Extensions;
using static Utility.EditorUtils;

namespace PropertyDrawers
{
	[CustomPropertyDrawer(typeof(SerializableDictionary<,>), false)]
	public class SerializableDictionaryDrawer : PropertyDrawer
	{
		// Constants, for consistent layout
		private const float spacingWarningToDictionary = 5.0f;
		private const float spacingDictionaryToPairs = 5.0f;
		private const float spacingLabelToPair = 0.0f;
		private const float pairIndent = 10.0f;
		private const float spacingBetweenPairValues = 2.0f;
		private const float spacingBetweenPairs = 0.0f;
		private const float paddingAtEndOfProperty = 0.0f;

		private const float foldoutHeight = 20.0f;
		private const float warningHeight = 30.0f;

		// Instance variables, to allow variable size between properties
		private Vector2 origin;
		private float propertySize;
		private float xpos;
		private float ypos;
		private float maxWidth;

		// Foldouts for the foldout fields
		private bool foldoutDictionary;
		private bool[] foldouts;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			origin = new Vector2(position.x, position.y);

			propertySize = 0;

			xpos     = origin.x;
			ypos     = origin.y;
			maxWidth = position.width;

			DrawDictionary(property, property.name);

			propertySize += paddingAtEndOfProperty;

			EditorGUI.EndProperty();
		}

		private void DrawDictionary(SerializedProperty property, string dictionaryName)
		{
			if (IsFoldOut(ref foldoutDictionary, $"{dictionaryName}"))
			{
				SerializedProperty list = property.FindPropertyRelative("serializedDictionary");
				
				List<string> pairLabels = GetLabels(list, "key");
				int actualCount = pairLabels.Distinct().Count(); 
				bool conflicts = actualCount != pairLabels.Count;
				
				if (conflicts)
				{
					DrawWarning(actualCount);
				}
				
				DrawSizeField(list);
				ResizeFoldouts(list);

				ypos += spacingDictionaryToPairs;

				DrawKeyValueArray(list, "key", "value", DrawPair);

				// Size = Y pos of end - Y pos of beginning - spacing at end of last pair
				propertySize = ypos - origin.y - spacingBetweenPairs;
			}
		}
		
		private void DrawWarning(int actualCount)
		{
			Rect rect = new Rect(xpos, ypos, maxWidth - xpos, warningHeight);
			EditorGUI.HelpBox(rect, "Duplicate keys found! These will be removed after serialization.", MessageType.Warning);

			ypos += warningHeight;
			
			rect.y      = ypos;
			rect.height = 20.0f;

			EditorGUI.LabelField(rect, $"Actual Size: {actualCount}");
			
			ypos += 20.0f;
			ypos += spacingWarningToDictionary;
		}

		private void ResizeFoldouts(SerializedProperty list)
		{
			if (foldouts == null)
			{
				foldouts = new bool[list.arraySize];
				return;
			}

			if (foldouts.Length != list.arraySize)
			{
				List<bool> temp = foldouts.ToList();
				temp.ResizeList(list.arraySize);
				foldouts = temp.ToArray();
			}
		}

		private void DrawSizeField(SerializedProperty list)
		{
			Rect sizeRect = new Rect(xpos + pairIndent, ypos, maxWidth - xpos, foldoutHeight);
			list.arraySize = Mathf.Clamp(EditorGUI.IntField(sizeRect, new GUIContent("Size"), list.arraySize), 0, int.MaxValue);

			ypos += foldoutHeight;
		}

		private void DrawPair(int index, SerializedProperty key, SerializedProperty value)
		{
			if (IsFoldOut(ref foldouts[index], GetPairLabel(key, index).ReplaceUnderscoreWithSpace()))
			{
				ypos += spacingLabelToPair;
				DrawVariable(key, new GUIContent($"Key [{key.type}]"));

				ypos += spacingBetweenPairValues;
				DrawVariable(value, new GUIContent($"Value [{value.type}]"));
			}

			ypos += spacingBetweenPairs;
		}

		private void DrawVariable(SerializedProperty value, GUIContent label)
		{
			float xPosition = xpos + pairIndent;
			float height = EditorGUI.GetPropertyHeight(value, true);

			Rect rect = new Rect(xPosition, ypos, maxWidth - xPosition, height);
			EditorGUI.PropertyField(rect, value, label, true);

			ypos += height;
		}

		/// <summary>
		/// Creates a foldout label
		/// </summary>
		private bool IsFoldOut(ref bool foldout, string label = "")
		{
			Rect rect = new Rect(xpos, ypos, maxWidth - xpos, foldoutHeight);
			foldout = EditorGUI.Foldout(rect, foldout, label);

			ypos += foldoutHeight;

			return foldout;
		}

		private static List<string> GetLabels(SerializedProperty list, string keyName)
		{
			List<string> labels = new List<string>();
			
			for (int i = 0; i < list.arraySize; i++)
			{
				SerializedProperty @struct = list.GetArrayElementAtIndex(i);
				SerializedProperty key = @struct.FindPropertyRelative(keyName);
				
				labels.Add(GetPairLabel(key, i));
			}

			return labels;
		}

		private static string GetPairLabel(SerializedProperty key, int index)
		{
			string defaultText = $"Pair {index}";

			switch (key.propertyType)
			{
				case SerializedPropertyType.Generic:
					return defaultText;
				case SerializedPropertyType.Integer:
					return key.intValue.ToString();
				case SerializedPropertyType.Boolean:
					return key.boolValue.ToString();
				case SerializedPropertyType.Float:
					return key.floatValue.ToString(CultureInfo.InvariantCulture);
				case SerializedPropertyType.String:
					string stringValue = key.stringValue;
					
					return string.IsNullOrEmpty(stringValue) ? nameof(string.Empty) : key.stringValue;
				case SerializedPropertyType.Color:
					return key.colorValue.ToString();
				case SerializedPropertyType.ObjectReference:
					return key.objectReferenceValue.name;
				case SerializedPropertyType.LayerMask:
					return defaultText;
				case SerializedPropertyType.Enum:
					string[] enumNames = key.enumNames;

					if (key.enumValueIndex < 0 || key.enumValueIndex > enumNames.Length)
					{
						return key.intValue.ToString();
					}

					return key.enumNames[key.enumValueIndex];
				case SerializedPropertyType.Vector2:
					return key.vector2Value.ToString();
				case SerializedPropertyType.Vector3:
					return key.vector3Value.ToString();
				case SerializedPropertyType.Vector4:
					return key.vector4Value.ToString();
				case SerializedPropertyType.Rect:
					return key.rectValue.ToString();
				case SerializedPropertyType.ArraySize:
					return key.intValue.ToString();
				case SerializedPropertyType.Character:
					return key.stringValue; //TODO: TEST
				case SerializedPropertyType.AnimationCurve:
					return key.animationCurveValue.ToString();
				case SerializedPropertyType.Bounds:
					return key.boundsValue.ToString();
				case SerializedPropertyType.Gradient:
					return key.colorValue.ToString(); //TODO: TEST
				case SerializedPropertyType.Quaternion:
					return key.quaternionValue.ToString();
				case SerializedPropertyType.ExposedReference:
					return key.exposedReferenceValue.name;
				case SerializedPropertyType.FixedBufferSize:
					return key.fixedBufferSize.ToString();
				case SerializedPropertyType.Vector2Int:
					return key.vector2IntValue.ToString();
				case SerializedPropertyType.Vector3Int:
					return key.vector3IntValue.ToString();
				case SerializedPropertyType.RectInt:
					return key.rectIntValue.ToString();
				case SerializedPropertyType.BoundsInt:
					return key.boundsIntValue.ToString();
				case SerializedPropertyType.ManagedReference:
					return key.managedReferenceFullTypename;
				default:
					return defaultText;
			}
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) =>
			base.GetPropertyHeight(property, label) + propertySize;
	}
}