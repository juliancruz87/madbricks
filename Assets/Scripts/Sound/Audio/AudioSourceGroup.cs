using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioSourceGroup : MonoBehaviour {

    public bool AllowClipping;

    public AudioSource[] AudioSources;

    private void Start() {
        AudioSources = GetComponentsInChildren<AudioSource>();

    }

    protected virtual void Init() {
        
    }

    public void PlayRandomAudioSource() {
        AudioSource audioSource = GetRandomAudioSource();
        PlayAudioSource(audioSource);
    }

    private AudioSource GetRandomAudioSource() {
        int randomIndex = Random.Range(0, (AudioSources.Length));
        Debug.Log("Random index = " + randomIndex + " total items = " + AudioSources.Length);
        return AudioSources[randomIndex];
    }

    public void PlayAudioSource(String audioSourceName) {
        AudioSource audioSource = FindAudioSourceByName(audioSourceName);
        PlayAudioSource(audioSource);
    }

    private AudioSource FindAudioSourceByName(string audioSourceName) {
        foreach (AudioSource audioSource in AudioSources) 
            if (audioSource.gameObject.name == audioSourceName)
                return audioSource;

        return null;
    }

    public void PlayAudioSource(AudioSource audioSource) {
        if (AudioSources.Contains(audioSource)) {
            if (AllowClipping)
                audioSource.Play();
            else if (AudiosBeingPlayed() == 0)
                audioSource.Play();
        }
    }

    private int AudiosBeingPlayed() {
        int audiosBeingPlayed = 0;
        foreach (AudioSource audioSource in AudioSources)
            if (audioSource.isPlaying)
                audiosBeingPlayed++;
        
        return audiosBeingPlayed;
    }
}