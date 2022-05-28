using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseInGameMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
