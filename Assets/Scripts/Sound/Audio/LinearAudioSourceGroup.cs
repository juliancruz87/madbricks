using UnityEngine;

public class LinearAudioSourceGroup : AudioSourceGroup {

    public int CurrentIndex = 0;

    private Transform _myTransform;

    private bool _inited;

    protected override void Init() {
        //TODO: Fix this :(
        _inited = true;
        _myTransform = transform;
        InitDistanceTriggers();
    }

    private void InitDistanceTriggers() {/*
        DistanceTrigger[] triggers = FindObjectsOfType<DistanceTrigger>();
        
        foreach (DistanceTrigger trigger in triggers) 
            if (trigger.TargetTransform == _myTransform)
                trigger.OnTriggerEnter = PlayCurrentAudio;*/
    }

    private void Update() {
        if (!_inited)
            Init();
    }

    public void PlayCurrentAudio() {
        AudioSources[CurrentIndex].Play();
        IncrementIndex();
    }

    private void IncrementIndex() {
        CurrentIndex++;
        if (CurrentIndex >= AudioSources.Length)
            CurrentIndex = 0;
    }
}