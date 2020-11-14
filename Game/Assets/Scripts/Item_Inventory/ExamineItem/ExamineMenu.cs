using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineMenu : MonoBehaviour
{
    [SerializeField] private GameObject examineMenu;
    [SerializeField] private Camera examineCamera;
    public Camera ExamineCamera => examineCamera;

    private void Update()
    {
        DisplayExamineMenu();
    }

    private void DisplayExamineMenu()
    {
        if (PlayerInput.CurrentControl == TypeOfControl.InExamine)
            examineMenu.SetActive(true);
        else
            examineMenu.SetActive(false);
    }
}
