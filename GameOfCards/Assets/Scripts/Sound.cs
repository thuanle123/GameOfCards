using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // The Sound class
    // name represents the name of the file
    // clip is the sound clip itself
    // volume and pitch represents volume and pitch
    // loop to loop the song over and over
    // Hide Inspector because we don't want to show the source music to people
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
