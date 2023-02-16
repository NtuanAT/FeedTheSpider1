using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = this.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop= s.loop;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        try
        {
            foreach (Sound s in sounds)
            {
                if (s.name.Equals(name))
                {
                    s.source.Play();
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Sound name: " + name + " not found!");
        }

        
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, s => s.name.Equals(name));
        if (s != null)
        {
            s.source.Stop();
        }
        else
        {
            Debug.LogWarning("Sound name: " + name + " not found!");
        }
    }
}
