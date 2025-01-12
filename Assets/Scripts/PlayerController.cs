using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    float horizontalInput;
    float moveSpeed = 10f;
    bool isFacingRight = false;
    float jumpPower = 10f;
    bool isGrounded = false;
    public Transform playerattackrange;

    public GameObject arrowPrefab;
    public Transform bowTransform;
    public float arrowSpeed;

    public int arrowCount = 12; 
    public int maxArrowCount = 12;

    public HealthBar healthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (playerattackrange == null)
        {
            playerattackrange = transform.Find("playerattackrange");
        }

        if (PlayerPrefs.HasKey("ArrowCount"))
        {
            arrowCount = PlayerPrefs.GetInt("ArrowCount");
        }
        else
        {
            arrowCount = maxArrowCount;
        }

        if (healthBar == null)
        {
            healthBar = FindObjectOfType<HealthBar>();
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

        if (Input.GetMouseButtonDown(0) && arrowCount > 0)
        {
            anim.SetBool("isShooting", true);
        }

        if (Input.GetMouseButtonUp(0) && arrowCount > 0)
        {
            ShootArrow();
            arrowCount--;
            PlayerPrefs.SetInt("ArrowCount", arrowCount);
            PlayerPrefs.Save();
            anim.SetBool("isShooting", false);
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

    void ShootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, bowTransform.position, Quaternion.identity);
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0; 

        Vector2 direction = (mouseWorldPosition - bowTransform.position).normalized;

        arrowRb.velocity = direction * arrowSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(arrow, 5f);
    }

    private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.CompareTag("quiver"))
        {
            arrowCount = Mathf.Min(arrowCount + 5, maxArrowCount);
            PlayerPrefs.SetInt("ArrowCount", arrowCount);
            PlayerPrefs.Save();
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("ecemkalp"))
        {
            healthBar.UpdateHealth(-1);

            Destroy(collision.gameObject);
        }

        isGrounded = true;
        anim.SetBool("isJumping", !isGrounded);
    }
}