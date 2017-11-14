using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int lifes = 3;

    [SerializeField] private Text textLifes;
    [SerializeField] private Transform[] heartSpawnsList;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private float heartSpawnTime = 20.0f;
    private bool heartSpawned = false;
    private InfoPlayer infoPlayer;

    private const string TEXT_LIFES = "Lifes : ";

    // Use this for initialization
    void Start ()
    {
        textLifes.text = TEXT_LIFES + lifes;
        StartCoroutine(HeartSpawning());
        infoPlayer = FindObjectOfType<InfoPlayer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void PlayerDie()
    {
        lifes--;
        infoPlayer.CountPlayerDie();
        if (lifes > 0)
        {
            textLifes.text = TEXT_LIFES + lifes;
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void PlayerGetLife()
    {
        lifes++;
        textLifes.text = TEXT_LIFES + lifes;
        heartSpawned = false;
    }

    private IEnumerator HeartSpawning()
    {
        while(true)
        {
            yield return new WaitForSeconds(heartSpawnTime);
            if(!heartSpawned && heartSpawnsList.Length > 0)
            {
                int spawnNumber = (int) Random.Range(0.0f, heartSpawnsList.GetLength(0));
                Transform spawner = heartSpawnsList[spawnNumber];
                GameObject Heart = Instantiate(heartPrefab, spawner.position, spawner.rotation);
                heartSpawned = true;
            }
        }
    }

    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void CheckWin()
    {

    }
}
