using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnemyHard : MonoBehaviour
{
    public Transform player;
    float speed = 3f;
    bool isChasing = false;
    Animator anim;
    HealthBar playerHealthBar;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerHealthBar = player.GetComponent<HealthBar>();
    }

    void Update()
    {
        if (isChasing && player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            if (player.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (player.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.transform;
            isChasing = true;
            anim.SetBool("isMoving", true);

            DealDamage(2); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    void DealDamage(int amount)
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.UpdateHealth(-amount);
        }
    }

    public void Defeat()
    {
        GameManager.Instance.DefeatEnemy("enemyhard");
        Destroy(gameObject); 
    }
}