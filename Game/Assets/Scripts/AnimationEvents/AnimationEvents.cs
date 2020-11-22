using UnityEngine;

public class AnimationEvents : MonoBehaviour
{

    public void ActivateGameObject() => this.gameObject.SetActive(true);
    public void DeactivateGameObject() => this.gameObject.SetActive(false);
    public void ChangeTypeOfControl(TypeOfControl typeOfControl)
    {
        PlayerInput input;
        input = FindObjectOfType<PlayerInput>();
        input.ChangeTypeOfControl(typeOfControl);
    }
}
