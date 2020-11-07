using System.Collections;
using UnityEngine;

public abstract class NPC_InteractionBase : Interaction_CR
{

    [SerializeField] protected float rotationSpeedModifier;

    // How many texts in npc
    [SerializeField] protected byte numberOfTexts;

    // Wait for seconds instance
    protected YieldInstruction waitForSecs;
    [SerializeField] protected float secondsToWait;

    protected byte speakCounter;

    // Components
    protected NPCText myText;

    private void Start()
    {
        myText = GetComponent<NPCText>();

        waitForSecs = new WaitForSeconds(secondsToWait);
        speakCounter = 0;
    }
    
    // Speak
    private void Update()
    {
        // myText.Counter = speakCounter;
    }
    protected abstract override IEnumerator CoroutineInteraction();
}
