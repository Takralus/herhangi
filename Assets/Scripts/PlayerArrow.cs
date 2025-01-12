using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    public int damage = 1; 
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateRotationBasedOnVelocity();
    }

    private void UpdateRotationBasedOnVelocity()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ground") ||
            collision.CompareTag("movingground") ||
            collision.CompareTag("enemyeasy") ||
            collision.CompareTag("enemyhard") ||
            collision.CompareTag("enemyranger"))
        {
            HandleCollision(collision);
        }
    }

    private void HandleCollision(Collider2D collision)
    {
        
        if (collision.CompareTag("enemyeasy"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.DefeatEnemy("easy");
        }

        
        if (collision.CompareTag("enemyhard"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.DefeatEnemy("hard");
        }

        
        if (collision.CompareTag("enemyranger"))
        {
            Destroy(collision.gameObject);
        }

        
        Destroy(gameObject);
    }
}