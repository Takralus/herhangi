using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    public int damage = 1; // Verilen hasar miktar�

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

        // Di�er objelere �arpt���nda oku yok et
        if (collision.CompareTag("ground"))
        {
            Destroy(gameObject);
        }
    }
}