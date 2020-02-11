using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    public NavMeshAgent playerAgent;
   public virtual void MoveToInteraction(NavMeshAgent playerAgent)
   {
        this.playerAgent = playerAgent;
        playerAgent.destination = this.transform.position;

        Interact();
   }

    public virtual void Interact()
    {
        Debug.Log("Interacting with base class");
    }
}
