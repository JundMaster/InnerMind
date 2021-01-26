using UnityEngine;
using System.Linq;

public class Frame : MonoBehaviour
{
    [SerializeField]
    private FramePosition solutionPosition;
    [SerializeField]
    private FramePosition initialPosition;
    [SerializeField]
    private FrameMoveModifier[] frames;
    
    /// <summary>
    /// Gets or sets the current position of the frame.
    /// </summary>
    public FramePosition CurrentPosition { get; set; }

    /// <summary>
    /// Gets the position of the frame in which it will be consired as solved.
    /// </summary>
    public FramePosition SolutionPosition => solutionPosition;

    /// <summary>
    /// Gets or sets the frames linked to this.
    /// </summary>
    public Frame[] LinkedFrames { get; private set; }

    /// <summary>
    /// Start method for Frame.
    /// </summary>
    private void Start()
    {
        // Orders the linked frames by its moveOrder
        LinkedFrames = 
            frames.OrderBy(f => f.moveOrder).Select(f => f.frame).ToArray();
        CurrentPosition = initialPosition;
    }
}
