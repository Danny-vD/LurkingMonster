using System;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VDFramework.Interfaces;

namespace Structs.Market
{
	[Serializable]
	public struct FoundationButtonData : IKeyValuePair<FoundationType, Button>, IBuyButtonData
	{
		[SerializeField]
		private FoundationType foundationType;

		[SerializeField]
		private Button button;

		[SerializeField]
		private BuyButtonText text;

		public FoundationType Key
		{
			get => foundationType;
			set => foundationType = value;
		}

		public Button Value
		{
			get => button;
			set => button = value;
		}

		public BuyButtonText Text => text;

		public bool Equals(IKeyValuePair<FoundationType, Button> other) => Key.Equals(other.Key);
	}
}