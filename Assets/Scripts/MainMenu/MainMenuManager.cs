using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Assigns")]
    public AudioMixer mixer;
    public TextMeshProUGUI senitivityPercentage, volumePercentage;
    public TMP_Dropdown resolutionDropdown;

    public void SetSensitivity(float value)
    {
        GameManager.sensitivity = value / 5;
        senitivityPercentage.text = ((int)value).ToString() + "%";
    }

    public void SetVolume(float value)
    {
        mixer.SetFloat("Volume", value - 80);
        volumePercentage.text = ((int)value).ToString() + "%";
    }

    public void Play()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }

    public void SetFullscreen(bool value)
    {
        if(value)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        } else
        {
            Screen.SetResolution(1280, 720, false);
        }
    }
}