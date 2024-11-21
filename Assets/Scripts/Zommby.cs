using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zommby : MonoBehaviour
{
    private bool isAttacking = false; 
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack(other.gameObject.GetComponent<Player>()));
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
            StopAllCoroutines(); 
        }
    }

    IEnumerator Attack(Player player)
    {
        while (isAttacking)
        {
            player.TakeDamage(1); 
            yield return new WaitForSeconds(2); 
        }
    }
}
