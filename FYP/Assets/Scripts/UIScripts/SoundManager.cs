using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    [SerializeField] Slider sensitivitySlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }


    [HideInInspector] public float mouseSensitivity = 0.1f;
    public void SaveSensitivity()
    {
        mouseSensitivity = sensitivitySlider.value;
        ChangeSensitivity();
    }

    public void LoadSensitivity()
    {
        volumeSlider.value = mouseSensitivity;
    }

    private void ChangeSensitivity()
    {
        if (UIScripts.instance && UIScripts.instance.mouseLook)
            UIScripts.instance.mouseLook.ChangeMouseSensitivity(mouseSensitivity);
    }


}
