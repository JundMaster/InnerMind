using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlidersStartValues : MonoBehaviour
{
    [SerializeField] private Slider mouseSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;

    private int firstPlayInt;

    private void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt("First Play");

        if (firstPlayInt == 0)
        {
            PlayerPrefs.SetFloat("mouseSpeed", 300f);
            mouseSlider.value = 300f;

            PlayerPrefs.SetInt("First Play", -1);
        }

        else
        {
            mouseSlider.value = PlayerPrefs.GetFloat("mouseSpeed");
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("mouseSpeed", mouseSlider.value);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            SaveSoundSettings();
        }
    }
}
