using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PolarBear : MonoBehaviour, IEnemy
{
    public int maxHealth = 20;
    public Transform Transform => this.transform;

    public GameObject player;
    public Animator anim;

    public float minChargeCooldown = 5f;
    public float maxChagrgeCooldown = 10f;
    public float chargeSpeed;
    public float speed;
    public float maxChargeDistance = 5f;
    public float attackDistance;

    private bool knockBack;
    private bool isCharging;
    private bool pleaseStop;
    private bool isAttacking = false;
    private float distance;
    private float nectChargeTime;
    private int currentHealth;

    private Vector2 knockBackTarget;
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

        if (knockBack)
        {
            transform.position = Vector2.Lerp(transform.position, knockBackTarget, 1f * Time.fixedDeltaTime);
            if (Vector2.Distance(transform.position, knockBackTarget) < 1)
            {
                knockBack = false;
                pleaseStop = false;
            }
        }
        if (BossFight.Instance.canMove)
        {
            if (!pleaseStop)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
                UpdateDirection(direction);
                if (Time.time >= nectChargeTime)
                {
                    StartCoroutine(StartCharge());
                }
                if (distance < attackDistance && !isAttacking)
                {
                    StartCoroutine(Attack(player.GetComponent<Player>()));
                    isAttacking = true;
                    pleaseStop = true;
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

    private void ScheduleNextCharge()
    {
        nectChargeTime = Time.time + Random.Range(minChargeCooldown, maxChagrgeCooldown);
    }

    IEnumerator Attack(Player player)
    {
          anim.SetTrigger("EnemyAttack");
          anim.SetFloat("LastHorizontal", lastDirection.x);
          anim.SetFloat("LastVertical", lastDirection.y);
          player.TakeDamage(1);

          yield return new WaitForSeconds(3);
          isAttacking = false;
        pleaseStop = false;
    }

    IEnumerator StartCharge()
    {
        pleaseStop = true;

        yield return new WaitForSeconds(1.5f);

        pleaseStop = false;

        chargeStartPosition = transform.position;
        chargeTarget = player.transform.position;
        isCharging = true;

        ScheduleNextCharge();
    }

    public void PlayerAttacking(int amount, Vector2 dir)
    {
        currentHealth -= amount;
        knockBackTarget = dir.normalized * 4 + (Vector2)transform.position;
        knockBack = true;
        if (currentHealth < 1)
        {
            pleaseStop = true;
            anim.SetTrigger("bearDying");
        }
    }

    IEnumerator DestroyBear()
    {
        AudioManager.instance.musicSource.Stop();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("WinScreen");
        AudioManager.instance.PlayMusic("Win");
    }
}