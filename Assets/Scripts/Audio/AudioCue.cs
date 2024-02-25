using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{
	[SerializeField] private bool playOnAwake = false;

	//AudioCueKey

	void Start()
	{
		if (playOnAwake) StartCoroutine(PlayDelayed());
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private IEnumerator PlayDelayed()
	{
		// wait one second, until audio manager is ready
		yield return new WaitForSeconds(1);

		//This additional check prevents the AudioCue from playing if the object is disabled or the scene unloaded
		//This prevents playing a looping AudioCue which then would be never stopped
		if (playOnAwake)
			PlayAudioCue();
	}

	public void PlayAudioCue()
	{
		//controlKey = _audioCueEventChannel.RaisePlayEvent(_audioCue, _audioConfiguration, transform.position);
	}

	public void StopAudioCue()
	{
		//if (controlKey != AudioCueKey.Invalid)
		{
			//if (!_audioCueEventChannel.RaiseStopEvent(controlKey))
			{
				//controlKey = AudioCueKey.Invalid;
			}
		}
	}

	public void FinishAudioCue()
	{
		//if (controlKey != AudioCueKey.Invalid)
		{
			//if (!_audioCueEventChannel.RaiseFinishEvent(controlKey))
			{
				//controlKey = AudioCueKey.Invalid;
			}
		}
	}
}
