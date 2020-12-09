using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for the Examine Menu
/// </summary>
public class ExamineMenu : MonoBehaviour
{
    // Game object that hold the elements that compose the Examine Menu
    [SerializeField] private GameObject examineMenuUIHolder;

    [SerializeField] private Camera examineCamera;
    
    /// <summary>
    /// Camera that will capture the object in examine
    /// </summary>
    public Camera ExamineCamera => examineCamera;

    private PlayerInput input;

    /// <summary>
    /// Start method for ExamineMenu
    /// </summary>
    private void Start()
    {
        input = FindObjectOfType<PlayerInput>();
    }

    /// <summary>
    /// Displays the examine menu UI
    /// </summary>
    public void DisplayExamineMenu()
    {
        if (input.CurrentControl == TypeOfControl.InExamine)
            examineMenuUIHolder.SetActive(true);
    }

    /// <summary>
    /// Hides the examine menu UI
    /// </summary>
    public void HideDisplayMenu()
    {
        examineMenuUIHolder.SetActive(false);
    }
}
