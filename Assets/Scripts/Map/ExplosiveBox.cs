using Interactive;
using Interactive.Detail;
using Interactive.Totems;
using UnityEngine;
using Sound;

namespace Map {
    public class ExplosiveBox : MapObject 
	{

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
            if (collider.gameObject.GetComponent<TotemExplosive>() != null)
                Explode(totem);
            else
                HittedByNonExplosiveTotem(totem);
        }

        private void Explode(Totem totem) 
		{
			SoundManager.Instance.AudioSourceLib.ExplosiveTotem.Play ();
			GameManager.Instance.Goal ();
            Destroy(totem.gameObject);
            Destroy(gameObject);
        }

        private void HittedByNonExplosiveTotem(Totem totem) {
            Debug.Log("Hitted by a non explosive totem");
            totem.Stop();
            GameManager.Instance.Lose();
        }
    }
}
