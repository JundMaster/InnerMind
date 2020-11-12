using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineMenu : MonoBehaviour
{
    [SerializeField] private GameObject examineMenu;
    [SerializeField] private Camera examineCamera;
    public static bool InExameningMode { get; set; }
    public static Camera ExamineCamera { get; private set; }
    private void Start()
    {
        ExamineCamera = examineCamera;
    }
    private void Update()
    {
        if (PlayerInput.CurrentControl == TypeOfControl.InExamine)
            examineMenu.SetActive(true);
        else
            examineMenu.SetActive(false);
    }
}
