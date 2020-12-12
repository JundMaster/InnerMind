using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionMedicineCabinet : InteractionCommon, ICoroutineT<string>
{
    //Components 
    private Animator cabinetDoorAnimation;
    private BoxCollider closetBoxCollider;
    private Inventory inventory;
    private Text displayText;
    private WaitForSeconds waitForSeconds;

    /// <summary>
    /// Property used to check if the player has opened the cabinet
    /// </summary>
    public bool IsOpen { get; private set; }

    public Coroutine ThisCoroutine { get; private set; }

    //Inspector Variables
    [SerializeField] private string thought;
    [SerializeField] private GameObject thoughtCanvas;
    [SerializeField] private ScriptableItem cabinetKey;

    /// <summary>
    /// Start method of InteractionMedicineCabinet
    /// </summary>
    private void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        IsOpen = false;
        cabinetDoorAnimation = GetComponentInChildren<Animator>();
        closetBoxCollider = GetComponent<BoxCollider>();
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        waitForSeconds = new WaitForSeconds(3);
        ThisCoroutine = null;
    }

    /// <summary>
    /// This method determines the action of the cabinet when clicked
    /// </summary>
    public override void Execute()
    {
        //If the player has the key, removes it from inventory
        //and plays the animation
        if (inventory.Bag.Contains(cabinetKey))
        {
            thought = null;
            IsOpen = true;
            closetBoxCollider.enabled = false;
            cabinetDoorAnimation.SetTrigger("Open Door");
            inventory.Bag.Remove(cabinetKey);
        }
        //Else it displays a thought on the locked cabinet
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
        if (thought != null)
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
    public override string ToString() => "Open Cabinet";

}




