using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public Player player = null;

    GameObject gameManagerGameObject;
    GameObject audioManagerGameObject;
    GameObject menuManagerGameObject;

    void Start()
    {
        Init();
    }

    // Initialization
    void Init() 
    {
        gameManagerGameObject = GameObject.Find("GameManager");
        audioManagerGameObject = GameObject.Find("AudioManager(Clone)");
        menuManagerGameObject = GameObject.Find("MenuManagerCanvas(Clone)");
        //canvas = menuManager.GetComponent<Canvas>();
        player.successDelegate = LoadNextLevel;

        // subscribe delegate
        //menuManager.GetComponent<MenuManager>().playDelegateEvent += LoadNextLevel;
        menuManagerGameObject.GetComponent<MenuManager>().playDelegate = LoadNextLevel;
    }

    public void LoadNextLevel(bool isRestart)
    {
        if (isRestart)
        {
            SceneManager.MoveGameObjectToScene(gameManagerGameObject, SceneManager.GetActiveScene());
            SceneManager.MoveGameObjectToScene(menuManagerGameObject, SceneManager.GetActiveScene());
            SceneManager.MoveGameObjectToScene(audioManagerGameObject, SceneManager.GetActiveScene());
            Destroy(gameManagerGameObject);
            Destroy(menuManagerGameObject);
            Destroy(audioManagerGameObject);

            StartCoroutine(LoadLevel(0));

        }
        else
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            Debug.Log(SceneManager.sceneCountInBuildSettings);

        }
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");

        // wait
        // pause this coroutine and WaitForSeconds, after it, the coroutine will continue
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);

        if (SceneManager.GetActiveScene().buildIndex + 1 + 1 == SceneManager.sceneCountInBuildSettings)
        {
            menuManagerGameObject.transform.Find("UI").gameObject.SetActive(true);
            menuManagerGameObject.transform.Find("UI/MainMenu").gameObject.SetActive(false);
            menuManagerGameObject.transform.Find("UI/OptionsMenu").gameObject.SetActive(false);
            menuManagerGameObject.transform.Find("UI/FinishMenu").gameObject.SetActive(true);
        }
        else
        {
            menuManagerGameObject.transform.Find("UI").gameObject.SetActive(false);
        }
    }
}
