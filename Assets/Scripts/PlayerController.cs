using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] // Force la présence d'un rigid body sur le player et empêche sa suppression
public class PlayerController : MonoBehaviour
{
    [Header("Jumps")]
    [Range(0.0f, 10.0f)]
    [SerializeField] private float maxJumpForce = 7;
    [Range(0.0f, 10.0f)]
    [SerializeField] private float minJumpVelocity = 4;
    [SerializeField] private Transform positionRaycastJump; // Position du détecteur de collision
    [SerializeField] private float radiusRaycastJump; // Rayon de la détection de collision
    [SerializeField] private LayerMask layerMaskJump; // Layout physique de l'objet

    [Header("Fire gun")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform GunTransformLeft; // Pour la sortie du tire
    [SerializeField] private Transform GunTransformRight; // Pour la sortie du tire
    [SerializeField] private float bulletVelocity = 2.0f; // Vitesse de la balle 
    [SerializeField] private float time2Fire = 2.0f; // Cooldown de tir
    private float lastTimeFire = 0; // Dernier moment ou on a tiré pour la derniere fois 

    private const float vitMax = 5.0f;
    private float distToGround;
    private bool isFacingLeft = false;

    //[TextArea] [SerializeField] private string unMessage;
    //[SerializeField] private Color color;

    private GameManager gameManager;
    private Transform spawnTransform;

    private Animator playerAnimationController;
    private Collider2D collider;
    private SpriteRenderer render;
    private Rigidbody2D rigid;

    // Use this for initialization
    void Start ()
    {
        playerAnimationController = GetComponent<Animator>();
        collider = GetComponent<Collider2D>();
        render = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        gameManager = FindObjectOfType<GameManager>();
        spawnTransform = GameObject.Find("Spawn").transform;

        distToGround = GetComponent<Collider2D>().bounds.extents.y;

        FindObjectOfType<PauseScript>().OnPauseEvent += OnPauseGame;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if(horizontalInput != 0)
        {
            isFacingLeft = horizontalInput < 0;
            render.flipX = isFacingLeft;
        }
        playerAnimationController.SetFloat("SpeedX", Mathf.Abs(horizontalInput));
        playerAnimationController.SetFloat("SpeedY", rigid.velocity.y);

        bool touchFloor = collider.IsTouchingLayers(LayerMask.GetMask("Plateform"));

        Vector2 forceDirection = new Vector2(horizontalInput, 0);
        ManageSpeed(forceDirection, touchFloor);

        if (Input.GetKeyDown(KeyCode.Space) && touchFloor && rigid.velocity.y == 0)
        {
            playerAnimationController.SetTrigger("Jumped");
            rigid.AddForce(Vector2.up * maxJumpForce, ForceMode2D.Impulse);
        }
        else
        {
            playerAnimationController.SetBool("IsGrounded", touchFloor);
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            if(rigid.velocity.y > minJumpVelocity)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, minJumpVelocity);
            }
        }

        if(Input.GetAxis("Fire1") > 0)
        {
            Fire();
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Limit" || collision.tag == "EnemyBullet")
        {
            gameManager.PlayerDie();
            transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            transform.position = spawnTransform.position;
        }

        if(collision.tag == "Heart")
        {
            gameManager.PlayerGetLife();
            Destroy(collision.gameObject);
        }
    }

    private void Fire()
    {
        if(Time.realtimeSinceStartup - lastTimeFire > time2Fire)
        {
            Transform Gun = GunTransformRight;
            if(isFacingLeft)
                Gun = GunTransformLeft;

            GameObject bullet = Instantiate(bulletPrefab, Gun.position, Gun.rotation);


            playerAnimationController.SetTrigger("Fire");
            bullet.GetComponent<Rigidbody2D>().velocity = Gun.right * bulletVelocity;
            bullet.tag = "AllyBullet";
            Destroy(bullet, 5);
            lastTimeFire = Time.realtimeSinceStartup;
        }
    }

    private void ManageSpeed(Vector2 forceDirection, bool touchFloor)
    {
        /*  
         *  Si on accélère
         *      Si on est en l'air
         *          On réduit l'accélération
         *          
         *      Applique accélération
         *  Sinon // On bouge pas
         *      Si on est au sol
         *          On décélère genitiment
         *      Sinon
         *          On fait rien
         *      fin si
         *  fin si
         *  
         *  
         *  Si on bouge
         *      Si on est au sol
         *          Accélération normal
         *      Si on est en l'air
         *          Accélération réduite
         *  Si on bouge pas
         *      Si on est en l'air
         *          On fait rien
         *      Si on est au sol
         *          On décélère gentiment
         *  Fin si
         *  
         *  Si on est trop rapide
         *      On mets une vitesse fixe
         */
        if (forceDirection.x != 0)
        {
            forceDirection *= 10;
            if (!touchFloor)
            {
                forceDirection /= 1.5f;
            }
            rigid.AddForce(forceDirection);
        }
        else
        {
            if (touchFloor)
            {
                rigid.AddForce(new Vector2(-rigid.velocity.x * 3.0f, 0));
            }
        }

        if (Mathf.Abs(rigid.velocity.x) > vitMax)
        {
            rigid.velocity = new Vector2(5.0f * Mathf.Sign(rigid.velocity.x), rigid.velocity.y);
        }
    }

    //Super pratique pour l'éditeur, ce code s'utilise quand on sélectionne le player
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(positionRaycastJump.position, radiusRaycastJump);
    }

    private void OnPauseGame()
    {

    }
}
