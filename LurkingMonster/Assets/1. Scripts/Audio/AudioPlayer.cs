using System.Collections.Generic;
using Enums.Audio;
using FMOD.Studio;
using FMODUnity;
using Structs.Audio;
using UnityEngine;
using EventType = Enums.Audio.EventType;

namespace Audio
{
	public static class AudioPlayer
	{
		public static void PlayEmitter(EmitterType emitter)
		{
			AudioManager.Instance.EventPaths.GetEmitter(emitter).Play();
		}

		public static void ToggleEmitter(EmitterType emitterType)
		{
			StudioEventEmitter emitter = AudioManager.Instance.EventPaths.GetEmitter(emitterType);

			if (emitter.IsPlaying())
			{
				emitter.Stop();
				return;
			}

			emitter.Play();
		}

		public static void StopEmitter(EmitterType emitter)
		{
			AudioManager.Instance.EventPaths.GetEmitter(emitter).Stop();
		}

		public static void PlayOneShot(EventType @event, EventParameters parameters)
		{
			EventInstance eventInstance = RuntimeManager.CreateInstance(AudioManager.Instance.EventPaths.GetPath(@event));

			foreach (KeyValuePair<string, float> pair in parameters.ParameterValues)
			{
				eventInstance.setParameterByName(pair.Key, pair.Value);
			}

			eventInstance.start();
			eventInstance.release(); //Release each event instance immediately, they are fire and forget, one-shot instances. 
		}

		public static void PlayOneShotAttached(EventType @event, EventParameters parameters, GameObject gameObject)
		{
			EventInstance eventInstance = RuntimeManager.CreateInstance(AudioManager.Instance.EventPaths.GetPath(@event));
			eventInstance.set3DAttributes(gameObject.transform.To3DAttributes());

			foreach (KeyValuePair<string, float> pair in parameters.ParameterValues)
			{
				eventInstance.setParameterByName(pair.Key, pair.Value);
			}

			eventInstance.start();
			eventInstance.release(); //Release each event instance immediately, they are fire and forget, one-shot instances. 
		}

		public static void PlayOneShotAttached(EventType @event, GameObject location)
		{
			RuntimeManager.PlayOneShotAttached(AudioManager.Instance.EventPaths.GetPath(@event), location);
		}

		public static void PlayOneShot3D(EventType @event)
		{
			RuntimeManager.PlayOneShotAttached(AudioManager.Instance.EventPaths.GetPath(@event), AudioManager.Instance.gameObject);
		}

		public static void PlayOneShot2D(EventType @event)
		{
			RuntimeManager.PlayOneShot(AudioManager.Instance.EventPaths.GetPath(@event));
		}

		/// <summary>
		/// Returns an instance of the specified event with given parameters, you will need to start, stop and release it manually
		/// </summary>
		public static EventInstance GetEventInstance(EventType @event, EventParameters parameters)
		{
			EventInstance eventInstance = RuntimeManager.CreateInstance(AudioManager.Instance.EventPaths.GetPath(@event));

			foreach (KeyValuePair<string, float> pair in parameters.ParameterValues)
			{
				eventInstance.setParameterByName(pair.Key, pair.Value);
			}

			return eventInstance;
		}

		/// <summary>
		/// Returns an instance of the specified event, you will need to start, stop and release it manually
		/// </summary>
		public static EventInstance GetEventInstance(EventType @event)
		{
			return RuntimeManager.CreateInstance(AudioManager.Instance.EventPaths.GetPath(@event));
		}
	}
}