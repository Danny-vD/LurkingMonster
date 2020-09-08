using System.Collections.Generic;
using Events.Temporary;
using UnityEngine;
using VDFramework.EventSystem;
using VDFramework.Extensions;
using VDFramework.Singleton;

namespace Temporary.Singletons
{
	public class CollectableManager : Singleton<CollectableManager>
	{
		[SerializeField]
	 	private int collectableAmount = 1;

		[SerializeField]
		private int winAmount = 1;
		
		private readonly List<CollectableSpawner> spawners = new List<CollectableSpawner>();

		private int actualCollectableCount;
		
		public int PickedUpCount { get; private set; }
		public int WinAmount => winAmount;

		private void Start()
		{
			// Make sure our amoungToSpawn and winAmount don't exceed our actual collectable count
			actualCollectableCount = Mathf.Min(Mathf.Abs(collectableAmount), spawners.Count);
			winAmount = Mathf.Min(Mathf.Abs(winAmount), actualCollectableCount);

			spawners.Randomize();
			
			for (int i = 0; i < actualCollectableCount; i++)
			{
				spawners[i].Spawn();
			}
			
			spawners.ForEach(x => Destroy(x.gameObject));
			spawners.Clear();
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

		public void RegisterSpawner(CollectableSpawner spawner)
		{
			if (spawners.Contains(spawner))
			{
				return;
			}
			
			spawners.Add(spawner);
		}

		private void OnPickedupCollectable()
		{
			++PickedUpCount;

			if (PickedUpCount >= winAmount)
			{
				EventManager.Instance.RaiseEvent(new WinGameEvent());
			}
		}
	}
}