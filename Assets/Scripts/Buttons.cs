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
        //SoundManager.instance.musicSource.Stop();
        //SoundManager.instance.PlayMusic("Game");
    }
}
