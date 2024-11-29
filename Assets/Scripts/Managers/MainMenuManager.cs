using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }

    public void TestScene()
    {
        SceneManager.LoadSceneAsync("TestScene");
    }


    public void QuitButton()
    {
        Application.Quit();
    }
}
