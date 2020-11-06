using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

sealed public class NPCNeighbor : NPCInteractable
{
    [SerializeField] private float rotationSpeedModifier;

    // How many texts in npc
    [SerializeField] private int numberOfTexts;

    // Wait for seconds instance
    private YieldInstruction waitForSecs;
    [SerializeField] private float secondsToWait;

    // NPC Head
    [SerializeField] private Transform head;

    // Speak
    private byte speakCounter;

    // Components
    private NPCText myText;

    private void Start()
    {
        myText = GetComponent<NPCText>();

        waitForSecs = new WaitForSeconds(secondsToWait);
        speakCounter = 0;
    }

    private void Update()
    {
        myText.Counter = speakCounter;
    }

    public override IEnumerator InteractionAction()
    {
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        PlayerInput input = player.GetComponent<PlayerInput>();
        PlayerLook playerCamera = player.GetComponentInChildren<PlayerLook>();
    

        // Smoothly rotates npc towards the player and player towards npc
        float elapsedTime = 0.0f;

        Quaternion npcFrom = transform.rotation;
        Quaternion npcTo = Quaternion.LookRotation(player.transform.position -
                                                transform.position);

        Quaternion playerFrom = player.transform.rotation;
        Quaternion playerTo = Quaternion.LookRotation(transform.position -
                                                player.transform.position);

        Quaternion pCameraFrom = playerCamera.transform.rotation;
        Quaternion pCameraTo = default;
        if (head != null)
            pCameraTo = Quaternion.LookRotation(head.position -
                                                playerCamera.transform.position);

        while (elapsedTime < 0.5f)
        {
            // Rotates NPC
            transform.rotation = Quaternion.Slerp(npcFrom, npcTo, elapsedTime *
                rotationSpeedModifier);

            transform.eulerAngles = new Vector3(0f,
                transform.eulerAngles.y, 0f);

            // Rotates Player's Body
            player.transform.rotation = Quaternion.Slerp(playerFrom, 
                playerTo, elapsedTime * rotationSpeedModifier);

            player.transform.eulerAngles = new Vector3(0f,
                player.transform.eulerAngles.y, 0f);

            // Rotates Player's Camera
            playerCamera.transform.rotation = Quaternion.Slerp(pCameraFrom,
                pCameraTo, elapsedTime * rotationSpeedModifier * 5f);

            // Moves player to desired range
            Vector3 newPosition = transform.position + 
                (player.transform.position - transform.position).normalized * 3f;

            player.transform.position = 
                Vector3.MoveTowards(player.transform.position, 
                new Vector3(newPosition.x, player.transform.position.y, 
                            newPosition.z),
                Time.deltaTime * rotationSpeedModifier * 2);


            elapsedTime += Time.deltaTime;
            yield return null;
        }
        playerCamera.VerticalRotation = 0;


        switch (speakCounter)
        {
            case 0:
                StartCoroutine(FirstTime(input));
                break;
            case 1:
                StartCoroutine(SecondTime(input));
                break;
            case 2:
                StartCoroutine(ThirdTime(input));
                break;
        }

        // If speakcounter reaches max number of texts, resets to 1
        speakCounter++;
        if (speakCounter == numberOfTexts)
            speakCounter = 1;

        yield break;
    }


    private IEnumerator FirstTime(PlayerInput input)
    {
        StartCoroutine(myText.NextLine());
        yield return waitForSecs;


        input.CurrentControl = TypeOfControl.InGameplay;
        ThisCoroutine = default;
        yield break;
    }
    private IEnumerator SecondTime(PlayerInput input)
    {
        StartCoroutine(myText.NextLine());
        yield return waitForSecs;


        input.CurrentControl = TypeOfControl.InGameplay;
        ThisCoroutine = default;
        yield break;
    }
    private IEnumerator ThirdTime(PlayerInput input)
    {
        StartCoroutine(myText.NextLine());
        yield return waitForSecs;


        input.CurrentControl = TypeOfControl.InGameplay;
        ThisCoroutine = default;
        yield break;
    }

    public override string ToString()
    {
        return "Speak to Neighbor";
    }
}
