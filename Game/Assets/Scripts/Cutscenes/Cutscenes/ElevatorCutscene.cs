using UnityEngine;

sealed public class ElevatorCutscene : MonoBehaviour
{
    private void Awake()
    {
        PlayerInput.ChangeTypeOfControl(TypeOfControl.InCutscene);
    }
}
