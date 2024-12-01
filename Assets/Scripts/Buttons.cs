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
    public void Game()
    {
        SceneManager.LoadScene("Game");
        AudioManager.instance.musicSource.Stop();
        AudioManager.instance.PlayMusic("Game");
    }
}
