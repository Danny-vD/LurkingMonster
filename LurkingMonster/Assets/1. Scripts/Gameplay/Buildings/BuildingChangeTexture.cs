using Enums;
using UnityEngine;
using Utility;
using VDFramework;

namespace Gameplay.Buildings
{
	public class BuildingChangeTexture : BetterMonoBehaviour
	{
		[SerializeField]
		private SerializableEnumDictionary<BuildingType, SerializableDictionary<int, Material>> crackedMaterials;

		private Material defaultMaterial;

		private Renderer meshRenderer;

		private void Start()
		{
			meshRenderer = GetComponent<Renderer>();

			CacheMaterial();

			//TODO: do something else than change texture for foundation and soil?
			GetComponent<BuildingHealth>().OnBuildingRepair   += ResetTextureBuilding;
			GetComponent<BuildingHealth>().OnFoundationRepair += ResetTextureBuilding;
			GetComponent<BuildingHealth>().OnSoilRepair       += ResetTextureBuilding;
			GetComponent<BuildingUpgrade>().OnUpgrade         += CacheMaterial;
		}

		/// <summary>
		/// Function that changes the building texture
		/// </summary>
		public void ChangeTexture(Building building)
		{
			SetTextures(building.BuildingType, building.CurrentTier);
		}

		private void SetTextures(BuildingType buildingType, int tier)
		{
			meshRenderer.sharedMaterial = crackedMaterials[buildingType][tier];
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