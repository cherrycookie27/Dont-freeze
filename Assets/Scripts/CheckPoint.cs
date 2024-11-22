using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool canSave;
    private SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }
    private void Update()
    {
        if (canSave && Input.GetKey(KeyCode.E))
        {
            //save game
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        sprite.enabled = true;
        canSave = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        sprite.enabled = false;
        canSave = false;
    }
}
