using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionCloset : InteractionCommon
{
   
    private Animator closetDoorAnimation;
    private BoxCollider closetBoxCollider;
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private string thought;
    [SerializeField] private string[] thoughts;
    [SerializeField] private GameObject thoughtCanvas;


    private void Start()
    {
        closetDoorAnimation = GetComponentInChildren<Animator>();
        closetBoxCollider = GetComponent<BoxCollider>();
        waitForSeconds = new WaitForSeconds(2);
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
    }
    public override void Execute()
    {
        
        closetDoorAnimation.SetTrigger("Open Door");
        closetBoxCollider.enabled = false;
        StartCoroutine(DisplayThougthText(thoughts));
    }

    private IEnumerator DisplayThougthText(string[] thoughts)
    {

        for (int i = 0; i < thoughts.Length; i++)
        {           
            thought = thoughts[i];
            thoughtCanvas.SetActive(true);
            displayText.enabled = true;
            displayText.text = thought;
            yield return waitForSeconds;
            thoughtCanvas.SetActive(false);
            displayText.enabled = false;
        }
    }

    public override string ToString() => "Open Closet";



}
