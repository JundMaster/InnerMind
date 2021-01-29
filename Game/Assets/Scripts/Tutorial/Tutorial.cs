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
    private IPlayerInput input;

    // Variable to know the control it was before tutorial
    private TypeOfControl lastTypeOfControl;

    /// <summary>
    /// Awake method for Tutorial
    /// </summary>
    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
    }

    private void OnEnable()
    {
        index = 0;
        lastTypeOfControl = input.CurrentControl;
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
            input.ChangeTypeOfControl(lastTypeOfControl);
            tutorialParent.SetActive(false);
        }
    }
}
