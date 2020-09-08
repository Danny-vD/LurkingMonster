using Events.Temporary;
using Temporary.Singletons;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.Utility;

namespace UI.Temporary
{
	[RequireComponent(typeof(Text))]
	public class CollectableCounter : BetterMonoBehaviour
	{
		private Text counter;
		
		// The variable writer will fill in the variables for {0}, {1}, {2} etc....
		private StringVariableWriter variableWriter;
		
		private void Awake()
		{
			counter = GetComponent<Text>();
			variableWriter = new StringVariableWriter(counter.text);
			
			// Start the counter at 0
			counter.text = variableWriter.UpdateText(0, CollectableManager.Instance.WinAmount);
		}

		private void OnEnable()
		{
			AddListeners();
		}

		private void OnDisable()
		{
			RemoveListeners();
		}
		
		private void AddListeners()
		{
			EventManager.Instance.AddListener<PickupCollectableEvent>(OnPickedupCollectable);
		}
		
		private void RemoveListeners()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
    
			EventManager.Instance.RemoveListener<PickupCollectableEvent>(OnPickedupCollectable);
		}

		private void OnPickedupCollectable()
		{
			counter.text = variableWriter.UpdateText(CollectableManager.Instance.PickedUpCount, CollectableManager.Instance.WinAmount);
		}
	}
}