using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioClip;
    public Slider volumeSlider;

    GameObject menuManagerGameObject;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {
        menuManagerGameObject = GameObject.Find("MenuManagerCanvas(Clone)");
        menuManagerGameObject.GetComponent<MenuManager>().playDelegateAudioValueSave = OnAudioValueSave;

        // check slider delegate
        volumeSlider = menuManagerGameObject.transform.Find("UI/OptionsMenu/VolumeSlider").GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(delegate { OnAudioValueChange(); });

        // read saved volume
        audioClip.volume = PlayerPrefs.GetFloat("SliderVolumeLevel", audioClip.volume);
        // modify the position of slider value according to the saved value
        volumeSlider.value = audioClip.volume;
    }

    // response to happened delegate
    private void OnAudioValueChange()
    {
        audioClip.volume = volumeSlider.value;
    }

    private void OnAudioValueSave(bool isRestart)
    {
        if (isRestart)
        {
            PlayerPrefs.SetFloat("SliderVolumeLevel", audioClip.volume);  // save offline volume
        }
        else
        {
            audioClip.volume = volumeSlider.value;
        }
    }
}
