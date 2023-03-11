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
    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width 
                && resolutions[i].height==Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        audioMixer.GetFloat("MasterVolumn", out float master);
        masterVolumn.value = master;
		audioMixer.GetFloat("SFXVolumn", out float sfx);
		sfxVolumn.value = sfx;
		audioMixer.GetFloat("MusicVolumn", out float music);
		musicVolumn.value = music;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
            
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetMasterVolumn(float volumn)
    {
        audioMixer.SetFloat("MasterVolumn", volumn);
    }
	public void SetSFXVolumn(float volumn)
	{
		audioMixer.SetFloat("SFXVolumn", volumn);
	}
	public void SetMusicVolumn(float volumn)
	{
		audioMixer.SetFloat("MusicVolumn", volumn);
	}
}
