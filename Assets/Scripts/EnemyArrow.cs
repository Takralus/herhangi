using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public int damage = 1; // Verilen hasar miktarý

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DamageController playerDamageController = collision.GetComponent<DamageController>();
            if (playerDamageController != null)
            {
                playerDamageController.TakeDamage(damage); // Oyuncuya hasar ver
            }

            Destroy(gameObject); // Oku yok et
        }

        // Diðer objelere çarptýðýnda oku yok et
        if (collision.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }
}