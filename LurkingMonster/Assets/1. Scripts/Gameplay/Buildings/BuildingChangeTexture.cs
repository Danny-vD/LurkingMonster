using System;
using Enums;
using Structs.Buildings;
using UnityEngine;
using VDFramework;

namespace Gameplay.Buildings
{
	public class BuildingChangeTexture : BetterMonoBehaviour
	{
		[SerializeField]
		private Material material;
		
		private Material defaultMaterial;

		private Renderer meshRenderer;

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
					SetTextures();
					break;
				case BuildingType.ApartmentBuilding:
					break;
				case BuildingType.Store:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void SetTextures()
		{
			meshRenderer.sharedMaterial = material;
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