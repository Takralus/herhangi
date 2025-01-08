using UnityEditor.Build.Content;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instance.defeatedEnemies >= GameManager.Instance.totalEnemies)
            {
                SceneController.Instance.NextLevel();
            }
        }
    }
}
