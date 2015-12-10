using System.Collections;
using Drag;
using Map;
using UnityEngine;

namespace Sound {
    public class LauncherSoundPlayer : MonoBehaviour{
        [SerializeField]
        private AudioClip[] clips;
        private ArrayList launchers;
        private AudioSource audioSource;
        private DraggableObject[] draggableObjects;

        private void Awake() {
            audioSource = GetComponent<AudioSource>();
        }

        private void Update() {
            if (launchers == null ||
                launchers.Count == 0) {
                InitLaunchers();
            }

            if (draggableObjects == null ||
                draggableObjects.Length == 0) {
                draggableObjects = FindObjectsOfType<DraggableObject>();
                if (draggableObjects != null)
                    InitDraggableObjects();
            }
        }

        private void InitLaunchers() {
            launchers = new ArrayList();
            MapObject[] candidateLaunchers = FindObjectsOfType<MapObject>();
            foreach (MapObject candidateLauncher in candidateLaunchers) {
                if (candidateLauncher.Type == MapObjectType.LauncherNormal ||
                    candidateLauncher.Type == MapObjectType.LauncherSticky)
                    launchers.Add(candidateLauncher);
            }
        }

        private void InitDraggableObjects() {
            foreach (DraggableObject draggableObject in draggableObjects) {
                draggableObject.OnLauncherTouched += PlayLauncherSound;
            }
        }

        private void PlayLauncherSound(MapObject launcher) {
            int launcherIndex = launchers.IndexOf(launcher);
            audioSource.clip = clips[launcherIndex];
            audioSource.Play();
        }
    }
}