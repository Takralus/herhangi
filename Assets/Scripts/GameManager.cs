using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int totalEnemies;
    public int defeatedEnemies = 0;

    public FinishPoint finishPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        totalEnemies = GameObject.FindGameObjectsWithTag("enemyeasy").Length + GameObject.FindGameObjectsWithTag("enemyhard").Length;

        finishPoint = FindObjectOfType<FinishPoint>();
    }

    public void DefeatEnemy(string enemyType)
    {
        defeatedEnemies++;
    }
}