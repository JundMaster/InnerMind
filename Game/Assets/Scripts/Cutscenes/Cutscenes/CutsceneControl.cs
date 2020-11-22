using UnityEngine;

sealed public class CutsceneControl : MonoBehaviour
{
    private PlayerInput input;
    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
        input.ChangeTypeOfControl(TypeOfControl.InCutscene);
    }
}
