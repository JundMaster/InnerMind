using UnityEngine;
public class Examiner : MonoBehaviour
{
    private ItemExaminer itemExaminer;
    [SerializeField] private Camera renderingCamera;
    private void Update()
    {
        if (itemExaminer != null)
            itemExaminer.Examine();
    }

    // Sets a instance of the ItemExaminer
    // Way to avoid an instance to be created every frame
    public void SetExaminer(ItemExaminer examiner)
    {
        itemExaminer = examiner;
    }

    public void DestroyExaminer()
    {
        if (itemExaminer != null)
            itemExaminer.StopExamine();
        itemExaminer = null;
        Debug.Log("DESTROYED");
    }
}
