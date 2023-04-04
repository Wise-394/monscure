using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public SpriteRenderer birdSprite;
    private bool canAttackPlayer = false;
    public float birdHP;
    public LogicScript logicsript;
    private Rigidbody2D rb;
    private Transform playerPosition;
    public GameObject bulletPreFab;
    public Transform fireDirection;
    private float lookAngle;
    private Transform corePosition;
    private Vector3 direction;
    private Vector3 checkdirection;
    private Vector2 movement;
    [Header("Gun Settings")]
    public float bulletSPD;
    public float shootDistance = 2;
    public float distance;
    public float checkdistance;
    public float moveSpeed = 3;
    public float attackSPD;
    public float birdVision;
    private Quaternion defaultRotation;
    private bool canShoot;
    private float timer;
    public Transform firePoint;

    private void Awake()
    {
        logicsript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        corePosition = GameObject.FindGameObjectWithTag("Core").GetComponent<Transform>();
        defaultRotation = transform.rotation;

    }
    private void Update()
    {
       

       
        birdMovement();
        Vector3 localScale = Vector3.one;
        if (lookAngle > 90f || lookAngle < -90f)
        {
            localScale.y = +1f;
            birdSprite.flipY = true;
        }
        else
        {
            localScale.y = -1f;
           birdSprite.flipY = false;
        }
        if (birdHP <= 0)
        {
            logicsript.dropElixir(transform.position,defaultRotation);
            Destroy(gameObject);
        }
        if (distance > shootDistance && canAttackPlayer)
        {
            direction.Normalize();
            movement = direction;
            canShoot = false;

        }
        else
        {
            if (canAttackPlayer == true)
            {
                movement = Vector2.zero;
                if (timer < attackSPD)
                {
                    timer += Time.deltaTime;
                }
                else if (timer > attackSPD)
                {
                    GameObject bullet = Instantiate(bulletPreFab, firePoint.position, transform.rotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    rb.AddForce(transform.right * bulletSPD, ForceMode2D.Impulse);
                    timer = 0;
                }

            }
        }
        Debug.Log(distance);
    }
    private void FixedUpdate()
    {      
            moveBird(movement); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            birdHP = logicsript.onHitEnemy(birdHP);         
        }
        if (collision.gameObject.layer == 8)
        {
            canAttackPlayer = true;
        }
    }
    public void birdMovement() 
    {
        if(canAttackPlayer == false) 
        {
            direction = corePosition.position - transform.position;
            distance = direction.magnitude;
            lookAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = lookAngle;
        }
        if(canAttackPlayer == true) 
        {
            direction = playerPosition.position - transform.position;
            distance = direction.magnitude;
            lookAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = lookAngle;
        }
        direction.Normalize();
        movement = direction;
        canShoot = false;
    }
    void moveBird(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            canAttackPlayer = false;
        }

    }*/

}
