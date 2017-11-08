using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPlayer : MonoBehaviour {

    private int playerDieCount = 0;

    private static GameObject lastObject = null;

	// Use this for initialization
	void Start () {
        if(lastObject == null)
        {
            DontDestroyOnLoad(gameObject);
            lastObject = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CountPlayerDie()
    {
        playerDieCount++;
    }

    public int GetPlayerDieCount()
    {
        return playerDieCount;
    }
}
