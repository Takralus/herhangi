using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    float horizontalInput;
    float moveSpeed = 10f;
    bool isFacingRight = false;
    float jumpPower = 10f;
    bool isGrounded = false;
    public Transform playerattackrange;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (playerattackrange == null)
        {
            playerattackrange = transform.Find("playerattackrange");
        }
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        FlipSprite();

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            anim.SetBool("isJumping", !isGrounded);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        { 
            Attack();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        anim.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    void FlipSprite()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 Is = transform.localScale;
            Is.x *= -1f;
            transform.localScale = Is;
        }
    }

    void Attack()
    {
        anim.SetBool("isAttacking", true);
        StartCoroutine(StopAttackAnimation());

        float attackRange = 2f;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(playerattackrange.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("enemyeasy")) 
            {
                Destroy(enemy.gameObject);
                GameManager.Instance.DefeatEnemy("easy");
            }

            if (enemy.CompareTag("enemyhard"))
            {
                Destroy(enemy.gameObject);
                GameManager.Instance.DefeatEnemy("hard");
            }
        }

        IEnumerator StopAttackAnimation()
        {
            yield return new WaitForSeconds(0.2f);
            anim.SetBool("isAttacking", false);
        }
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        isGrounded = true;
        anim.SetBool("isJumping", !isGrounded);
    }
}