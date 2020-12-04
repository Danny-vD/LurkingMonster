using Enums;
using UnityEngine;
using VDFramework;

namespace Utility
{
	public class TESTSCRIPT : BetterMonoBehaviour
	{
		[SerializeField]
		private SerializableDictionary<int, string> dictionary;

		[SerializeField]
		private SerializableEnumDictionary<SoilType, string> enumDictionary;

		[ContextMenu("Test")]
		private void Test()
		{
			enumDictionary.Populate();
		}
	}
}