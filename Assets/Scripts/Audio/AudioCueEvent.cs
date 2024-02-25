using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/AudioCue Event")]
public class AudioCueEvent : ScriptableObjectBase
{
	public AudioCuePlayAction OnAudioCuePlayRequested;
	public AudioCueStopAction OnAudioCueStopRequested;
	public AudioCueFinishAction OnAudioCueFinishRequested;

	public AudioCueKey RaisePlayEvent(AudioCueData audioCueData, AudioConfiguration audioConfiguration, Vector3 position = default)
	{
		AudioCueKey audioCueKey = AudioCueKey.Invalid;

		if (OnAudioCuePlayRequested != null)
		{
			audioCueKey = OnAudioCuePlayRequested.Invoke(audioCueData, audioConfiguration, position);
		}

		return audioCueKey;
	}

	public bool RaiseStopEvent(AudioCueKey audioCueKey)
	{
		bool requestSucceed = false;

		if (OnAudioCueStopRequested != null)
		{
			requestSucceed = OnAudioCueStopRequested.Invoke(audioCueKey);
		}

		return requestSucceed;
	}

	public bool RaiseFinishEvent(AudioCueKey audioCueKey)
	{
		bool requestSucceed = false;

		if (OnAudioCueStopRequested != null)
		{
			requestSucceed = OnAudioCueFinishRequested.Invoke(audioCueKey);
		}

		return requestSucceed;
	}
}

public delegate AudioCueKey AudioCuePlayAction(AudioCueData audioCue, AudioConfiguration audioConfiguration, Vector3 position);
public delegate bool AudioCueStopAction(AudioCueKey emitterKey);
public delegate bool AudioCueFinishAction(AudioCueKey emitterKey);
