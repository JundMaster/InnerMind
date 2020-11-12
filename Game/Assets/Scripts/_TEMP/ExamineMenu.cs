using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineMenu : MonoBehaviour
{
    [SerializeField] private GameObject examineMenu;
    public static bool InExameningMode { get; set; }
    private void Update()
    {
        if (InExameningMode)
            examineMenu.SetActive(true);
    }
}
