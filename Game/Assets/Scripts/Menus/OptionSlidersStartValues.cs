using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class that sets sliders values on new scenes to desired value
/// </summary>
public class OptionSlidersStartValues : MonoBehaviour
{
    // Sliders
    [SerializeField] private Slider mouseSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Slider contrastSlider;

    // Variable to know if this is the first time the player is playing
    private int firstPlayInt;

    /// <summary>
    /// Start method for OptionSlidersStartValues
    /// </summary>
    private void Start()
    {
        // Player prefs with first play int
        firstPlayInt = PlayerPrefs.GetInt("First Play");

        // If it's the first time the user is opening the game
        if (firstPlayInt == 0)
        {
            PlayerPrefs.SetFloat("mouseSpeed", 30f);
            mouseSlider.value = 30f;

            PlayerPrefs.SetFloat("soundVolume", 0.8f);
            soundSlider.value = 0.8f;

            PlayerPrefs.SetFloat("musicVolume", 0.3f);
            musicSlider.value = 0.3f;

            PlayerPrefs.SetFloat("brightness", 0);
            brightnessSlider.value = 0;

            PlayerPrefs.SetFloat("contrast", 0);
            contrastSlider.value = 0;


            PlayerPrefs.SetInt("First Play", -1);
        }

        // If it's not the first the time the user is opening the game
        else
        {
            mouseSlider.value = PlayerPrefs.GetFloat("mouseSpeed");
            soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            brightnessSlider.value = PlayerPrefs.GetFloat("brightness");
            contrastSlider.value = PlayerPrefs.GetFloat("contrast");
        }
    }

    // Saves settings on PlayerPrefs
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("mouseSpeed", mouseSlider.value);
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("brightness", brightnessSlider.value);
        PlayerPrefs.SetFloat("contrast", contrastSlider.value);
    }

    /// <summary>
    /// OnApplicationFocus for OptionSlidersStartValues
    /// </summary>
    /// <param name="focus">If the application loses focus</param>
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            // Saves settings as they are at the moment
            SaveSettings();
        }
    }
}
