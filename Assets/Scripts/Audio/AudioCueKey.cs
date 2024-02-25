using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AudioCueKey
{
	public static AudioCueKey Invalid = new AudioCueKey(-1, null);

	internal int Value;
	internal AudioCue AudioCue;

	internal AudioCueKey(int value, AudioCue audioCue)
	{
		Value = value;
		AudioCue = audioCue;
	}

	public override bool Equals(object obj)
	{
		if (!(obj is AudioCueKey)) { return false; }

		var audioCueKey = (AudioCueKey)obj;
		return audioCueKey.AudioCue == this.AudioCue && audioCueKey.Value == this.Value;
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode() ^ AudioCue.GetHashCode();
	}

	public static bool operator == (AudioCueKey x, AudioCueKey y)
	{
		return x.Value == y.Value && x.AudioCue == y.AudioCue;
	}

	public static bool operator != (AudioCueKey x, AudioCueKey y)
	{
		return !(x == y);
	}
}