using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    [HideInInspector]
    public AudioSource source;

    public AudioMixerGroup outPut;
    public string name;
    public AudioClip clip;
    [Range(0f,1.0f)]
    public float volume;
    [Range(-3.0f,3.0f)]
    public float pitch;
    public bool loop;
}
