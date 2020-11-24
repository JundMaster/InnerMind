using UnityEngine;

sealed public class OnCutsceneAwake : MonoBehaviour
{
    private PlayerInput input;
    private void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
        input.ChangeTypeOfControl(TypeOfControl.InCutscene);
    }
}
