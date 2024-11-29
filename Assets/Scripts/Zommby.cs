using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zommby : MonoBehaviour
{
    public static Zommby instance;

    public int maxHealth = 3;

    public GameObject player;
    public Animator anim;

    public float patrolRadius;
    public float speed;
    public float attackDistance;
    public int followDistance = 5;

    private bool pleaseStop;
    private bool isAttacking = false;
    private float distance;
    private int currentHealth;
    private bool knockBack;
    private bool following;
    private bool roaming;

    private Vector2 knockBackTarget;
    private Vector2 lastDirection;
    private Vector2 patrolCenter;
    private Vector2 patrolTarget;
    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();

        if (instance == null)
            instance = this;

        lastDirection = Vector2.down;

        patrolCenter = transform.position;
        float x = Random.Range(-patrolRadius, patrolRadius);
        float y = Random.Range(-patrolRadius, patrolRadius);
        patrolTarget = new Vector2(x, y) + patrolCenter;
        anim.SetBool("isMoving", false);
        Invoke("Roam", 3.6f);
    }

    private void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        if (knockBack)
        {
            transform.position = Vector2.Lerp(transform.position, knockBackTarget, 10f * Time.fixedDeltaTime);
            if (Vector2.Distance(transform.position, knockBackTarget) < 1)
            {
                knockBack = false;
                pleaseStop = false;
            }
        }
        if (distance < followDistance && !pleaseStop)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            UpdateDirection(direction);
            following = true;
            if (distance < attackDistance && !isAttacking)
            {
                StartCoroutine(Attack(player.GetComponent<Player>()));
                isAttacking = true;
                pleaseStop = true;
            }
        }

        if (distance > followDistance)
        {
           if (following)
            {
                following = false;
                patrolCenter = transform.position;
                float x = Random.Range(-patrolRadius, patrolRadius);
                float y = Random.Range(-patrolRadius, patrolRadius);
                patrolTarget = new Vector2(x, y) + patrolCenter;
                anim.SetBool("isMoving", false);
                Invoke("Roam", 3.6f);
            }
           if (roaming)
            {
                Vector2 roamDirection = patrolTarget - (Vector2)transform.position;
                transform.position = Vector2.MoveTowards(this.transform.position, patrolTarget, speed * Time.deltaTime);
                UpdateDirection(roamDirection);
            }
            if (Vector2.Distance(transform.position, patrolTarget) < 1)
            {
                patrolCenter = transform.position;
                float x = Random.Range(-patrolRadius, patrolRadius);
                float y = Random.Range(-patrolRadius, patrolRadius);
                patrolTarget = new Vector2(x, y) + patrolCenter;
                roaming = false;
                anim.SetBool("isMoving", false);
                Invoke("Roam", 3.6f);
            }
        }

    }

    private void Roam()
    {
        roaming = true;
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

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pleaseStop = false;
        }
    }

    IEnumerator Attack(Player player)
    {
        anim.SetTrigger("EnemyAttack");
        anim.SetFloat("LastHorizontal", lastDirection.x);
        anim.SetFloat("LastVertical", lastDirection.y);
        player.TakeDamage(1);
        //SoundManager.instance.PlaySFX("ZombieBite");

        yield return new WaitForSeconds(2);
        isAttacking = false;
        pleaseStop = false;
    }

    public void PlayerAttacking(int amount, Vector2 dir)
    {
        currentHealth -= amount;
        //SoundManager.instance.PlaySFX("ZombieHit");
        knockBackTarget = dir.normalized * 4 + (Vector2)transform.position;
        knockBack = true;
        if (currentHealth < 1)
        {
            pleaseStop = true;
            anim.SetTrigger("zombieDying");
        }
    }

    public void DestroyZombie()
    {
        Destroy(gameObject);
    }
}

