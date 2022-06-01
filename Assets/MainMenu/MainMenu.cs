using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayTheGame()
    {
        SceneManagerAdapter.LoadSceneAsenkron("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
