using Enums;
using UnityEngine;
using VDFramework;

namespace Utility
{
	public abstract class AbstractFunctionHandler : BetterMonoBehaviour
	{
		[SerializeField]
		private UnityFunction unityFunction;

		protected virtual void OnEnable()
		{
			if ((unityFunction & UnityFunction.OnEnable) == 0)
			{
				return;
			}

			ReactToEvent(UnityFunction.OnEnable);
		}

		protected virtual void Start()
		{
			if ((unityFunction & UnityFunction.Start) == 0)
			{
				return;
			}

			ReactToEvent(UnityFunction.Start);
		}

		protected virtual void OnDisable()
		{
			if ((unityFunction & UnityFunction.OnDisable) == 0)
			{
				return;
			}

			ReactToEvent(UnityFunction.OnDisable);
		}

		protected virtual void OnDestroy()
		{
			if ((unityFunction & UnityFunction.OnDestroy) == 0)
			{
				return;
			}

			ReactToEvent(UnityFunction.OnDestroy);
		}

		protected abstract void ReactToEvent(UnityFunction unityFunction);
	}
}