using System.Collections;
using UnityEngine;

/// <summary>
/// Class for interaction with Neighbor. Extends InteractionNPCBase
/// </summary>
public class InteractionNPCNeighbor : InteractionNPCBase
{
    // NPC Prize related variables
    // Door to open when its puzzle is done
    [SerializeField] private Animator doorToOpen;

    // Components
    private IPlayerInput input;
    private Inventory playerInventory;
    private ItemComparer itemComparer;

    // Animation Behaviour
    [SerializeField] private NPCMovementBehaviour movementBehaviour;

    /// <summary>
    /// Awake method for InteractionNPCNeighbor
    /// </summary>
    private void Awake()
    {
        dialog = GetComponent<DialogText>();
        input = FindObjectOfType<PlayerInput>();
        playerInventory = FindObjectOfType<Inventory>();
        itemComparer = FindObjectOfType<ItemComparer>();
    }

    /// <summary>
    /// Start method for InteractionNPCNeighbor
    /// </summary>
    private void Start()
    {
        // If the player already has a map, the speakcounter will be 3
        if (FindObjectOfType<Inventory>().Bag.Contains(itemComparer.Map))
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
        if (playerInventory.Bag.Contains(
                                itemComparer.NoBatteryFlashlight) == true &&
            playerInventory.Bag.Contains(itemComparer.OldBattery) == false &&
            playerInventory.Bag.Contains(itemComparer.FlashLight) == false)
        {
            // Gives NPC's prize
            dialog.GivePrize = true;
        }
        else
        {
            dialog.GivePrize = false;
        }

        // If the player has a not rewound tape
        if (playerInventory.Bag.Contains(itemComparer.NotRewoundAudioTape))
        {
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
        movementBehaviour.ExecuteBehaviour();

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
            playerInventory.Bag.Contains(itemComparer.OldBattery) == false)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.Bag.Add(itemComparer.OldBattery);
        }

        // If player has a not rewound audio tape
        else if (dialog.OpenDoor)
        {
            doorToOpen.SetTrigger("Open Door");

            PlayerGeneralInfo player = FindObjectOfType<PlayerGeneralInfo>();
            // Adds mypuzzle to player's puzzles done
            player.PuzzlesDone |= PuzzlesEnum.Puzzle2;

            // Save puzzle to txt, so the txt will know which puzzles the player
            // has alreayd done
            FileWriter fw = new FileWriter(FilePath.puzzlePath);
            fw.AddToTxt(player);
        }

        // Give a piano key on second time speaking
        else if (speakCounter == 2)
        {
            Inventory inventory = FindObjectOfType<Inventory>();
            inventory.Bag.Add(itemComparer.Map);
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
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this npc
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        return "Speak to Neighbor";
    }
}