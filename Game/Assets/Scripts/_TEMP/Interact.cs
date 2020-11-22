using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour, IInteract
{
    [SerializeField] private TypeOfInteraction interactionType;

    InteractionCommon interaction;

    private void OnValidate()
    {
        switch (interactionType)
        {
            case TypeOfInteraction.Item:
                if (interaction == null)
                    interaction = gameObject.AddComponent<InteractionItem>();
                break;
            case TypeOfInteraction.NPC:
                if (interaction == null)
                    interaction = gameObject.AddComponent<InteractionNPCNeighbor>();
                break;
        }   
    }

    public void Execute()
    {
        interaction.Execute();
    }

    
}
