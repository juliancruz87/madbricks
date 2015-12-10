using Drag;
using UnityEngine;

namespace Sound {
    [RequireComponent (typeof (AudioSource))]
    public class DragSoundPlayer : MonoBehaviour {
        private AudioSource audioSource;
        
        private void Awake() {
            audioSource = GetComponent<AudioSource>();
            InitDraggEvents();
        }

        protected virtual void InitDraggEvents() {
            DraggableObject draggableObject = GetComponent<DraggableObject>();
            draggableObject.OnObjectDragged += PlayDragSound;
            draggableObject.OnObjectStopDrag += StopDrag;
            draggableObject.OnSnap += PlayDragSoundOnce;

        }

        private void PlayDragSound(Vector3 currentposition, Vector3 newposition) {
            if (!audioSource.isPlaying) {
                audioSource.loop = true;
                audioSource.Play();
            }
                
            if (currentposition == newposition)
                audioSource.loop = false;
            else
                audioSource.loop = true;
                
            
        }

        private void StopDrag() {
            audioSource.Stop();
        }

        private void PlayDragSoundOnce() {
            audioSource.loop = false;
            audioSource.Play();
        }
    }
}