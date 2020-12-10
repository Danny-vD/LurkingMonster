using System;
using UnityEngine;
using VDFramework;

namespace UI.Market
{
	public class LockEnabler : BetterMonoBehaviour
	{
		[SerializeField]
		private GameObject @lock;

		private void Awake()
		{
			if (@lock == null)
			{
				@lock = CachedTransform.Find("Lock").gameObject;
			}

			// Throw exception if lock is still null
			if (@lock == null)
			{
				Exception exception = new NullReferenceException("Lock is not assigned in the inspector");
				Debug.LogException(exception, this);
			}
		}

		public void SetLocked(bool locked)
		{
			if (@lock == null)
			{
				return;
			}
			
			@lock.SetActive(locked);
		}
	}
}