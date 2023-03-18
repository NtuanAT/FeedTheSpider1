using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;
    public Slider masterVolumn;
    public Slider sfxVolumn;
    public Slider musicVolumn;
    public Toggle isFullScreen;
    // Start is called before the first frame update
    void Start()
    {
        
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " @" + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if (resolutions[i].width == Screen.width 
                && resolutions[i].height==Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        audioMixer.GetFloat("MasterVolume", out float master);
        masterVolumn.value = Mathf.Pow(10,(float)master/20f);
		audioMixer.GetFloat("SFXVolume", out float sfx);
		sfxVolumn.value = Mathf.Pow(10, (float)sfx / 20f); 
		audioMixer.GetFloat("MusicVolume", out float music);
		musicVolumn.value = Mathf.Pow(10, (float)music / 20f);

        isFullScreen.isOn = Screen.fullScreen;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
	}

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
	public void SetSFXVolume(float volume)
	{
		audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
	}
	public void SetMusicVolume(float volume)
	{
		audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
	}
}
