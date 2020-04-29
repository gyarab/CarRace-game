using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{



    // FindObjectOfType<AudioManager>().Play("PlayerDeath");


    public sound[] sounds;

    void Awake()
    {
        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

   
    void Start()
    {

        PlaySound("SongHlavniNabidka");

    }

    public void PlaySound(string name)
    {
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();







    }
}