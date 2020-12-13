using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for the closet  in the 1st Room
/// </summary>
public class InteractionCloset : InteractionCommon, ICoroutineT<string[]>
{
    //Components
    private Animator closetDoorAnimation;
    private BoxCollider closetBoxCollider;
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private string thought;

    //Variables in Inspector
    [SerializeField] private string[] thoughts;
    [SerializeField] private Canvas thoughtCanvas;

    /// <summary>
    /// Property to control a couroutine
    /// </summary>
    public Coroutine ThisCoroutine { get; private set;}

    /// <summary>
    /// Start method of InteractionCloset
    /// </summary>
    private void Awake()
    {
        closetDoorAnimation = GetComponentInChildren<Animator>();
        closetBoxCollider = GetComponent<BoxCollider>();
        waitForSeconds = new WaitForSeconds(2);
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        ThisCoroutine = null;
    }

    /// <summary>
    /// This method determines the action of the closet when clicked
    /// </summary>
    public override void Execute()
    { 
        closetDoorAnimation.SetTrigger("Open Door");
        closetBoxCollider.enabled = false;

        if(ThisCoroutine == null)
            if (thoughtCanvas.enabled == false)
                ThisCoroutine = StartCoroutine(CoroutineExecute(thoughts));
    }

    /// <summary>
    /// Renders thougths regarding the interacted object
    /// one at a time during a few seconds before dissapearing
    /// </summary>
    /// <param name="thoughts"> Array with all thoughts regarding the object</param>
    /// <returns>Returns paused time in seconds</returns>
    public IEnumerator CoroutineExecute(string[] thoughts)
    {
        for (int i = 0; i < thoughts.Length; i++)
        {
            thought = thoughts[i];
            thoughtCanvas.enabled = true;
            displayText.enabled = true;
            displayText.text = thought;
            yield return waitForSeconds;
            thoughtCanvas.enabled = false;
            displayText.enabled = false;
            ThisCoroutine = null;
        }
    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => "Open Closet";

    
}
