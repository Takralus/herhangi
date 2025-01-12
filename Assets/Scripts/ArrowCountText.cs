using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro namespace'ini ekliyoruz

public class ArrowCountText : MonoBehaviour
{
    public TextMeshProUGUI arrowCountText; // TextMeshPro referans�
    public PlayerController playerController; // MovementController referans�

    void Start()
    {
        // E�er arrowCountText atanmad�ysa, bu objeyi bulup ata
        if (arrowCountText == null)
        {
            arrowCountText = GetComponent<TextMeshProUGUI>();
        }

        // E�er movementController atanmad�ysa, bu objeyi bulup ata
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }

    void Update()
    {
        // Her frame'de ok say�s�n� g�ncelle
        UpdateArrowCountText();
    }

    void UpdateArrowCountText()
    {
        // Arrow say�s�n� metne d�n��t�r ve ekrana yaz
        arrowCountText.text = "Ok Say�s�: " + playerController.arrowCount.ToString();
    }
}