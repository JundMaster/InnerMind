using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used when interacting with the piano with missing keys
/// </summary>
public class InteractionMissingPianoKey : InteractionCommon
{
    //Components
    private BoxCollider boxCollider;
    private Inventory inventory;
    private Text displayText;
    private WaitForSeconds waitForSeconds;

    //Inspector Variables
    [SerializeField] private string thought;
    [SerializeField] private GameObject thoughtCanvas;
    [SerializeField] private GameObject keyPosition;
    [SerializeField] private ScriptableItem[] pianoKeys;


    /// <summary>
    /// Start method of InteractionMissingPianoKey
    /// </summary>
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        boxCollider = gameObject.GetComponent<BoxCollider>();
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        waitForSeconds = new WaitForSeconds(3);
    }

    /// <summary>
    /// This method determines the action of the missing key when clicked
    /// </summary>
    public override void Execute()
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
            else thought = "Hmm...Some keys are missing...";
        }

        StartCoroutine(DisplayThougthText(thought));
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
    private IEnumerator DisplayThougthText(string thought)
    {
        thoughtCanvas.SetActive(true);
        displayText.text = thought;
        displayText.enabled = true;
        yield return waitForSeconds;
        displayText.enabled = false;
        thoughtCanvas.SetActive(false);
    }
}
