using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsController : MonoBehaviour
{
    public TMPro.TMP_Dropdown resolutionsDropdown;
    public AudioMixer audioMixer;
    [Header("Tabs")]
    public GameObject[] tabs;
    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

       int CurrentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++) 
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                CurrentResolution = i;
            }
           
        } 

        resolutionsDropdown.AddOptions(options);    
    }
    public void SetVolumeMaster (float volumemaster)
    {
        audioMixer.SetFloat("volume Master", volumemaster);
    }

    public void SetVolumeSFX(float volumesfx)
    {
        audioMixer.SetFloat("volume SFX",volumesfx);
    }

    public void SetVolumeMusic(float volumemusic)
    {
        audioMixer.SetFloat("volume Music",volumemusic);
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);   
    }

    public void SetFullScreen (bool isFullscreen)
    {
        Screen.fullScreen= isFullscreen;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void changeTabs (int index)
    {
        for (int i = 0; i < tabs.Length; i++)
        {
            if (i == index)
            {
                tabs[i].SetActive(true);
            }
            else
            {
                tabs[i].SetActive(false);
            }
        }
    }
}
