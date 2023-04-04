using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private bool canAttackPlayer = false;
    public float birdHP;
    public LogicScript logicsript;
    private Rigidbody2D rb;
    private Transform playerPosition;
    public GameObject bullet;
    public Transform fireDirection;
    private float lookAngle;
    private bool canShoot;
    private Transform corePosition;
    private Vector3 direction;
    private Vector3 checkdirection;
    private Vector2 movement;
    [Header("Gun Settings")]
    public float shootDistance = 2;
    public float distance;
    public float checkdistance;
    public float moveSpeed = 3;
    public float birdVision;


    private void Awake()
    {
        logicsript = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        corePosition = GameObject.FindGameObjectWithTag("Core").GetComponent<Transform>();


    }
    private void Update()
    {
       if(canAttackPlayer == false)
        {
            birdAttackCore();
            birdMoveLocation();
        }
        if (canAttackPlayer == true)
        {
            birdAttackPlayer();
            birdMoveLocation();

             if(distance > shootDistance) 
              {
                  birdMoveLocation();
              }
              else 
              {
                  movement = Vector2.zero;
              }
             
        }
        if (birdHP <= 0)
        {
            logicsript.dropElixir(transform.position, transform.rotation);
            Destroy(gameObject);
        }
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
            Debug.Log(birdHP);
        }
    }
    public void birdAttackCore() 
    {
        direction = corePosition.position - transform.position;
        distance = direction.magnitude;
        lookAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = lookAngle;

    }
    public void birdAttackPlayer() 
    {
      direction = playerPosition.position - transform.position;
      distance = direction.magnitude;
        lookAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = lookAngle;
    }
    public void birdMoveLocation() 
    {
        direction.Normalize();
        movement = direction;
        canShoot = false;
    }
    void moveBird(Vector2 direction)
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
   
}
