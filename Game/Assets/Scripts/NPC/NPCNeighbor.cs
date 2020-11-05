using System;
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

        PlayerLook player = FindObjectOfType<PlayerLook>();

        // Smoothly rotates towards the player
        float elapsedTime = 0.0f;
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.LookRotation(-player.transform.forward);
        while (elapsedTime < 0.5f)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsedTime / 0.5f);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);


        switch (speakCounter)
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
     
        // If speakcounter reaches max number of texts, resets to 1
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
