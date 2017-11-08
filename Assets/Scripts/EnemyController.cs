using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    [SerializeField] private Transform[] gunsTransformList;
    [SerializeField] private float time2Fire = 2.0f;
    [SerializeField] private float bulletVelocity = 8.0f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeInvulnerability = 2.0f; // Temps d'invulnérabilité
    private float lastTimeTouched = -5; // Dernier moment ou il s'est fait touché 

    private float life = 5;
    private bool isTransparent = false;

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(Fire());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(isInvulnerable(true))
        {
            animateInvulnerability();
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 0, 255, 1);
        }
    }

    private IEnumerator Fire()
    {
        while(true)
        {
            yield return new WaitForSeconds(time2Fire);
            foreach(Transform t in gunsTransformList)
            {
                GameObject bullet = Instantiate(bulletPrefab, t.position, t.rotation);
                bullet.GetComponent<Rigidbody2D>().velocity = t.right * bulletVelocity;
                bullet.tag = "EnemyBullet";
                Destroy(bullet, 5);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AllyBullet")
        {
            if(!isInvulnerable(false))
            {
                life--;
                if (life == 0)
                {
                    Destroy(gameObject);
                    SceneManager.LoadScene("WinScene");
                }
            }
        }
    }

    private bool isInvulnerable(bool isCheck)
    {
        if (Time.realtimeSinceStartup - lastTimeTouched > timeInvulnerability)
        { 
            if(!isCheck)
            {
                lastTimeTouched = Time.realtimeSinceStartup;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    private void animateInvulnerability()
    {
        if(isTransparent)
        {
            GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 0.3f) / life;
            if (GetComponent<SpriteRenderer>().color.a >= 1)
                isTransparent = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 0.3f) / life;
            if (GetComponent<SpriteRenderer>().color.a <= 0)
                isTransparent = true;
        }
    }
}
