using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed public class NPCNeighbor : NPCInteractable
{
    private int speakCounter;

    private void Start()
    {
        waitForSecs = new WaitForSeconds(secondsToWait);
        speakCounter = 0;
    }
    public override IEnumerator InteractionAction()
    {
        switch(speakCounter)
        {
            case 0:
                StartCoroutine(FirstTime());
                break;
            case 1:
                StartCoroutine(SecondTime());
                break;
            case 2:
                StartCoroutine(ThirdTime());
                break;
        }
     
        speakCounter++;
        if (speakCounter == numberOfTexts)
            speakCounter = 1;

        yield break;
    }


    private IEnumerator FirstTime()
    {
        Debug.Log("Hello theeeere  for the first time");
        yield return waitForSecs;



        CR_RunningCoroutine = false;
        yield break;
    }
    private IEnumerator SecondTime()
    {
        Debug.Log("Hello theeeere  for the second time");
        yield return waitForSecs;



        CR_RunningCoroutine = false;
        yield break;
    }
    private IEnumerator ThirdTime()
    {
        Debug.Log("Hello theeeere  for the third time");
        yield return waitForSecs;
 


        CR_RunningCoroutine = false;
        yield break;
    }
}
