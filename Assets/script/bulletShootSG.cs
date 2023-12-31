using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletShootSG : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDestroy;
    private Transform bulletTransform;
    public Rigidbody2D rb;
    private void Awake()
    {
        bulletTransform = gameObject.GetComponent<Transform>();
        Vector3 mousePosition = Logic.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        bulletTransform.eulerAngles = new Vector3(0, 0, angle);
        Vector3 localScale = Vector3.one;
        bulletTransform.localScale = localScale;
    }
    private void Update()
    {
        if (gameObject != null)
        {
            Destroy(gameObject, bulletDestroy);
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
    }
}
