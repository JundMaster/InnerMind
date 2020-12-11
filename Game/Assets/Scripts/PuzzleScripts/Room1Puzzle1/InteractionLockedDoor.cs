using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for the locked Door in the 1st Room
/// </summary>
public class InteractionLockedDoor : InteractionCommon, ICoroutineT<string>
{
    //Components
    private Room1 room1;
    private Text displayText;
    private WaitForSeconds waitForSeconds;

    //Variables in Inspector
    [SerializeField] private string thought;
    [SerializeField] private GameObject thoughtCanvas;

    public Coroutine ThisCoroutine { get; private set; }

    /// <summary>
    /// Start method of InteractionLockedDoor
    /// </summary>
    private void Start()
    {
        room1 = FindObjectOfType<Room1>();
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        waitForSeconds = new WaitForSeconds(3);
        ThisCoroutine = null;
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
        if(ThisCoroutine == null)
            ThisCoroutine = StartCoroutine(CoroutineExecute(thought));

    }


    /// <summary>
    /// Renders a thought during a few seconds
    /// </summary>
    /// <param name="thought">Represents a single thought</param>
    /// <returns>Returns paused time in seconds</returns>
    public IEnumerator CoroutineExecute(string thought)
    {
        if(thought != null)
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

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "Open Door";

    
}

