using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button playButton;
    public Button optionsButton;
    public Button quitButton;
    public Button backButton;
    public Button optionsQuitButton;
    public Button restartButton;
    public Button finishQuitButton;

    public delegate void PlayDelegate(bool isRestart);
    public PlayDelegate playDelegate;
    public PlayDelegate playDelegateAudioValueSave;

    bool toggleOptions = false;

    // Start is called before the first frame update
    void Start()
    {
        ButtonListen();
    }

    void ButtonListen()
    {
        // Main Menu
        quitButton = transform.Find("UI/MainMenu/QuitButton").GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);

        playButton = transform.Find("UI/MainMenu/PlayButton").GetComponent<Button>();
        playButton.onClick.AddListener(PlayGame);

        optionsButton = transform.Find("UI/MainMenu/OptionsButton").GetComponent<Button>();
        optionsButton.onClick.AddListener(OnOptions);

        // Options Menu
        backButton = transform.Find("UI/OptionsMenu/BackButton").GetComponent<Button>();
        backButton.onClick.AddListener(BackToGame);

        optionsQuitButton = transform.Find("UI/OptionsMenu/QuitButton").GetComponent<Button>();
        optionsQuitButton.onClick.AddListener(QuitGame);

        // Finish Menu
        restartButton = transform.Find("UI/FinishMenu/RestartButton").GetComponent<Button>();
        restartButton.onClick.AddListener(RestartGame);

        finishQuitButton = transform.Find("UI/FinishMenu/QuitButton").GetComponent<Button>();
        finishQuitButton.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        CheckOptionsInGame();
    }

    void CheckOptionsInGame()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) && transform.Find("UI/MainMenu").gameObject.activeSelf == false) 
            && (Input.GetKeyDown(KeyCode.Escape) && transform.Find("UI/FinishMenu").gameObject.activeSelf == false))
        {
            transform.Find("UI").gameObject.SetActive(true);

            transform.Find("UI/OptionsMenu").gameObject.SetActive(!toggleOptions);
            transform.Find("UI/OptionsMenu/Background").gameObject.SetActive(toggleOptions);
        }
    }

    private void PlayGame()
    {
        transform.Find("UI/MainMenu").gameObject.SetActive(false);
        transform.Find("UI/OptionsMenu/QuitButton").gameObject.SetActive(true);
        transform.Find("UI/OptionsMenu").gameObject.SetActive(false);
        transform.Find("UI/FinishMenu").gameObject.SetActive(false);
        playDelegate?.Invoke(false); // if subscribed, then invoke
    }

    private void OnOptions()
    {
        GameObject go = transform.Find("UI/MainMenu").gameObject;
        go.SetActive(false);
        //transform.Find("UI/MainMenu").gameObject.SetActive(false);
        transform.Find("UI/OptionsMenu").gameObject.SetActive(true);
        transform.Find("UI/FinishMenu").gameObject.SetActive(false);

        transform.Find("UI/OptionsMenu/QuitButton").gameObject.SetActive(false);
    }

    private void QuitGame()
    {
        playDelegateAudioValueSave?.Invoke(true); // if subscribed, then invoke
        Debug.Log("Quit");
        Application.Quit();
    }

    private void BackToGame()
    {
        transform.Find("UI/OptionsMenu").gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "SceneStart")
        {
            transform.Find("UI/MainMenu").gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        transform.Find("UI/MainMenu").gameObject.SetActive(true);
        transform.Find("UI/OptionsMenu").gameObject.SetActive(false);
        transform.Find("UI/FinishMenu").gameObject.SetActive(false);
        playDelegate?.Invoke(true); // if subscribed, then invoke
        playDelegateAudioValueSave?.Invoke(true); // if subscribed, then invoke
    }

    //private void OnDestroy() {}
}
