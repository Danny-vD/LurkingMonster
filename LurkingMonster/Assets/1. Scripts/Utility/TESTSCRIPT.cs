using System.Linq;
using Enums;
using UnityEngine;
using VDFramework;

namespace Utility
{
	public class TESTSCRIPT : BetterMonoBehaviour
	{
		[SerializeField]
		private SerializableDictionary<SoilType, string> dictionary;

		[ContextMenu("Test")]
		private void Test()
		{
			dictionary = dictionary.Distinct().ToList();
		}
	}
}