using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player" && gameObject.tag == "EnemyBullet") || 
            (collision.tag == "Enemy" && gameObject.tag == "AllyBullet") ||
            collision.tag == "Limit")
        {
            Destroy(gameObject);
        }

        if((collision.tag == "AllyBullet" && gameObject.tag == "EnemyBullet") || 
            (collision.tag == "EnemyBullet" && gameObject.tag == "AllyBullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
    
}
