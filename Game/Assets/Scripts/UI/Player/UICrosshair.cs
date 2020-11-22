using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICrosshair : MonoBehaviour
{
    // Text to print action
    [SerializeField] private GameObject actionTextGameObject;
    private TextMeshProUGUI actionText;

    // Components
    private PlayerRays ray;
    private PlayerInteract interact;
    private Image crosshair;
    private PlayerInput input;


    void Start()
    {
        actionText = actionTextGameObject.GetComponent<TextMeshProUGUI>();
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
                    if (actionTextGameObject.activeSelf == false)
                        actionTextGameObject.SetActive(true);

                    if (actionText)
                        actionText.text = other.ToString();

                    crosshair.color = new Color(1, 0, 0, 1);
                }
                else
                {
                    if (actionTextGameObject.activeSelf)
                        actionTextGameObject.SetActive(false);

                    crosshair.color = new Color(1, 1, 1, 1);
                }
            }
            else
            {
                if (actionTextGameObject.activeSelf)
                    actionTextGameObject.SetActive(false);
                crosshair.color = new Color(1, 1, 1, 1);
            }
        }
        else
        {
            if (actionTextGameObject.activeSelf)
                actionTextGameObject.SetActive(false);

            crosshair.color = new Color(0, 0, 0, 0);
        }
    }
}
