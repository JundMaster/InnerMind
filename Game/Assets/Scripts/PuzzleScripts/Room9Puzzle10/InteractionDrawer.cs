using UnityEngine;

/// <summary>
/// Classe for interaction with drawer
/// </summary>
public class InteractionDrawer : InteractionCommon
{
    // Editor variables
    [SerializeField] private Animator drawerAnimator;
    [SerializeField] private bool isOpen;

    /// <summary>
    /// This method determines the action of the piano key when clicked
    /// </summary>
    public override void Execute()
    {
        if(!isOpen)
        {
            drawerAnimator.ResetTrigger("closeDrawer");
            drawerAnimator.SetTrigger("openDrawer");
            isOpen = true;
        }
        else if (isOpen)
        {
            drawerAnimator.ResetTrigger("openDrawer");
            drawerAnimator.SetTrigger("closeDrawer");
            isOpen = false;
        }  
    }

    /// <summary>
    /// This method overrides ToString, and it determines what the player sees
    /// when the Crosshair is on top of this object
    /// </summary>
    /// <returns>Returns a string with an action</returns>
    public override string ToString() => isOpen ? "Close Drawer" : "Open Drawer";
}
