using UnityEngine;
/// <summary>
/// Responsible for controlling the <see cref="ItemExaminer"/> 
/// </summary>
public class ExaminerController : MonoBehaviour
{
    private ItemExaminer itemExaminer;
    private void Update()
    {
        if (itemExaminer != null)
            itemExaminer.Examine();
    }

    /// <summary>
    /// Sets an instance of the <see cref="ItemExaminer"/>
    /// </summary>
    /// <param name="examiner"></param>
    public void SetExaminer(ItemExaminer examiner)
    {
        itemExaminer = examiner;
    }

    /// <summary>
    /// Destroys the <see cref="ItemExaminer"/> that is being controlled
    /// </summary>
    public void DestroyExaminer()
    {
        if (itemExaminer != null)
            itemExaminer.StopExamine();
        itemExaminer = null;
    }
}
