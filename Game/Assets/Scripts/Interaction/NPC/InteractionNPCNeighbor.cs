using System.Collections;
using UnityEngine;

public class InteractionNPCNeighbor : InteractionNPCBase
{
    // NPC Head & speed of the rotation
    [SerializeField] private Transform head;
    [SerializeField] private float rotationSpeedModifier;

    [SerializeField] private ScriptableItem[] npcBag;

    private PlayerInput input;
    private Animator anim;

    private void Awake()
    {
        dialog = GetComponent<DialogText>();
        input = FindObjectOfType<PlayerInput>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        waitForSecs = new WaitForSeconds(secondsToWait);
        speakCounter = 0;

        dialog.WaitForSecs = waitForSecs;
    }

    private void Update()
    {
        dialog.Counter = speakCounter;
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


        // will be needed to give items to player
        //Inventory temp = FindObjectOfType<Inventory>();




        // If speakcounter reaches max number of texts, resets to 1
        speakCounter++;
        if (speakCounter == dialog.LinesOfText.Length)
            speakCounter = 1;
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