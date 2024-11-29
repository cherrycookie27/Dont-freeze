using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject interract;
    [SerializeField] GameObject door;
    [SerializeField] GameObject roof;
    [SerializeField] GameObject frontWall;

    private bool doorOpen;
    private bool canOpen;
    private void Awake()
    {
        interract.SetActive(false);
        canOpen = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canOpen)
        {
            door.SetActive(false);
            roof.SetActive(false);
            interract.SetActive(false);
            Color c = Color.white;
            c.a = 0.1f;
            frontWall.GetComponent<SpriteRenderer>().color = c;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !doorOpen)
        {
            interract.SetActive(true);
            canOpen = true;
        }
        if (other.gameObject.CompareTag("Player") && !doorOpen)
        {
             door.SetActive(true);
             roof.SetActive(true);
             Color c = Color.white;
             c.a = 1f;
             frontWall.GetComponent<SpriteRenderer>().color = c;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canOpen = false;
            interract.SetActive(false);
        }
    }
}
