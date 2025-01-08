using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanger : MonoBehaviour
{
    public GameObject arrowPrefab; // Fýrlatýlacak ok
    public Transform bowPosition; // Okun fýrlatýlacaðý yer
    public float shootInterval = 3.5f; // Atýþlar arasýndaki süre
    public float arrowSpeed = 20f; // Okun hýzý

    private Transform player; // Oyuncunun pozisyonu
    private bool canShoot = true; // Atýþ yapma kontrolü

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (bowPosition == null)
        {
            bowPosition = transform.Find("bow");
        }
        StartCoroutine(ShootArrow());
    }

    IEnumerator ShootArrow()
    {
        while (true)
        {
            if (canShoot && player != null)
            {
                canShoot = false; // Atýþ yapýlýrken yeni atýþ yapýlmasýný engelle

                // Yönü hesapla
                Vector3 direction = (player.position - bowPosition.position).normalized;

                // Ok oluþtur
                GameObject arrow = Instantiate(arrowPrefab, bowPosition.position, Quaternion.identity);

                // Okun yönünü hedefe döndür
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle); // Oku döndür

                // Rigidbody2D ile harekete geçir
                Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * arrowSpeed; // Oku fýrlat
                }
                else
                {
                    Debug.LogError("Arrow prefabýnda Rigidbody2D bulunamadý!");
                }

                Destroy(arrow, 2.5f); // Oku 7 saniye sonra yok et

                yield return new WaitForSeconds(shootInterval); // Atýþlar arasýndaki süre
                canShoot = true; // Yeni atýþa izin ver
            }

            yield return null; // Her frame kontrol et
        }
    }
}