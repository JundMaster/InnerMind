using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionMissingPianoKey : InteractionCommon
{
    private BoxCollider boxCollider;
    private Inventory inventory;
    private Text displayText;
    private WaitForSeconds waitForSeconds;
    private string thought;
    
    [SerializeField] private string[] thoughts;
    [SerializeField] private GameObject thoughtCanvas;
    [SerializeField] private GameObject keyPosition;
    [SerializeField] private ScriptableItem[] pianoKeys;


    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        boxCollider =  gameObject.GetComponent<BoxCollider>();
        displayText = thoughtCanvas.GetComponentInChildren<Text>();
        waitForSeconds = new WaitForSeconds(3);
    }


    public override void Execute()
    {
        for(int i = 0; i < pianoKeys.Length; i++)
        {
            if (inventory.Bag.Contains(pianoKeys[i]))
            {                 
                inventory.Bag.Remove(pianoKeys[i]);
                keyPosition.SetActive(true);
                boxCollider.enabled = false;
                thoughts[0] = "Ah, there ya go.";
                break;
            }  
        }
       
        StartCoroutine(DisplayThougthText(thoughts));
    }

    public override string ToString()
    {
        string str = "Examine Piano";
        for (int i = 0; i < pianoKeys.Length; i++)
        {
            if (inventory.Bag.Contains(pianoKeys[i]))
            {               
                str = "Place Missing Key";
            }  
        }
        
        return str;
    }
    private IEnumerator DisplayThougthText(string[] thoughts)
    {

        for (int i = 0; i < thoughts.Length; i++)
        {
            thoughtCanvas.SetActive(true);
            thought = thoughts[i];   
            displayText.text = thought;
            displayText.enabled = true;
            yield return waitForSeconds;
            displayText.enabled = false;
            thoughtCanvas.SetActive(false);
        }
    }
}
