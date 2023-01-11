using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {

    public string audioName;
    public int audioID;
    public AudioClip audioClip;
    public AudioMixerGroup mixerGroup;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 1f)]
    public float pitch;

    public bool isLoop;

    [HideInInspector]
    public AudioSource audioSource;

}