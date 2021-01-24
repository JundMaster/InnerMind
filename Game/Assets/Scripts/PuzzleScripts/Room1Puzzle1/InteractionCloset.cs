using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class used for the closet  in the 1st Room
/// </summary>
public class InteractionCloset : InteractionCommon, ICoroutineT<string[]>
{
    //Components
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private string thought;
    private Inventory inventory;

    //Variables in Inspector
    [SerializeField] private string[] thoughts;
    [SerializeField] private Canvas thoughtCanvas;
    [SerializeField] private string animTrigger;
    [SerializeField] private Animator closetDoorAnimation;
    [SerializeField] private ScriptableItem cabinetKey;
    [SerializeField] private ScriptableItem pillBottle;

    /// <summary>
    /// Property to control a couroutine
    /// </summary>
    public Coroutine ThisCoroutine { get; private set; }

    /// <summary>
    /// Start method of InteractionCloset
    /// </summary>
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        closetDoorAnimation = GetComponentInChildren<Animator>();
        waitForSeconds = new WaitForSeconds(2);
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        ThisCoroutine = null;
    }

    /// <summary>
    /// This method determines the action of the closet when clicked
    /// </summary>
    public override void Execute()
    {
            this.gameObject.layer = 2;
            closetDoorAnimation.SetTrigger(animTrigger);
            if (!inventory.Bag.Contains(cabinetKey) && 
                !inventory.Bag.Contains(pillBottle))
            {
                if (ThisCoroutine == null)
                    if (thoughtCanvas.enabled == false)
                        ThisCoroutine = StartCoroutine(CoroutineExecute(thoughts));
            }
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
    public override string ToString() => "Open closet's door";


}
