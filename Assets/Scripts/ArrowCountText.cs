using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro namespace'ini ekliyoruz

public class ArrowCountText : MonoBehaviour
{
    public TextMeshProUGUI arrowCountText; // TextMeshPro referansý
    public PlayerController playerController; // MovementController referansý

    void Start()
    {
        // Eðer arrowCountText atanmadýysa, bu objeyi bulup ata
        if (arrowCountText == null)
        {
            arrowCountText = GetComponent<TextMeshProUGUI>();
        }

        // Eðer movementController atanmadýysa, bu objeyi bulup ata
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }

    void Update()
    {
        // Her frame'de ok sayýsýný güncelle
        UpdateArrowCountText();
    }

    void UpdateArrowCountText()
    {
        // Arrow sayýsýný metne dönüþtür ve ekrana yaz
        arrowCountText.text = "Ok Sayýsý: " + playerController.arrowCount.ToString();
    }
}