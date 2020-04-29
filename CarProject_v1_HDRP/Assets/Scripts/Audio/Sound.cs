using UnityEngine.Audio;
using UnityEngine;

//pokud vytvarime novo ne monobehaviour class a chceme aby se oběvila v inspektoru
[System.Serializable]
public class sound {
    [HideInInspector]
    public AudioSource source;

 public AudioClip clip;
    public string name;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    
}