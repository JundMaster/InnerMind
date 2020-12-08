using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionFamilyPicture : InteractionCommon
{
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private string thought;
    [SerializeField] private string[] thoughts;
    [SerializeField] private GameObject thoughtCanvas;

    private void Start()
    {
        waitForSeconds = new WaitForSeconds(2);
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
    }

   

    public override void Execute()
    {
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
    public override string ToString() => "Examine Photo";
}
