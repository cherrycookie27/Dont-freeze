using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void Quit()
    {
        Application.Quit();
        SaveSystem.SavePlayer(Player.Instance);
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
