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

    private void Update()
    {
        DisplayExamineMenu();
    }

    private void DisplayExamineMenu()
    {
        if (input.CurrentControl == TypeOfControl.InExamine)
            examineMenu.SetActive(true);
        else
            examineMenu.SetActive(false);
    }
}
