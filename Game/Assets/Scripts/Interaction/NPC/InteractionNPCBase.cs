using System.Collections;
using UnityEngine;

public abstract class InteractionNPCBase : InteractionCR
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
        waitForSecs = new WaitForSeconds(secondsToWait);
        speakCounter = 0;
    }
    
    private void Update()
    {
        if (myText != null)
            myText.Counter = speakCounter;
    }
    public abstract override IEnumerator CoroutineInteraction();
}
