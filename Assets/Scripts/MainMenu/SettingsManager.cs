using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public static float sensitivity = 10;

    public AudioMixer mixer;

    public TextMeshProUGUI senitivityPercentage, volumePercentage;

    public void SetSensitivity(float value)
    {
        sensitivity = value;
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
