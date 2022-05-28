using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
