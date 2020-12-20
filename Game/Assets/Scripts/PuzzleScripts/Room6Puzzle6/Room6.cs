using UnityEngine;

/// <summary>
/// Responsible for controlling the puzzle of the room 6
/// </summary>
public class Room6 : PuzzleBase
{
    // Prize related variables in inspector
    [SerializeField] private GameObject prize;
    [SerializeField] private Animator drawerAnimator;

    // Components
    private PictureFramePuzzleParent pictureFramePuzzleParent;
    private ItemComparer itemComparer;

    #region Unity Functions
    /// <summary>
    /// Start method for Room6
    /// </summary>
    private void Start()
    {
        pictureFramePuzzleParent = FindObjectOfType<PictureFramePuzzleParent>();
        itemComparer = FindObjectOfType<ItemComparer>();

        inventory.Bag.CollectionChanged += PrizedPicked;

        if (player.PuzzlesDone.HasFlag(myPuzzle))
        {
            this.Victory();
        }
    }

    private void OnDisable()
    {
        //pictureFramePuzzleParent.PuzzleSolved -= Victory;
        inventory.Bag.CollectionChanged -= PrizedPicked;
    }

    #endregion

    /// <summary>
    /// Fires the animation for closing the drawer
    /// </summary>
    /// <param name="sender">The object that raised the event</param>
    /// <param name="e">Information about the event</param>
    private void PrizedPicked(
        object sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        drawerAnimator.SetTrigger("CloseDrawer");
    }

    /// <summary>
    /// Does an action when the puzzle is solved
    /// </summary>
    public override void Victory()
    {
        base.Victory();
        if (inventory.Bag.Contains(itemComparer.PianoKey3) == false)
        {
            prize.SetActive(true);
            drawerAnimator.SetTrigger("OpenDrawer");
        }

        for (int i  = 0; i < pictureFramePuzzleParent.FramePictures.Length; i++)
        {
            Destroy(pictureFramePuzzleParent.FramePictures[i].
                GetComponentInChildren<TranslateInteractionPictureFrame>());
        }
    }
}