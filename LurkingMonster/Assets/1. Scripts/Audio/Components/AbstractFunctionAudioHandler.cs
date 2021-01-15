using Utility;

namespace Audio.Components
{
	public abstract class AbstractFunctionAudioHandler : AbstractFunctionHandler
	{
		protected override void OnEnable()
		{
			if (AudioManager.IsInitialized)
			{
				base.OnEnable();
			}
			else
			{
				Invoke(nameof(OnEnable), 0.1f);
			}
		}

		protected override void OnDisable()
		{
			if (AudioManager.IsInitialized)
			{
				base.OnDisable();
			}
		}

		protected override void OnDestroy()
		{
			if (AudioManager.IsInitialized)
			{
				base.OnDestroy();
			}
		}
	}
}