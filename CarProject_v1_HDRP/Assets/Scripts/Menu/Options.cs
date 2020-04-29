using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Options : MonoBehaviour
{

    public AudioMixer audioMixerMusic;
    public AudioMixer audioMixerSFX;

    public void VolumeMusicSetter(float volumeMusic)
    {
        audioMixerMusic.SetFloat("volumeMusic", volumeMusic);

    }

    public void VolumeSFXSetter(float volumeSFX)
    {
        audioMixerSFX.SetFloat("volumeSFX", volumeSFX);

    }
}
