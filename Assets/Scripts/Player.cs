using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] AudioSource ouch;
    [SerializeField] AudioSource dash;
    [SerializeField] AudioSource attack;

    public static Player Instance;
    public Animator anim;
    FreezingSlider slider;

    public float deathDelay = 1f;
    public int health;
    public int maxHealth = 5;

    public float moveSpeed;
    private Vector3 moveInput;
    Rigidbody2D rb;

    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLenght = .5f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCooldownCounter;

    private bool holdingAxe;

    private Zommby nearbyEnemy;

    public List<Image> heartImages; 
    public Sprite fullHeart;  
    public Sprite emptyHeart;     

    void Start()
    {
        slider = GameObject.FindFirstObjectByType<FreezingSlider>();
        health = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        activeMoveSpeed = moveSpeed;

        if (Instance == null)
            Instance = this;

        UpdateHearts();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb.velocity = moveInput * activeMoveSpeed;

        anim.SetFloat("Horizontal", moveInput.x);
        anim.SetFloat("Vertical", moveInput.y);
        anim.SetFloat("Speed", moveInput.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCooldownCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLenght;
                dash.Play();
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;

            if (dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCooldownCounter = dashCooldown;
            }
        }

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && nearbyEnemy != null)
        {
            Attack();
        }

        if (Input.GetMouseButton(1))
        {
            Inventory.instance.Use();
        }
    }

    public void EqipAxe(bool b)
    {
        holdingAxe = b;
        anim.SetBool("HasAxe", b);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemy = other.GetComponentInParent<Zommby>();
        }
        if (other.GetComponent<IPickupable>() != null)
        {
            other.GetComponent<IPickupable>().PickUp();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (nearbyEnemy == other.GetComponentInParent<Zommby>())
            {
                nearbyEnemy = null;
            }
        }
    }

    public void TakeDamage(int amount)
    {
        ouch.Play();
        health -= amount;
        if (health < 1)
        {
            anim.SetTrigger("IsDead");
            Invoke("LoseScreen", deathDelay);
        }

        UpdateHearts();
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        slider.Eating();
        UpdateHearts(); 
    }

    private void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < health)
            {
                heartImages[i].sprite = fullHeart;
            }
            else
            {
                heartImages[i].sprite = emptyHeart;
            }
        }
    }

    private void LoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }

    public void Attack()
    {
        anim.SetTrigger("PlayerAttack");
        attack.Play();

        if (nearbyEnemy != null)
        {
            Vector2 direction = nearbyEnemy.transform.position - transform.position;
            nearbyEnemy.PlayerAttacking(1, direction);
        }
    }
}
