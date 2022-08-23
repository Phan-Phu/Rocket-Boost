using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneMenu : SingletonMonobehaviour<LoadSceneMenu>
{
    private string _sceneName;
    public string SceneName { get { return _sceneName; } set { _sceneName = value; } }

    [SerializeField] private Animator sceneMenu;
    [SerializeField] private Animator sceneLevel;
    private float speedLoadAnimation = 1f;

    [HideInInspector] public bool blockLevel = false;

    public void LoadScene(string sceneName)
    {
        _sceneName = sceneName;
        SceneManager.LoadScene("PersistentScene");
    }

    public void PlayGame()
    {
        // if file json is nothing or not appear => error script (solution press S)
        EventHandler.CallLoadGameEvent();

        sceneMenu.SetFloat("LoadScene", speedLoadAnimation);
        sceneLevel.SetFloat("LoadScene", -speedLoadAnimation);

        blockLevel = true;
    }

    public void BackScene()
    {
        sceneMenu.SetFloat("LoadScene", -speedLoadAnimation);
        sceneLevel.SetFloat("LoadScene", speedLoadAnimation);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
