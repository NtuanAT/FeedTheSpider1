using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [HideInInspector]
    public string theme;
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
        string level = SceneManager.GetActiveScene().name;
        switch (level)
        {
            case "Level1":
                this.theme = "Theme1";
                break;
            case "Level2":
                this.theme = "Theme2";
                break;
            case "LevelHell":
                this.theme = "Theme3";
                break;
            default: break;
        }
        Play(theme);
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
        catch (Exception)
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
