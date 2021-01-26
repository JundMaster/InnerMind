using UnityEngine;

/// <summary>
/// Responsible for controlling the puzzle of the room 6
/// </summary>
public class Room6 : PuzzleBase
{
    // Prize related variables in inspector
    [SerializeField] private GameObject prize;
    [SerializeField] private Animator drawerAnimator;
    [SerializeField] 
    private FrameTranslationInteraction[] frameTranslationInteractions;
    private ItemComparer itemComparer;

    #region Unity Functions
    /// <summary>
    /// OnEnable method for Room6.
    /// </summary>
    private void OnEnable()
    {
        for (int i = 0; i < frameTranslationInteractions.Length; i++)
        {
            frameTranslationInteractions[i].FramesChanged += SolutionCheck;
        }
        inventory.Bag.CollectionChanged += PrizedPicked;
    }

    /// <summary>
    /// Start method for Room6.
    /// </summary>
    private void Start()
    {
        itemComparer = FindObjectOfType<ItemComparer>();
        if (player.PuzzlesDone.HasFlag(myPuzzle))
        {
            this.Victory();
        }
    }

    /// <summary>
    /// OnDisable method for Room6.
    /// </summary>
    private void OnDisable()
    {
        for (int i = 0; i < frameTranslationInteractions.Length; i++)
        {
            frameTranslationInteractions[i].FramesChanged -= SolutionCheck;
        }
        inventory.Bag.CollectionChanged -= PrizedPicked;
    }

    #endregion

    /// <summary>
    /// Fires the animation for closing the drawer.
    /// </summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="e">Information about the event.</param>
    private void PrizedPicked(
        object sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        drawerAnimator.SetTrigger("CloseDrawer");
        SoundManager.PlaySound(SoundClip.DrawerClosing);
    }

    /// <summary>
    /// Does an action when the puzzle is solved.
    /// </summary>
    public override void Victory()
    {
        base.Victory();
        if (inventory.Bag.Contains(itemComparer.PianoKey3) == false)
        {
            prize.SetActive(true);
            drawerAnimator.SetTrigger("OpenDrawer");
            SoundManager.PlaySound(SoundClip.DrawerOpen);
        }

        for (int i = 0; i < frameTranslationInteractions.Length; i++)
        {
            frameTranslationInteractions[i].CanInteract = false;
            frameTranslationInteractions[i].
                 MoveToSolutionPoint(frameTranslationInteractions[i].Frame);
        }
    }

    /// <summary>
    /// Checks if the puzzle is solved.
    /// </summary>
    private void SolutionCheck()
    {
        int solutionCount = 0;
        for (int i = 0; i < frameTranslationInteractions.Length; i++)
        {
            if (frameTranslationInteractions[i].Frame.SolutionPosition ==
                frameTranslationInteractions[i].Frame.CurrentPosition)
            {
                solutionCount++;
            }
        }
        if (solutionCount == 3)
        {
            this.Victory();
        }
    }
}