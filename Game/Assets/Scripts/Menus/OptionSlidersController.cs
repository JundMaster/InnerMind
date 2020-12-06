using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlidersController : MonoBehaviour
{
    public void AdjustMouseSpeed(float speed)
    {
        PlayerPrefs.SetFloat("mouseSpeed", PlayerInput.MouseSpeed = speed);
    }
    public void AdjustSoundVolume(float sound)
    {

    }
    public void AdjustMusicVolume(float music)
    {

    }
}
