using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioMaster
{
    private static float vol = 1;

    public static float GetVol()
    {
        return vol;
    }

    public static void Reset()
    {
        AudioListener.volume = vol;
    }

    public static void AudioLevel(float audioLevel)
    {
        AudioListener.volume = audioLevel;
        vol = audioLevel;
    }
}

