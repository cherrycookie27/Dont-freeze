using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Continue()
    {
        //IDONTKNOW
    }
    public void GameNew()
    {
        SceneManager.LoadScene("Game");
    }
}
