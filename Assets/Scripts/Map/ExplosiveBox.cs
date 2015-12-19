using Interactive;
using Interactive.Detail;
using Map;
using UnityEngine;

namespace Map {
    public class ExplosiveBox : MapObject {

        // Use this for initialization
        void Start () {
            InitExplosiveBox();
        }

        private void InitExplosiveBox() {
            gameObject.tag = "Obstacle";
	        type = MapObjectType.ExplosiveBox;
        }

        private void OnTriggerEnter(Collider collider) {
            GameObject collisionGameObject = collider.gameObject;
            Totem totem = collisionGameObject.GetComponent<Totem>();

            if (GameManager.Instance.CurrentState != GameStates.Play || 
                totem == null)
                return;
            if (collider.gameObject.GetComponent<ExplosiveTotem>() != null)
                Explode();
            else
                HittedByNonExplosiveTotem(totem);
        }

        private void Explode() {
            Destroy(gameObject);
        }

        private void HittedByNonExplosiveTotem(Totem totem) {
            Debug.Log("Hitted by a non explosive totem");
            totem.Stop();
            GameManager.Instance.Lose();
        }
    }
}
