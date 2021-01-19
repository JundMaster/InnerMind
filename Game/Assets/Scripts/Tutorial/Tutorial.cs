using UnityEngine;

/// <summary>
/// Class for tutorial
/// </summary>
public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorialParent;
    [SerializeField] private GameObject[] tutorials;

    byte index;

    // Components
    private PlayerInput input;

    /// <summary>
    /// Awake method for Tutorial
    /// </summary>
    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
        index = 0;
    }

    /// <summary>
    /// Start method for Tutorial
    /// </summary>
    private void Start()
    {
        tutorials?[0]?.SetActive(true);
        
    }

    /// <summary>
    /// Update method for tutorial
    /// </summary>
    private void Update()
    {
        if (input.Space) ChangeTutorial();

        if (tutorialParent.activeSelf)
            input.ChangeTypeOfControl(TypeOfControl.InTutorial);
    }

    /// <summary>
    /// Everytime the player presses space, it passes to the next tutorial
    /// </summary>
    private void ChangeTutorial()
    {
        tutorials?[index++]?.SetActive(false);

        if (index < tutorials.Length)
            tutorials?[index]?.SetActive(true);
        else
        {
            input.ChangeTypeOfControl(TypeOfControl.InGameplay);
            tutorialParent.SetActive(false);
        }
    }
}
