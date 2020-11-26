using System;
using UnityEngine;

namespace Structs.Buildings
{
	[Serializable]
	public struct BuildingTexture
	{
		[SerializeField]
		private Texture albedo;
		
		[SerializeField]
		private Texture metalic;
		
		[SerializeField]
		private Texture normal;

		public Texture Albedo
		{
			get => albedo;
			set => albedo = value;
		}

		public Texture Metalic
		{
			get => metalic;
			set => metalic = value;
		}

		public Texture Normal
		{
			get => normal;
			set => normal = value;
		}
	}
}