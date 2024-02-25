using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioEmitter : MonoBehaviour
{
	private AudioSource audioSource;

	public event UnityAction<AudioEmitter> OnSoundFinishedPlaying;

	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		audioSource.playOnAwake = false;
	}

	public void PlayAudioClip(AudioClip clip, AudioConfiguration settings, bool loop, Vector3 position = default)
	{
		audioSource.clip = clip;
		settings.ApplyTo(audioSource);

		audioSource.transform.position = position;
		audioSource.loop = loop;
		audioSource.time = 0; //Reset in case this AudioSource is being reused for a short SFX after being used for a long music track
		audioSource.Play();

		if (!loop)
		{
			StartCoroutine(FinishedPlaying(clip.length));
		}
	}

	public AudioClip GetClip()
	{
		return audioSource.clip;
	}


	public void Resume()
	{
		audioSource.Play();
	}

	public void Pause()
	{
		audioSource.Pause();
	}

	public void Stop()
	{
		audioSource.Stop();
	}

	public void Finish()
	{
		if (audioSource.loop)
		{
			audioSource.loop = false;
			float timeRemaining = audioSource.clip.length - audioSource.time;
			StartCoroutine(FinishedPlaying(timeRemaining));
		}
	}

	public bool IsPlaying()
	{
		return audioSource.isPlaying;
	}

	public bool IsLooping()
	{
		return audioSource.loop;
	}

	IEnumerator FinishedPlaying(float clipLength)
	{
		yield return new WaitForSeconds(clipLength);

		NotifyBeingDone();
	}

	private void NotifyBeingDone()
	{
		OnSoundFinishedPlaying?.Invoke(this);
	}
}
