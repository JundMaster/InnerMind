using System.Collections;
using UnityEngine;

/// <summary>
/// Class for interaction with Neighbor. Extends InteractionNPCBase
/// </summary>
public class InteractionNPCNeighbor : InteractionNPCBase
{
    // NPC Head 
    [SerializeField] private Transform lookAt;

    // NPC rotation speed for its animation
    [SerializeField] private float rotationSpeedModifier;

    // NPC Prize related variables
    // Door to open when its puzzle is done
    [SerializeField] private Animator doorToOpen;
    // ScriptableItems to compare if the player as a certain item
    [SerializeField] private ScriptableItem[] npcItemsToCompare;
    // Items that the npc has in its back
    [SerializeField] private ScriptableItem[] npcBag;

    // Components
    private PlayerInput input;
    private Animator anim;
    private Inventory playerInventory;

    /// <summary>
    /// Awake method for InteractionNPCNeighbor
    /// </summary>
    private void Awake()
    {
        dialog = GetComponent<DialogText>();
        input = FindObjectOfType<PlayerInput>();
        anim = GetComponent<Animator>();
        playerInventory = FindObjectOfType<Inventory>();
    }

    /// <summary>
    /// Start method for InteractionNPCNeighbor
    /// </summary>
    private void Start()
    {
        // If the player already has a piano key, the speakcounter will be 3
        if (FindObjectOfType<Inventory>().Bag.Contains(npcBag[0]))
        {
            speakCounter = 3;
        }
        // Else it wil be 0
        else
        {
            speakCounter = 0;
        }

        // Instantiates a new WaitForSeconds with X seconds to wait
        waitForSecs = new WaitForSeconds(secondsToWait);
        dialog.WaitForSecs = waitForSecs;
    }

    /// <summary>
    /// Updated method for InteractionNPCNeighbor
    /// </summary>
    private void Update()
    {
        dialog.Counter = speakCounter;

        // If the player has a No battery flashlight
        // If the player doesn't have an old battery
        // If the player doesn't have a flashlight
        if (playerInventory.Bag.Contains(npcItemsToCompare[0]) == true &&
            playerInventory.Bag.Contains(npcItemsToCompare[1]) == false &&
            playerInventory.Bag.Contains(npcItemsToCompare[2]) == false)
        {
            // Gives NPC's prize
            dialog.GivePrize = true;
        }
        else
        {
            dialog.GivePrize = false;
        }

        // If the player has a not rewound tape
        if (playerInventory.Bag.Contains(npcItemsToCompare[3]))    
        {
            PlayerGeneralInfo player = FindObjectOfType<PlayerGeneralInfo>();
            // Adds mypuzzle to player's puzzles done
            player.PuzzlesDone |= PuzzlesEnum.Puzzle2;

            // Save puzzle to txt, so the txt will know which puzzles the player
            // has alreayd done
            FileWriter fw = new FileWriter(FilePath.puzzlePath);
            fw.AddToTxt(player);

            // Opens NPC's locked door
            dialog.OpenDoor = true;
        }
    }

    /// <summary>
    /// This Coroutine determines the action of the NPC when clicked
    /// </summary>
    /// <returns>Returns null</returns>
    public override IEnumerator CoroutineExecute()
    {
        input.ChangeTypeOfControl(TypeOfControl.InNPCInteraction);

        // Smoothly rotates npc towards the player and player towards npc
        StartCoroutine(RotationAnimation());

        // Gets next npc's action
        StartCoroutine(GetNextAction());
       
        yield break;
    }

    /// <summary>
    /// Coroutine used to get next line from DialogText.
    /// This Coroutine also checks NPC's conditions to know if it can
    /// give items, or open a locked door
    /// </summary>
    /// <returns>Returns null</returns>
    private IEnumerator GetNextAction()
    {
        StartCoroutine(dialog.GetNextLine());
        yield return waitForSecs;

        // If player has a no battery lantern
        // If the player doesn't have an old battery
        if (dialog.GivePrize &&
            playerInventory.Bag.Contains(npcItemsToCompare[1]) == false)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.Bag.Add(npcBag[1]);
        }

        // If player has a not rewound audio tape
        else if (dialog.OpenDoor)
        {
            doorToOpen.SetTrigger("Open Door");
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
        // to the initial loop after the text that happens only once
        if (speakCounter == dialog.LinesOfText.Length)
            speakCounter = 3;

        // Sets the coroutine to false so it can be called again
        ThisCoroutine = default;
        // Changes type of input
        input.ChangeTypeOfControl(TypeOfControl.InGameplay);
        yield break;
    }

    /// <summary>
    /// Coroutine used to animate the npc and the player at the sametime
    /// Rotates the npc towards the player and the player towards the npc
    /// </summary>
    /// <returns>Returns null</returns>
    private IEnumerator RotationAnimation()
    {
        anim.SetTrigger("turn");
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        PlayerLook playerCamera = player.GetComponentInChildren<PlayerLook>();

        float elapsedTime = 0.0f;

        // Sets desired values
        Quaternion npcFrom = transform.rotation;
        Quaternion npcTo = Quaternion.LookRotation(player.transform.position -
                                                transform.position);

        Quaternion playerFrom = player.transform.rotation;
        Quaternion playerTo = Quaternion.LookRotation(transform.position -
                                                player.transform.position);

        Quaternion pCameraFrom = playerCamera.transform.rotation;
        Quaternion pCameraTo = default;
        if (lookAt != null)
            pCameraTo = Quaternion.LookRotation(lookAt.position -
                                                playerCamera.transform.position);

        // Animation for both NPC and player rotations
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
        // Sets VerticalRotation to the same as rotation.eulerAngles.X at the 
        // moment, so the camera stays exactly the same angle after the anim
        playerCamera.VerticalRotation = 
            playerCamera.transform.rotation.eulerAngles.x;

        anim.SetTrigger("idle");
    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this npc
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        return "Speak to Neighbor";
    }
}