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


    private void Start()
    {
        closetDoorAnimation = GetComponentInChildren<Animator>();
        closetBoxCollider = GetComponent<BoxCollider>();
        waitForSeconds = new WaitForSeconds(2);
        displayText = FindObjectOfType<Text>();
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
            displayText.enabled = true;
            displayText.text = thought;
            yield return waitForSeconds;
            displayText.enabled = false;
        }
    }

    public override string ToString() => "Open Closet";



}
