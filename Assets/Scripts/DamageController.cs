using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageController : MonoBehaviour
{
    [SerializeField] public HealthBar healthBar; 
    public int lives = 5; 

    public void TakeDamage(int damage)
    {
        lives -= damage;
        lives = Mathf.Clamp(lives, 0, 5); 

        if (healthBar != null)
        {
            healthBar.UpdateHealth(damage);
        }

        if (lives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadSceneAsync(15);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyeasy"))
        {
            TakeDamage(1); 
        }
        else if (collision.CompareTag("enemyhard"))
        {
            TakeDamage(2); 
        }
    }
}