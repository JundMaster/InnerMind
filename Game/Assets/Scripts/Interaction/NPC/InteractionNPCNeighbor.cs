using System.Collections;
using UnityEngine;

public class InteractionNPCNeighbor : InteractionNPCBase
{
    // NPC Head & speed of the rotation
    [SerializeField] private Transform head;
    [SerializeField] private float rotationSpeedModifier;

    [SerializeField] private Animator doorToOpen;
    [SerializeField] private ScriptableItem[] npcItemsToCompare;
    [SerializeField] private ScriptableItem[] npcBag;

    private PlayerInput input;
    private Animator anim;
    private Inventory playerInventory;

    private void Awake()
    {
        dialog = GetComponent<DialogText>();
        input = FindObjectOfType<PlayerInput>();
        anim = GetComponent<Animator>();
        playerInventory = FindObjectOfType<Inventory>();
    }

    private void Start()
    {
        // If the player already has a piano key, the speakcounter will be 3
        if (FindObjectOfType<Inventory>().Bag.Contains(npcBag[0]))
        {
            speakCounter = 3;
        }
        else
        {
            speakCounter = 0;
        }

        waitForSecs = new WaitForSeconds(secondsToWait);

        dialog.WaitForSecs = waitForSecs;
    }

    private void Update()
    {
        dialog.Counter = speakCounter;

        // If the player has a No battery lantern
        // If the player doesn't have an old battery
        // If the player doesn't have a lantern
        if (playerInventory.Bag.Contains(npcItemsToCompare[0]) == true &&
            playerInventory.Bag.Contains(npcItemsToCompare[1]) == false &&
            playerInventory.Bag.Contains(npcItemsToCompare[2]) == false)
        {
            dialog.givePrize = true;
        }
        else
        {
            dialog.givePrize = false;
        }
    }

    public override IEnumerator CoroutineExecute()
    {
        input.ChangeTypeOfControl(TypeOfControl.InNPCInteraction);

        // Smoothly rotates npc towards the player and player towards npc
        StartCoroutine(RotationAnimation());

        StartCoroutine(GetNextAction());
       
        yield break;
    }

    private IEnumerator GetNextAction()
    {
        StartCoroutine(dialog.GetNextLine());
        yield return waitForSecs;

        // If player has a no battery lantern
        if (dialog.givePrize)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.Bag.Add(npcBag[1]);
        }

        // Give a piano key on second time speaking
        else if (speakCounter == 2)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.Bag.Add(npcBag[0]);
            speakCounter++;
        }

        else
        {
            speakCounter++;
        }
        
        // If speakcounter reaches max number of texts, resets the text
        if (speakCounter == dialog.LinesOfText.Length)
            speakCounter = 3;

        ThisCoroutine = default;
        input.ChangeTypeOfControl(TypeOfControl.InGameplay);
        yield break;
    }


    private IEnumerator RotationAnimation()
    {
        anim.SetTrigger("turn");
        PlayerMovement player;
        player = FindObjectOfType<PlayerMovement>();
        PlayerLook playerCamera;
        playerCamera = player.GetComponentInChildren<PlayerLook>();

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
                pCameraTo, elapsedTime * rotationSpeedModifier);

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
        playerCamera.VerticalRotation = playerCamera.transform.rotation.eulerAngles.x;
        anim.SetTrigger("idle");


    }

    public override string ToString()
    {
        return "Speak to Neighbor";
    }
}