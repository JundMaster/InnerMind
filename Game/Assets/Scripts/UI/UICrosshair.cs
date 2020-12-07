using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UICrosshair : MonoBehaviour
{
    // Text to print action
    [SerializeField] private GameObject actionTextGameObject;
    private TextMeshProUGUI actionText;
    private Image panel;

    // Components
    private PlayerRays ray;
    private PlayerInteract interact;
    private Image crosshair;
    private PlayerInput input;

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

    // Update is called once per frame
    void Update()
    {
        if (input.CurrentControl == TypeOfControl.InGameplay)
        {
            if (Physics.Raycast(ray.RayToMouse, out RaycastHit hit,
                    interact.InteractDistance))
            {
                if (hit.collider.gameObject.TryGetComponent
                    (out IInteract other))
                {
                    panel.color = new Color(0, 0, 0, 0.4f);
                    actionText.text = other.ToString();
                    actionText.color = new Color(1, 1, 1, 1);
                    crosshair.color = new Color(1, 0, 0, 1);          
                }
                else
                {
                    panel.color = new Color(0, 0, 0, 0);
                    actionText.color = new Color(0, 0, 0, 0);
                    crosshair.color = new Color(1, 1, 1, 1);
                }
            }
            else
            {
                panel.color = new Color(0, 0, 0, 0);
                actionText.color = new Color(0, 0, 0, 0);
                crosshair.color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            panel.color = new Color(0, 0, 0, 0f);
            actionText.color = new Color(0, 0, 0, 0);
            crosshair.color = new Color(0, 0, 0, 0);
        }
    }
}
