using UnityEngine;

sealed public class CutsceneControl : MonoBehaviour
{
    private void Awake()
    {
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InCutscene);
    }
}
