using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMasterMono : MonoBehaviour
{
    [SerializeField] GameObject mute = null;
    [SerializeField] GameObject unmute = null;

    private void Start()
    {
        AudioMaster.Reset();
        if (AudioMaster.GetVol() == 0)
        {
            mute.gameObject.SetActive(true);
            unmute.gameObject.SetActive(false);
        }
    }

    public void Mute()
    {
        AudioMaster.AudioLevel(0);
    }

    public void Unmute()
    {
        AudioMaster.AudioLevel(1);
    }
}
