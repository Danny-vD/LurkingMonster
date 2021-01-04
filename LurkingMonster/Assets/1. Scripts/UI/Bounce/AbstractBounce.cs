using System.Collections;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Bounce
{
	public abstract class AbstractBounce<T> : BetterMonoBehaviour
		where T : VDEvent
	{
		[SerializeField]
		private float speed;

		private Vector3 targetPosition;
		private Vector3 oldPosition;

		private float time;
		protected bool IsRunning;

		protected virtual void Start()
		{
			EventManager.Instance.AddListener<T>(StartBounce);
		}

		protected void StartBounce()
		{
			if (IsRunning)
			{
				StopAllCoroutines();
				transform.position = oldPosition;
			}
			
			if (gameObject.activeInHierarchy)
			{
				StartCoroutine(Bounce());
			}
		}

		private IEnumerator Bounce()
		{
			IsRunning      = true;
			oldPosition    = transform.position;
			targetPosition = transform.position + Vector3.up * 5;
			
			while (time < 1)
			{
				time               += Time.deltaTime / speed;
				transform.position =  Vector3.Lerp(oldPosition, targetPosition, time);
				yield return null;
			}

			//yield return new WaitForSeconds(0.1f);

			while (time > 0)
			{
				time               -= Time.deltaTime / speed;
				transform.position =  Vector3.Lerp(oldPosition, targetPosition, time);
				yield return null;
			}

			IsRunning = false;
		}
	}
}