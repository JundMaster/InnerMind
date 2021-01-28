using UnityEngine;

/// <summary>
/// Class responsible for creating an animation event.
/// </summary>
public class PianoKeyAnimationEvent : MonoBehaviour
{
    [SerializeField] private InteractionPianoKey pianoKey;

    /// <summary>
    /// Method called with animation event. Sets piano key CanPlay to true.
    /// </summary>
    public void CanPlayTrueAnimationEvent()
    {
        pianoKey.KeyAnimator.ResetTrigger("playKey");
        pianoKey.CanPlay = true;
    }
}
