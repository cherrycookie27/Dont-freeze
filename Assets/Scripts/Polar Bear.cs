using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PolarBear : MonoBehaviour
{
    public int maxHealth = 20;

    public GameObject player;
    public Animator anim;

    public float minChargeCooldown = 5f;
    public float maxChagrgeCooldown = 10f;
    public float chargeSpeed;
    public float speed;
    public float maxChargeDistance = 5f;

    private bool isCharging;
    private bool pleaseStop;
    private bool isAttacking = false;
    private float distance;
    private float nectChargeTime;
    private int currentHealth;

    private Vector2 lastDirection;
    private Vector2 chargeTarget;
    private Vector2 chargeStartPosition;
    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();

        lastDirection = Vector2.down;
        ScheduleNextCharge();
    }

    private void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (!pleaseStop)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            UpdateDirection(direction);
            if (Time.time >= nectChargeTime)
            {
                StartCoroutine(StartCharge());
            }
        }

        if (isCharging)
        {
            // Charging toward the player's last known position
            transform.position = Vector2.MoveTowards(transform.position, chargeTarget, chargeSpeed * Time.deltaTime);
            if (Vector2.Distance(chargeStartPosition, transform.position) >= maxChargeDistance || Vector2.Distance(transform.position, chargeTarget) < 0.5f)
            {
                isCharging = false;
                pleaseStop = false;
            }
            return;
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

    private void ScheduleNextCharge()
    {
        nectChargeTime = Time.time + Random.Range(minChargeCooldown, maxChagrgeCooldown);
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

    IEnumerator StartCharge()
    {
        pleaseStop = true;
        anim.SetTrigger("prepareForCharge");

        yield return new WaitForSeconds(1.5f);

        pleaseStop = false;

        chargeStartPosition = transform.position;
        chargeTarget = player.transform.position;
        isCharging = true;
        anim.SetTrigger("charge");

        ScheduleNextCharge();
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