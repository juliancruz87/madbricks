using System;
using Drag;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound {
    public class SnapSoundPlayer : MonoBehaviour {
        [SerializeField]
        private bool playAtRandomOrder;

        [SerializeField] 
        private int lastIndexPlayed = -1;

        [SerializeField]
        private int currentIndexPlayed = -1;

        [SerializeField]
        private AudioClip[] audioClips;

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private DraggableObject[] draggableObjects;

        private void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update() {
            if (draggableObjects == null || 
                draggableObjects.Length == 0) {
                draggableObjects = FindObjectsOfType<DraggableObject>();
                if (draggableObjects != null)
                    InitDraggableObjects();
            }
        }

        private void InitDraggableObjects() {
            foreach (DraggableObject draggableObject in draggableObjects) {
                draggableObject.OnNodeUpdated += PlaySnapSound;
            }
        }

        private void PlaySnapSound() {
            int nextIndex = GetNextIndex();
            lastIndexPlayed = currentIndexPlayed;
            currentIndexPlayed = nextIndex;
            audioSource.clip = audioClips[nextIndex];
            audioSource.Play();
        }

        private int GetNextIndex() {
            if (playAtRandomOrder)
                return GetRandomIndex();
            
            return GetNextOrderedIndex();
        }

        private int GetRandomIndex() {
            int randomIndex = Random.Range(0, audioClips.Length - 1);

            while (randomIndex == currentIndexPlayed)
                randomIndex = Random.Range(0, audioClips.Length - 1);

            Debug.Log("Random " + randomIndex);
            return randomIndex;
        }

        private int GetNextOrderedIndex() {
            int nextIndex = currentIndexPlayed + 1;

            if (nextIndex > (audioClips.Length - 1))
                nextIndex = 0;

            return nextIndex;
        }
    }
}