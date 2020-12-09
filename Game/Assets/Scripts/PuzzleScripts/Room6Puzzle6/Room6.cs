using System.Collections;
using UnityEngine;

public class Room6 : PuzzleBase
{
    // Prize related variables in inspector
    [SerializeField] private GameObject prize;
    [SerializeField] private ScriptableItem prizeScriptableItem;
    [SerializeField] private Animator drawerAnimator;
    private bool isPrizePicked;

    private PictureFramePuzzleParent pictureFramePuzzleParent;

    private void Bag_CollectionChanged(
        object sender,
        System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        drawerAnimator.SetTrigger("CloseDrawer");
    }

    private void Start()
    {
        pictureFramePuzzleParent = FindObjectOfType<PictureFramePuzzleParent>();
        pictureFramePuzzleParent.PuzzleSolved += Victory;
        inventory.Bag.CollectionChanged += Bag_CollectionChanged;
        isPrizePicked = false;
    }

    public override void Victory()
    {
        base.Victory();
        drawerAnimator.SetTrigger("OpenDrawer");
    }
}