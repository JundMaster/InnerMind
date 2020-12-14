using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for handling the exhibition of the character's thoughts
/// </summary>
public abstract class ThoughtHandler : MonoBehaviour
{
	[SerializeField] private GameObject textThoughtCanvas;


    [SerializeField]
    private List<string> thoughtsText;

    private Text displayText;

    private bool onShowingText;

    /// <summary>
    /// List with the text of the thoughts
    /// </summary>
	private List<string> ThoughtsText { get; set; }

    /// <summary>
    /// Start method for ThoughtHandler
    /// </summary>
    private void Start()
    {
        onShowingText = false;
        ThoughtsText = thoughtsText;
        displayText = textThoughtCanvas?.GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Executes the necessary methods to reveal the thought to the player
    /// </summary>
    public abstract void ExecuteThought(int thoughtIndex);

    /// <summary>
    /// Reveals the text of the thought
    /// </summary>
    /// <param name="thoughtIndex">Index of the thought</param>
	protected void RevealTextThought(int thoughtIndex)
    {
		if (displayText != null)
        {
            StartCoroutine(RevealTextCoroutine(thoughtIndex));
        }
    }

    /// <summary>
    /// Coroutine that displays the text thought for limited time
    /// </summary>
    /// <param name="thoughtIndex">Index of the thought</param>
    /// <returns></returns>
    private IEnumerator RevealTextCoroutine(int thoughtIndex)
    {
 
        float elapsedTime = 0f;
        float timeLimit = 2.5f;

        if (elapsedTime < timeLimit && onShowingText)
            yield break;

        onShowingText = true;
        textThoughtCanvas.SetActive(true);
        displayText.text = ThoughtsText[thoughtIndex];
        while (elapsedTime < timeLimit)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
        }
        textThoughtCanvas.SetActive(false);
        onShowingText = false;


    }
}
