using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicles : MonoBehaviour
{
    float randChance = Random.value;
    private Animator anim;
    private Player player;
    private AudioSource iciclefall;
    Zommby zommby;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (randChance < .60f)
        {
            //dont fall
        }
        else
        {
            // fall
            anim.SetTrigger("KYS");
            if (other.gameObject.CompareTag("Player"))
            {
                iciclefall.Play();
                player.TakeDamage(3);
            }
            else if (other.gameObject.CompareTag("Enemy"))
            {
                iciclefall.Play();
                //zommby.PlayerAttacking(3);
            }
        }
    }
}
