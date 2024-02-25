using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Audio/Audio Configuration")]

public class AudioConfiguration : ScriptableObject
{
	// enumeration for audio priority levels with explicit integer values
	private enum PriorityLevel
	{
		Highest = 0,
		High = 64,
		Standard = 128,
		Low = 194,
		VeryLow = 256,
	}

	// reference to an AudioMixerGroup to route the audio output through
	[SerializeField] private AudioMixerGroup outputAudioMixerGroup = null;
	[SerializeField] private PriorityLevel priority = PriorityLevel.Standard;
		
	[Header("Properties")]
	[SerializeField] private bool mute = false;
	[SerializeField][Range(0, 1)] private float volume = 1;
	[SerializeField][Range(-3, 3)] private float pitch = 1;
	[SerializeField][Range(-1, 1)] private float panStereo = 0f;
	[SerializeField][Range(0, 1.1f)] private float reverbZoneMix = 1;

	[Header("Spatialization")]
	[SerializeField][Range(0, 1)] private float spatialBlend = 1; // Controls the blend between 2D and 3D audio (0 to 1).
	[SerializeField] private AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic; // Sets the rolloff mode for 3D sound.
	[SerializeField][Range(0.01f, 5)] private float minDistance = 0.1f; // Minimum distance for 3D audio effect.
	[SerializeField][Range(5, 100)] private float maxDistance = 50; // Maximum distance for 3D audio effect.
	[SerializeField][Range(0, 360)] private int spread = 0; // Stereo spread angle for 3D sound (0 to 360 degrees).
	[SerializeField][Range(0, 5)] private float dopplerLevel = 1; // Controls the level of the Doppler effect (0 to 5).

	[Header("Ignores")]
	[SerializeField] private bool bypassEffects = false; // Bypasses all audio effects when true.
	[SerializeField] private bool bypassListenerEffects = false; // Bypasses effects applied by the audio listener.
	[SerializeField] private bool bypassReverbZones = false; // Bypasses reverb zones.
	[SerializeField] private bool ignoreListenerVolume = false; // Ignores global volume setting of the audio listener.
	[SerializeField] private bool ignoreListenerPause = false; // Ignores pause state of the audio listener.

	// apply this configuration to a given AudioSource component
	public void ApplyTo(AudioSource audioSource)
	{
		audioSource.outputAudioMixerGroup = outputAudioMixerGroup;
		audioSource.mute = mute;
		audioSource.bypassEffects = bypassEffects;
		audioSource.bypassListenerEffects = bypassListenerEffects;
		audioSource.bypassReverbZones = bypassReverbZones;
		audioSource.priority = (int)priority;
		audioSource.volume = volume;
		audioSource.pitch = pitch;
		audioSource.panStereo = panStereo;
		audioSource.spatialBlend = spatialBlend;
		audioSource.reverbZoneMix = reverbZoneMix;
		audioSource.dopplerLevel = dopplerLevel;
		audioSource.spread = spread;
		audioSource.rolloffMode = rolloffMode;
		audioSource.minDistance = minDistance;
		audioSource.maxDistance = maxDistance;
		audioSource.ignoreListenerVolume = ignoreListenerVolume;
		audioSource.ignoreListenerPause = ignoreListenerPause;
	}
}

