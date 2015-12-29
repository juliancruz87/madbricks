using DG.Tweening;
using Interactive;
using Interactive.Detail;
using Interactive.Totems;
using UnityEngine;

namespace Map 
{
    public class Portal : MapObject 
	{
        [SerializeField]
        private PortalTarget portalTarget;

        private void Start() 
		{
         	gameObject.tag = "Obstacle";
        }

        private void OnTriggerEnter(Collider collider) 
		{
            GameObject collisionGameObject = collider.gameObject;
            Totem totem = collisionGameObject.GetComponent<Totem>();

            if (GameManager.Instance.CurrentState != GameStates.Play || totem == null)
                return;

            TeleportTotem(totem);
        }

        private void TeleportTotem(Totem totem) 
		{
            totem.Stop();
            totem.transform.DOKill();
            totem.transform.position = portalTarget.transform.position;

            totem.GoToSecondaryPositionToGo();
        }
    }
}