using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PolarBear : MonoBehaviour
{
    public int maxHealth = 20;

    public GameObject player;
    public Animator anim;

    public float speed;

    private bool pleaseStop;
    private bool isAttacking = false;
    private float distance;
    private int currentHealth;

    private Vector2 lastDirection;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();

        lastDirection = Vector2.down;
    }

    private void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (!pleaseStop)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            UpdateDirection(direction);
        }

        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void UpdateDirection(Vector2 direction)
    {
        if (direction.magnitude > 0.1f)
        {
            lastDirection = direction;
            anim.SetBool("isMoving", true);

            anim.SetFloat("Horizontal", lastDirection.x);
            anim.SetFloat("Vertical", lastDirection.y);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack(other.gameObject.GetComponent<Player>()));
            pleaseStop = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
            StopAllCoroutines();
            pleaseStop = false;
        }
    }

    IEnumerator Attack(Player player)
    {
        while (isAttacking)
        {
            anim.SetTrigger("EnemyAttack");
            anim.SetFloat("LastHorizontal", lastDirection.x);
            anim.SetFloat("LastVertical", lastDirection.y);
            player.TakeDamage(1);

            yield return new WaitForSeconds(3);
        }
    }

    public void PlayerAttacking(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 1)
        {
            pleaseStop = true;
            anim.SetTrigger("bearDying");
        }
    }

    IEnumerator DestroyBear()
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("WinScreen");
    }
}