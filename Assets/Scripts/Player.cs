using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public Animator anim;

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

    void Start()
    {
        health = maxHealth;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        activeMoveSpeed = moveSpeed;

        if(Instance == null)
            Instance = this;
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
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health < 1)
        {
            anim.SetTrigger("IsDead");

            Invoke("LoseScreen", deathDelay);
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }
    private void LoseScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }

}