using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	[System.Serializable]
	struct AudioParameter
	{
		public string name;
		public FloatVariable value;
	}

	[Header("Audio Mixer")]
	[SerializeField] private AudioMixer audioMixer;
	[SerializeField] private Event audioUpdateEvent;
	[SerializeField] private AudioParameter[] parameters;

	private void OnEnable()
	{
		audioUpdateEvent.Subscribe(OnUpdateAudio);
	}

	void OnUpdateAudio()
	{
		foreach (var parameter in parameters)
		{
			SetGroupVolume(parameter.name, parameter.value);
		}
	}

	public void SetGroupVolume(string parameterName, float linearVolume)
	{
		bool volumeSet = audioMixer.SetFloat(parameterName, LinearToDB(linearVolume));
		if (!volumeSet)
		{
			Debug.LogError($"The AudioMixer parameter {parameterName} was not found");
		}
	}

	public float GetGroupVolume(string parameterName)
	{
		if (audioMixer.GetFloat(parameterName, out float db))
		{
			return DBToLinear(db);
		}
		else
		{
			Debug.LogError($"The AudioMixer parameter {parameterName} was not found");
			return 0f;
		}
	}

	public static float LinearToDB(float linear)
	{
		return (linear != 0) ? 20.0f * Mathf.Log10(linear) : -144.0f;
	}

	public static float DBToLinear(float dB)
	{
		return Mathf.Pow(10.0f, dB / 20.0f);
	}
}
