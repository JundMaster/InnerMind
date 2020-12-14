using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDrawer : InteractionCommon
{
    [SerializeField] private Animator drawerAnimator;

    private bool isOpen;

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }


    /// <summary>
    /// This method determines the action of the piano key when clicked
    /// </summary>
    public override void Execute()
    {
        if(!isOpen)
        {
            SoundManager.PlaySound(SoundClip.DrawerOpen);
            drawerAnimator.SetTrigger("openDrawer");
            isOpen = true;
        }
        else if (isOpen)
        {
            SoundManager.PlaySound(SoundClip.DrawerClosing);
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
