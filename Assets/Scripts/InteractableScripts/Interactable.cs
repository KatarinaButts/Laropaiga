using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 1.0f;
    public Transform interactionTransform;

    bool hasInteracted = false;
    Transform player;

    private void OnDrawGizmosSelected()
    {
        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(interactionTransform.position.x, interactionTransform.position.y, 0), radius);
    }

    public virtual void Interact(Transform PlayerTransform)
    {
        player = PlayerTransform;

        float distance = Vector3.Distance(player.position, interactionTransform.position);
        if(distance <= radius)
        {
            //This method is meant to be overwritten
            //Debug.Log("Interacting with " + transform.name);
        }

    }

}
