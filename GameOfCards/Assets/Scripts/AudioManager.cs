using UnityEngine.Audio;
using UnityEngine;
using System;


public class AudioManager : MonoBehaviour {

    // List or Remove Sound as we go
    // Audio Clip, Loop, Volume
    // We need an array of sound so that we can create size
    // instance function so that the AudioManager doesn't create a duplicate

    // TODO
    // Add a slider so the user can change the volume themselves
    public Sound[] sounds;
    public static AudioManager instance;
	// Use this for initialization
	void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // when we go to a new scene the song will not stop/restart
        DontDestroyOnLoad(gameObject);
        // this for loop grab the sounds, volume,pitch
		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}

    // play at the beginning
    void Start()
    {
        Play("Bar");
    }
    // DOn't need this (?)
    
    public void Play (string name)
    { 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.Play();
    }
}
