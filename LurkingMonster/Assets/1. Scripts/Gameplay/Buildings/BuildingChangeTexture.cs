using System;
using Enums;
using Structs.Buildings;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;
using VDFramework;

namespace Gameplay.Buildings
{
	public class BuildingChangeTexture : BetterMonoBehaviour
	{
		[SerializeField]
		private BuildingTexture[] houseTextures;

		private Material defaultMaterial;

		private Renderer meshRenderer;
		
		private static readonly int mainTex = Shader.PropertyToID("_MainTex");
		private static readonly int bumpMap = Shader.PropertyToID("_BumpMap");
		private static readonly int metallicGlossMap = Shader.PropertyToID("_MetallicGlossMap");

		private void Awake()
		{
			meshRenderer                                    =  GetComponent<Renderer>();

			CacheMaterial();
			GetComponent<BuildingHealth>().OnBuildingRepair += ResetTextureBuilding;
			GetComponent<BuildingUpgrade>().OnUpgrade       += CacheMaterial;
		}

		public void ChangeTexture(Building building)
		{
			switch (building.BuildingType)
			{
				case BuildingType.House: 
					SetTextures(houseTextures[building.CurrentTier - 1]);
					break;
				case BuildingType.ApartmentBuilding:
					break;
				case BuildingType.Store:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void SetTextures(BuildingTexture buildingTexture)
		{
			meshRenderer.material.SetTexture(mainTex, buildingTexture.Albedo);
			meshRenderer.material.SetTexture(bumpMap, buildingTexture.Normal);
			meshRenderer.material.SetTexture(metallicGlossMap, buildingTexture.Metalic);
		}
		
		private void ResetTextureBuilding()
		{
			meshRenderer.material = defaultMaterial;
		}

		private void CacheMaterial()
		{
			defaultMaterial = meshRenderer.sharedMaterial;
		}
	}
}