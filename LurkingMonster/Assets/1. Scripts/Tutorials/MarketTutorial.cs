using Events;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.EventSystem;

namespace Tutorials
{
	public class MarketTutorial : Tutorial
	{
		[SerializeField]
		private Material material;

		[SerializeField]
		private float scale;
		
		[SerializeField]
		private float offsetDistance = 1.5f;
		
		private Material oldMaterial;

		private Renderer meshRenderer;

		private GameObject prefabInstance;
		
		public override void StartTutorial(GameObject narrator, GameObject arrow)
		{
			base.StartTutorial(narrator, arrow);

			Building building = FindObjectOfType<Building>();
			AbstractBuildingTile abstractBuildingTile = building.GetComponentInParent<AbstractBuildingTile>();
			meshRenderer = abstractBuildingTile.GetComponent<Renderer>();

			Canvas buildingCanvas = building.GetComponentInChildren<Canvas>();
			
			oldMaterial = meshRenderer.sharedMaterial;

			prefabInstance              = Instantiate(arrow, buildingCanvas.transform);
			prefabInstance.transform.localScale = new Vector3(scale, scale, scale);
			prefabInstance.transform.Translate(Vector3.up * offsetDistance, Space.Self);
			meshRenderer.sharedMaterial = material;

			OpenMarketEvent.ParameterlessListeners += OnMarketScreen;
		}

		private void CompletedTutorial()
		{
			meshRenderer.sharedMaterial = oldMaterial;
			Destroy(prefabInstance);
			TutorialManager.Instance.CompletedTutorial();
		}

		private void OnMarketScreen()
		{
			OpenMarketEvent.ParameterlessListeners -= OnMarketScreen;
			CompletedTutorial();
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			OpenMarketEvent.ParameterlessListeners -= ShowNextText;
		}
	}
}