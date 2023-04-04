using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 10f);
    }
    private void FixedUpdate()
    {
        rb.AddForce(transform.right * bulletSpeed *Time.deltaTime, ForceMode2D.Impulse);
    }
}
