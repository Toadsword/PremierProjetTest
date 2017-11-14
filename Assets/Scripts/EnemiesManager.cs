using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour {

    private int monsterCount = 0;
    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void AddMonster()
    {
        monsterCount++;
    }

    public void RemoveMonster()
    {
        monsterCount--;
        gameManager.CheckWin();
    }

    public int GetMonsterCount()
    {
        return monsterCount;
    }
}
