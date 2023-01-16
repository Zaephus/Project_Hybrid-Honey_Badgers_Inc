using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    
    void Awake() {

        if(instance == null) {
            instance = this;
        }
        else if(instance != this) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        foreach(Sound sound in sounds) {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;

            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.isLoop;
            sound.audioSource.playOnAwake = false;
            sound.audioSource.outputAudioMixerGroup = sound.mixerGroup;
        }

    }

    public void Play(string _name) {
        Sound sound = Array.Find(sounds, sound => sound.audioName == _name);
        if(sound == null) {
            Debug.LogWarning("Sound: " + _name + "  not found!");
            return;
        }
        sound.audioSource.Play(); 
    }

    public void Play(int _id) {
        Sound sound = Array.Find(sounds, sound => sound.audioID == _id);
        if(sound == null) {
            Debug.LogWarning("Sound: " + _id + "  not found!");
            return;
        }
        sound.audioSource.Play(); 
    }

    public void Stop(string _name) {
        Sound sound = Array.Find(sounds, sound => sound.audioName == _name);
        if(sound == null) {
            Debug.LogWarning("Sound: " + _name + "  not found!");
            return;
        }
        sound.audioSource.Stop();
    }

    public void Stop(int _id) {
        Sound sound = Array.Find(sounds, sound => sound.audioID == _id);
        if(sound == null) {
            Debug.LogWarning("Sound: " + _id + "  not found!");
            return;
        }
        sound.audioSource.Stop();
    }

    public void StopAll() {
        foreach(Sound sound in sounds) {
            sound.audioSource.Stop();
        }
    }

}