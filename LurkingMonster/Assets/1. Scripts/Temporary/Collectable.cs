using System;
using Events.Temporary;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Temporary
{
	public class Collectable : BetterMonoBehaviour
	{
		private void Collect()
		{
			EventManager.Instance.RaiseEvent(new PickupCollectableEvent(this));
			Destroy(gameObject);
		}
		
		// Parameter is mandatory (in OnCollisionEnter it can be left out) 
		private void OnTriggerEnter(Collider other)
		{
			Collect();
		}
	}
}