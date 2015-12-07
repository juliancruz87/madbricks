using UnityEngine;

public class AudioSourceInterpolation {

    private float _startVolume;
    private float _targetVolume;

    private float _currentTime;
    private float _totalTime;

    private float _alpha;
    private float _range;

    private bool _finished;

    public AudioSource AudioSource { set; get; }

    public OnComplete OnComplete;

    public AudioSourceInterpolation(AudioSource audioSource, 
                                    float startVolume, 
                                    float targetVolume, 
                                    float totalTime) {
        _finished = false;
        AudioSource = audioSource;
        _startVolume = startVolume;
        _targetVolume = targetVolume;
        _range = _targetVolume - _startVolume;
        _totalTime = totalTime;
        SetCurrentAlpha();
        _currentTime = _totalTime * _alpha;
        
    }

    private void SetCurrentAlpha() {
        float actualVolume = AudioSource.volume;
        _alpha = (actualVolume - _startVolume)/_range;
    }

    public void Update() {
        if (!_finished) {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _totalTime)
                _currentTime = _totalTime;

            _alpha = _currentTime/_totalTime;
            AudioSource.volume = _startVolume + (_range*_alpha);

            if (_alpha == 1 && OnComplete != null) {
                OnComplete(this);
                _finished = true;
            }
        }
    }

}

public delegate void OnComplete(AudioSourceInterpolation interpolation);