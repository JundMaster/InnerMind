using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used when interacting with the piano with missing keys
/// </summary>
public class InteractionMissingPianoKey : InteractionCommon, ICoroutineT<string>
{
    //Components
    private BoxCollider boxCollider;
    private Inventory inventory;
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private Room4 roomPuzzle;
    

    //Inspector Variables
    [SerializeField] private string thought;
    [SerializeField] private GameObject thoughtCanvas;
    [SerializeField] private GameObject keyPosition;
    [SerializeField] private ScriptableItem[] pianoKeys;
    

    public Coroutine ThisCoroutine { get; private set; }
    public bool CanInteractWithPiano { get; private set; }


    /// <summary>
    /// Start method of InteractionMissingPianoKey
    /// </summary>
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        waitForSeconds = new WaitForSeconds(3);
        ThisCoroutine = null;
        roomPuzzle = FindObjectOfType<Room4>();


        CanInteractWithPiano = inventory.Bag.Contains(pianoKeys[0]) &&
                inventory.Bag.Contains(pianoKeys[1]) &&
                    inventory.Bag.Contains(pianoKeys[2]);
    }

    /// <summary>
    /// This method determines the action of the missing key when clicked
    /// </summary>
    public override void Execute()
    {
        if (CanInteractWithPiano)
        {
            for (int i = 0; i < pianoKeys.Length; i++)
            {

                //Checks if the player has piano keys in its inventory
                //placing missing key and removing it from it's inventory
                if (inventory.Bag.Contains(pianoKeys[i]))
                {

                    inventory.Bag.Remove(pianoKeys[i]);
                    keyPosition.SetActive(true);
                    boxCollider.enabled = false;
                    thought = "Ah, there ya go.";
                    break;


                }
            }
        }
        else thought = "Hmm...Some keys are missing...";

        if (ThisCoroutine == null && !thoughtCanvas.activeSelf)
        {
            ThisCoroutine = StartCoroutine(CoroutineExecute(thought));
        }
        
    }


    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString()
    {
        string str = "Examine Piano";
        for (int i = 0; i < pianoKeys.Length; i++)
        {
            //If the player has a key in its inventory changes the returned
            //string
            if (inventory.Bag.Contains(pianoKeys[i]))
            {
                str = "Place Missing Key";
            }
        }

        return str;
    }

    /// <summary>
    /// Renders a thought during a few seconds
    /// </summary>
    /// <param name="thought">Represents a single thought</param>
    /// <returns>Returns paused time in seconds</returns>
    public IEnumerator CoroutineExecute(string thought)
    {
        thoughtCanvas.SetActive(true);
        displayText.text = thought;
        displayText.enabled = true;
        yield return waitForSeconds;
        displayText.enabled = false;
        thoughtCanvas.SetActive(false);
        ThisCoroutine = null;
    }
}
