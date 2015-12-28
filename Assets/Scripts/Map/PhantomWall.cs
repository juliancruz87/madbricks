using DG.Tweening;
using Interactive;
using Interactive.Detail;
using Interactive.Totems;
using UnityEngine;

namespace Map 
{
    public class PhantomWall : MapObject 
	{
        private void OnTriggerEnter(Collider collider) 
		{
            GameObject collisionGameObject = collider.gameObject;
            Totem totem = collisionGameObject.GetComponent<Totem>();

            if (GameManager.Instance.CurrentState != GameStates.Play ||
                totem == null)
                return;

            TryStopTotem(totem);
        }

        private void TryStopTotem(Totem totem) 
		{
            TotemPhantom phantomTotem = totem.GetComponent<TotemPhantom>();

            if (phantomTotem == null) 
                StopTotem(totem);
            else 
                Debug.Log("Hitted by a phantom totem, its ok!");
        }

        private void StopTotem(Totem totem) 
		{
            Debug.Log("Hitted by a non phantom totem, fuck you!");
            totem.Stop();
            totem.transform.DOKill();
        }
    }
}