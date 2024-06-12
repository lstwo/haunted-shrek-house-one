using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Assigns")]
    public AudioMixer mixer;
    public TextMeshProUGUI senitivityPercentage, volumePercentage;

    [Header("Funny Numbers")]
    public static float sensitivity = 5;

    

    public void SetSensitivity(float value)
    {
        sensitivity = value / 5;
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
}
