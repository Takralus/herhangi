using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Baslangic : MonoBehaviour
{
    public void Basla()
    {
        SceneManager.LoadSceneAsync(4);
    }
    public void Ayarlar()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void Credits()
    {
        SceneManager.LoadSceneAsync(3);
    }
    public void Geri()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
