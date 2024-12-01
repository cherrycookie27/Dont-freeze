using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public static BossFight Instance;
    [SerializeField] GameObject cutEhScene;
    [SerializeField] GameObject oldObjective;
    [SerializeField] GameObject newObjective;
    [SerializeField] GameObject allRed;
    public bool canMove;
    private void Start()
    {
        canMove = false;
        cutEhScene.SetActive(false);
        newObjective.SetActive(false);
        allRed.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Time.timeScale = 0f;
            AudioManager.instance.musicSource.Stop();
            cutEhScene.SetActive(true);
            oldObjective.SetActive(false);
            newObjective.SetActive(true);
        }
    }

    public void BossTime()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlayMusic("BearFight");
        cutEhScene.SetActive(false);
        allRed.SetActive(true);
        gameObject.SetActive(false);
        canMove = true;
    }
}
