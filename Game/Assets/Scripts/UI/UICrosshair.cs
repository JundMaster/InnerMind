using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Class that controls UI and crosshair in the middle of the screen while
/// in gameplay
/// </summary>
public class UICrosshair : MonoBehaviour
{
    // Text to print action
    [SerializeField] private GameObject actionTextGameObject;
    private TextMeshProUGUI actionText;

    // Black square behind the text
    private Image panel;

    // Components
    private PlayerRays ray;
    private PlayerInteract interact;
    private Image crosshair;
    private IPlayerInput input;

    /// <summary>
    /// Awake method for UICrosshair
    /// </summary>
    void Awake()
    {
        actionText = actionTextGameObject.
            GetComponentInChildren<TextMeshProUGUI>();
        panel = actionTextGameObject.
            GetComponentInChildren<Image>();

        ray = FindObjectOfType<PlayerRays>();
        interact = FindObjectOfType<PlayerInteract>();
        crosshair = GetComponent<Image>();
        input = FindObjectOfType<PlayerInput>();
    }

    /// <summary>
    /// Update method for UICrosshair
    /// </summary>
    void Update()
    {
        // If in gameplay
        if (input.CurrentControl == TypeOfControl.InGameplay)
        {
            // If there's an object inside interact distance
            // it returns that object as RaycastHit hit
            if (Physics.Raycast(ray.RayToMouse, out RaycastHit hit,
                    interact.InteractDistance))
            {
                // Uses that information to check if the object is
                // an IInteract obj
                if (hit.collider.gameObject.TryGetComponent
                    (out IInteract other))
                {
                    // Shows text with information near crosshair
                    panel.color = new Color(0, 0, 0, 0.4f);
                    actionText.text = other.ToString();
                    actionText.color = new Color(1, 1, 1, 1);
                    crosshair.color = new Color(1, 0, 0, 1);          
                }
                // If it's not IInteract
                else
                {
                    // Doesn't show anything
                    panel.color = new Color(0, 0, 0, 0);
                    actionText.color = new Color(0, 0, 0, 0);
                    crosshair.color = new Color(1, 1, 1, 1);
                }
            }
            else
            {
                // Doesn't show anything
                panel.color = new Color(0, 0, 0, 0);
                actionText.color = new Color(0, 0, 0, 0);
                crosshair.color = new Color(1, 1, 1, 1);
            }
        }
        // Else if the player is not in gameplay mode
        else
        {
            // Doesn't show a crosshair
            panel.color = new Color(0, 0, 0, 0f);
            actionText.color = new Color(0, 0, 0, 0);
            crosshair.color = new Color(0, 0, 0, 0);
        }
    }
}
