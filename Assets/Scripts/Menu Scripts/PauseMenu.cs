using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool gamePaused = false;
    [SerializeField] GameObject panel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == false)
        {
            Time.timeScale = 0;
            gamePaused = true;
            panel.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && gamePaused == true)
        {
            Time.timeScale = 1;
            gamePaused = false;
            panel.SetActive(false);
        }
    }

    public void DevamEt()
    {
        Time.timeScale = 1;
        gamePaused = false;
        panel.SetActive(false);
    }

    public void AnaMenu()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }

    public void Masaustu()
    {
        Application.Quit();
    }
}
