using UnityEngine;
using Cinemachine;

/// <summary>
/// Class for player camera/look
/// </summary>
public class PlayerLook : MonoBehaviour
{
    /// <summary>
    /// Property for vertical rotation of the camera
    /// </summary>
    public float VerticalRotation { get; set; }

    private float horizontalRotation;

    // Components
    private Transform player;
    private IPlayerInput input;
    private PlayerMovement movement;
    private CinemachineBasicMultiChannelPerlin cam;

    /// <summary>
    /// Awake method for PlayerLook
    /// </summary>
    private void Awake()
    {
        input = GetComponentInParent<PlayerInput>();
        movement = GetComponentInParent<PlayerMovement>();
        cam = GetComponent<CinemachineVirtualCamera>().
            GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // player Transform is the same as this class's parent
        player = transform.parent.transform;

        VerticalRotation = 0f;
    }

    /// <summary>
    /// Update method for PlayerLook. Shakes camera while walking.
    /// </summary>
    private void Update()
    {
        if (input.CurrentControl == TypeOfControl.InGameplay)
        {
            if (movement?.Movement.magnitude > 0)
            {
                cam.m_FrequencyGain = 1.5f;
            }
            else
            {
                cam.m_FrequencyGain = 0.3f;
            }
        }
        else
        {
            cam.m_FrequencyGain = 0.3f;
        }
    }

    /// <summary>
    /// LateUpdate for PlayerLook
    /// </summary>
    private void LateUpdate()
    {
        if (input.CurrentControl == TypeOfControl.InGameplay)
        {
            // Clamps vertical axis
            VerticalRotation -= input.VerticalMouse * 
                input.MouseSpeed * Time.deltaTime;
            VerticalRotation = Mathf.Clamp(VerticalRotation, -70f, 70f);

            // Rotates the camera (vertical) around X axis
            transform.localRotation = Quaternion.Euler(VerticalRotation, 0f, 0f);

            // Rotates the player on Y axis (horizontal)
            player.Rotate(Vector3.up * input.HorizontalMouse * 
                input.MouseSpeed * Time.deltaTime);
        }
        else if (input.CurrentControl == TypeOfControl.InCutscene)
        {
            // Clamps vertical axis
            VerticalRotation -= input.VerticalMouse *
                input.MouseSpeed * Time.deltaTime;
            VerticalRotation = Mathf.Clamp(VerticalRotation, -20f, 20f);

            // Clamps horizontal axis
            horizontalRotation += input.HorizontalMouse * 
                input.MouseSpeed * Time.deltaTime;
            horizontalRotation = Mathf.Clamp(horizontalRotation, -20f, 20f);

            // Rotates with a limit position
            transform.localRotation = Quaternion.Euler(VerticalRotation,
                horizontalRotation, 0f);
        }
    }
}
