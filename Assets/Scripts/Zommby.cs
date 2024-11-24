using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zommby : MonoBehaviour
{
    public static Zommby instance;

    [SerializeField] private float maxHealth = 3f;

    public GameObject player;
    public Animator anim;
    public float speed;

    private bool pleaseStop;
    private bool isAttacking = false;
    private float distance;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();

        if (instance == null)
            instance = this;
    }

    private void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        if (distance < 22 && pleaseStop == false)
        {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (distance > 22)
        {

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
            player.TakeDamage(1);
            yield return new WaitForSeconds(2); 
        }
    }
}
