using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioCue", menuName = "Audio/Audio Cue")]
public class AudioCueData : ScriptableObject
{
	public bool looping = false;
	[SerializeField] private AudioClipsGroup[] audioClipGroups = default;

	public AudioClip[] GetClips()
	{
		AudioClip[] resultingClips = new AudioClip[audioClipGroups.Length];

		for (int i = 0; i < audioClipGroups.Length; i++)
		{
			resultingClips[i] = audioClipGroups[i].GetNextClip();
		}

		return resultingClips;
	}
}

[Serializable]
public class AudioClipsGroup
{
	public SequenceMode sequenceMode = SequenceMode.RandomNoImmediateRepeat;
	public AudioClip[] audioClips;

	private int _nextClipToPlay = -1;
	private int _lastClipPlayed = -1;

	public AudioClip GetNextClip()
	{
		// Fast out if there is only one clip to play
		if (audioClips.Length == 1)
			return audioClips[0];

		if (_nextClipToPlay == -1)
		{
			// Index needs to be initialised: 0 if Sequential, random if otherwise
			_nextClipToPlay = (sequenceMode == SequenceMode.Sequential) ? 0 : UnityEngine.Random.Range(0, audioClips.Length);
		}
		else
		{
			// Select next clip index based on the appropriate SequenceMode
			switch (sequenceMode)
			{
				case SequenceMode.Random:
					_nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
					break;

				case SequenceMode.RandomNoImmediateRepeat:
					do
					{
						_nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
					} while (_nextClipToPlay == _lastClipPlayed);
					break;

				case SequenceMode.Sequential:
					_nextClipToPlay = (int)Mathf.Repeat(++_nextClipToPlay, audioClips.Length);
					break;
			}
		}

		_lastClipPlayed = _nextClipToPlay;

		return audioClips[_nextClipToPlay];
	}

	public enum SequenceMode
	{
		Random,
		RandomNoImmediateRepeat,
		Sequential,
	}
}

