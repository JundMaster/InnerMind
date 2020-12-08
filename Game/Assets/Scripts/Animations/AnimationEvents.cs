using UnityEngine;

/// <summary>
/// Class to be used on animation events
/// </summary>
public class AnimationEvents : MonoBehaviour
{
    /// <summary>
    /// Sets a gameobject .SetActive to true
    /// </summary>
    public void ActivateGameObject() => this.gameObject.SetActive(true);

    /// <summary>
    /// Sets a gameobject .SetActive to false
    /// </summary>
    public void DeactivateGameObject() => this.gameObject.SetActive(false);

    /// <summary>
    /// Changes TypeOfControl from PlayerInput
    /// </summary>
    /// <param name="typeOfControl">TypeOfControl to change to</param>
    public void ChangeTypeOfControl(TypeOfControl typeOfControl)
    {
        PlayerInput input;
        input = FindObjectOfType<PlayerInput>();
        input.ChangeTypeOfControl(typeOfControl);
    }
}
