using System.Collections;
using UnityEngine;

/// <summary>
/// Class for PlayerSounds
/// </summary>
public class PlayerSounds : MonoBehaviour
{
    // Variable to control footstep coroutine
    private Coroutine footstepCoroutine;

    // Components
    private PlayerMovement movement;
    private PlayerInput input;

    /// <summary>
    /// Awake method for PlayerSounds
    /// </summary>
    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        input = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// Update method for PlayerSounds
    /// </summary>
    private void Update()
    {
        // Plays footstep sounds
        if (input.CurrentControl == TypeOfControl.InGameplay)
        {
            if (movement.Movement.magnitude > 0)
            {
                if (footstepCoroutine == null)
                    footstepCoroutine = StartCoroutine(Footstep());
            }
        }
    }

    /// <summary>
    /// Coroutine to play footstep sounds
    /// </summary>
    /// <returns>Returns new waitforseconds</returns>
    private IEnumerator Footstep()
    {
        SoundManager.PlaySound(SoundClip.footstep);
        yield return new WaitForSeconds(0.5f);
        footstepCoroutine = null;
    }
}
