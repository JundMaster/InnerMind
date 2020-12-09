using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for the locked Door in the 1st Room
/// </summary>
public class InteractionLockedDoor : InteractionCommon
{
    //Components
    private Room1 room1;
    private Text displayText;
    private WaitForSeconds waitForSeconds;

    //Variables in Inspector
    [SerializeField] private string thought;
    [SerializeField] private GameObject thoughtCanvas;

    /// <summary>
    /// Start method of InteractionLockedDoor
    /// </summary>
    private void Start()
    {
        room1 = FindObjectOfType<Room1>();
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        waitForSeconds = new WaitForSeconds(3);
    }

    /// <summary>
    /// This method determines the action of the door when clicked
    /// </summary>
    public override void Execute()
    {
        //Opens the door if the player has finished the puzzle
        if (room1.FinishedPuzzle)
        {
            Animator doorAnimation = GetComponent<Animator>(); ;
            doorAnimation.SetTrigger("Open Door");
            thought = null;
        }

        //Else it displays a thought on the locked door
        StartCoroutine(DisplayThougthText(thought));

    }


    /// <summary>
    /// Renders a thought during a few seconds
    /// </summary>
    /// <returns>Returns paused time in seconds</returns>
    private IEnumerator DisplayThougthText(string thought)
    {
        //Checks if there is a thought to render
        if (thought != null)
        {
            thoughtCanvas.SetActive(true);
            displayText.enabled = true;
            displayText.text = thought;
            yield return waitForSeconds;
            thoughtCanvas.SetActive(false);
            displayText.enabled = false;
        }


    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "Open Door";
}

