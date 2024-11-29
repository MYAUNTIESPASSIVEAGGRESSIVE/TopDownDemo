using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMenuManager : MonoBehaviour
{
    public void MainMenuButton()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void UnPause()
    {
        Time.timeScale = 1;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
