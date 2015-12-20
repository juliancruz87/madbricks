using DG.Tweening;
using Interactive;
using Interactive.Detail;
using Interactive.Totems;
using UnityEngine;

namespace Map {
    public class MapHole : MapObject {
        [SerializeField]
        private TotemType holeType;
        [SerializeField]
        private bool filled;
        [SerializeField] 
        private Vector3 holeOffset;
        [SerializeField]
        private float timeToSnapToThisTransform;
        [SerializeField]
        private float timeToSnapToTheBottom;

        private Transform myTransform;
        private Totem fillTotem;

        // Use this for initialization

        private void Start() {
            myTransform = transform;
        }

        private void OnTriggerEnter(Collider collider) {
            GameObject collisionGameObject = collider.gameObject;
            Totem totem = collisionGameObject.GetComponent<Totem>();
            
            if (!filled &&
                GameManager.Instance.CurrentState != GameStates.Play ||
                totem == null)
                return;
            
            TryToFillHole(totem);
        }
        
        private void TryToFillHole(Totem totem) {
            //totem.Stop();
            Debug.Log("Totem type: " + totem.Type);
            Debug.Log("This hole type: " + holeType);

            TotemHoleFiller holeFiller = totem.GetComponent<TotemHoleFiller>();

            if (holeFiller != null &&
                holeType == totem.Type) {
                FillHole(totem);
                return;
            }

            totem.Stop();
            totem.transform.DOKill();
            
            Debug.Log("Hit a totem, but it was not of the same type of the hole");
            GameManager.Instance.Lose();
        }

        private void FillHole(Totem totem) {
            filled = true;
            fillTotem = totem;
            fillTotem.Stop();
            Destroy(fillTotem.gameObject.GetComponent<Collider>());
            Destroy(gameObject.GetComponent<Collider>());
            SnapTotemToThisTransform(); 
        }

        private void SnapTotemToThisTransform() {
            fillTotem.transform.DOKill();
            fillTotem.transform.DOMove(myTransform.position, timeToSnapToThisTransform).SetEase(Ease.Linear);
            Invoke("SnapTotemToBottom", timeToSnapToThisTransform);
        }

        private void SnapTotemToBottom() {
            fillTotem.transform.DOMove(holeOffset + myTransform.position, timeToSnapToTheBottom).SetEase(Ease.Linear);
        }
    }
}