using UnityEngine;
using System.Collections;

public class AudioClipGroup : MonoBehaviour {

    public float ShowTime;
    public float HideTime;

    public AudioSource[] AudioSources;

    private ArrayList _openInterpolations;
    private ArrayList _closedInterpolations;
    
	// Use this for initialization
    private void Awake() {
        _openInterpolations = new ArrayList();
        _closedInterpolations = new ArrayList();
    }

	private void Start () {
	    AudioSources = GetComponentsInChildren<AudioSource>();
	}
	
	// Update is called once per frame
	private void Update () {
	    RemoveUnusedInterpolations();
	    UpdateCurrentInterpolations();
	    
	}

    private void RemoveUnusedInterpolations() {
        foreach (AudioSourceInterpolation closedInterpolation in _closedInterpolations)
            _openInterpolations.Remove(closedInterpolation);
    }

    private void UpdateCurrentInterpolations() {
        foreach (AudioSourceInterpolation interpolation in _openInterpolations)
            interpolation.Update();
    }
    
    public void ShowAudioSource(int index) {
        AudioSource audioSource = AudioSources[index];
        if (SourcesBeingPlayed() == 0)
            PlayAllAudioSources();
            
        ShowAudioSource(audioSource);
    }

    public void ShowAudioSource(AudioSource audioSource) {
        Debug.Log("ShowAudioSource(AudioSource audioSource)");
        AddInterpolation(audioSource, 0, 1, ShowTime);
    }

    private void PlayAllAudioSources() {
        Debug.Log("PlayAllAudioSources()");
        foreach (AudioSource audioSource in AudioSources) {
            audioSource.volume = 0;
            audioSource.Play();
        }
    }

    public void HideAudioSource(int index) {
        Debug.Log("HideAudioSource(int index)");
        AudioSource audioSource = AudioSources[index];
        AddInterpolation(audioSource, 1, 0, HideTime);
    }

    private void StopAllAudioSources() {
        foreach (AudioSource audioSource in AudioSources)
            audioSource.Stop();
    }

    private int SourcesBeingPlayed() {
        int sourcesBeingPlayed = 0;
        foreach (AudioSource audioSource in AudioSources) 
            if (audioSource.isPlaying)
                sourcesBeingPlayed++;

        return sourcesBeingPlayed;
    }

    private int SourcesMuted() {
        int sourcesMuted = 0;
        foreach (AudioSource audioSource in AudioSources)
            if (audioSource.volume == 0)
                sourcesMuted++;

        return sourcesMuted;
    }

    public void AddInterpolation (AudioSource audioSource, float startVolume, float targetVolume, float totalTime) {
        AudioSourceInterpolation currentInterpolation = InterpolationExist(audioSource);

        if (currentInterpolation != null) 
            _openInterpolations.Remove(currentInterpolation);
           
        AudioSourceInterpolation interpolation = new AudioSourceInterpolation(audioSource, startVolume, targetVolume, totalTime);
        interpolation.OnComplete = OnInterpotalionComplete;
        _openInterpolations.Add(interpolation);

    }

    public AudioSourceInterpolation InterpolationExist(AudioSource audioSource) {
        foreach (AudioSourceInterpolation interpolation in _openInterpolations) 
            if (interpolation.AudioSource == audioSource) 
                return interpolation;

        return null;
    }

    private void OnInterpotalionComplete(AudioSourceInterpolation interpolation) {
        _closedInterpolations.Add(interpolation);
    }
}
