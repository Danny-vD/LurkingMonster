using Events;
using Gameplay.Buildings;
using Grid.Tiles.Buildings;
using UnityEngine;
using UnityEngine.UI;
using VDFramework.EventSystem;

namespace _1._Scripts.Tutorial
{
	public class MarketTutorial : Tutorial
	{
		[SerializeField]
		private Material material;
		
		[SerializeField]
		private Button manageScreen;

		private AbstractBuildingTile abstractBuildingTile;
		
		public override void StartTutorial(GameObject narrator)
		{
			base.StartTutorial(narrator);
			
			Building building = FindObjectOfType<Building>();
			abstractBuildingTile = building.GetComponentInParent<AbstractBuildingTile>();
			abstractBuildingTile.GetComponent<Renderer>().sharedMaterial = material;

			OpenMarketEvent.ParameterlessListeners += OnMarketScreen;
			manageScreen.onClick.AddListener(CompletedTutorial);
		}

		private static void CompletedTutorial()
		{
			TutorialManager.Instance.CompletedTutorial();
		}

		private void OnMarketScreen()
		{
			OpenMarketEvent.ParameterlessListeners -= OnMarketScreen;
			ShowNextText();
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