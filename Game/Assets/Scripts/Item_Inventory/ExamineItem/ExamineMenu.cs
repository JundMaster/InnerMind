using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineMenu : MonoBehaviour
{
    [SerializeField] private GameObject examineMenu;
    [SerializeField] private Camera examineCamera;
    public Camera ExamineCamera => examineCamera;

    private PlayerInput input;

    private void Start()
    {
        input = FindObjectOfType<PlayerInput>();
    }

    public void DisplayExamineMenu()
    {
        if (input.CurrentControl == TypeOfControl.InExamine)
            examineMenu.SetActive(true);
    }
    public void HideDisplayMenu()
    {
        examineMenu.SetActive(false);
    }
}
