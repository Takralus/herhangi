using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanger : MonoBehaviour
{
    public GameObject arrowPrefab; // F�rlat�lacak ok
    public Transform bowPosition; // Okun f�rlat�laca�� yer
    public float shootInterval = 3.5f; // At��lar aras�ndaki s�re
    public float arrowSpeed = 20f; // Okun h�z�

    private Transform player; // Oyuncunun pozisyonu
    private bool canShoot = true; // At�� yapma kontrol�

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
                canShoot = false; // At�� yap�l�rken yeni at�� yap�lmas�n� engelle

                // Y�n� hesapla
                Vector3 direction = (player.position - bowPosition.position).normalized;

                // Ok olu�tur
                GameObject arrow = Instantiate(arrowPrefab, bowPosition.position, Quaternion.identity);

                // Okun y�n�n� hedefe d�nd�r
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle); // Oku d�nd�r

                // Rigidbody2D ile harekete ge�ir
                Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = direction * arrowSpeed; // Oku f�rlat
                }
                else
                {
                    Debug.LogError("Arrow prefab�nda Rigidbody2D bulunamad�!");
                }

                Destroy(arrow, 2.5f); // Oku 7 saniye sonra yok et

                yield return new WaitForSeconds(shootInterval); // At��lar aras�ndaki s�re
                canShoot = true; // Yeni at��a izin ver
            }

            yield return null; // Her frame kontrol et
        }
    }
}