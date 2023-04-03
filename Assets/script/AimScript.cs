using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimScript : MonoBehaviour
{
    private Transform aimTransform;
    public SpriteRenderer player;
    // Start is called before the first frame update
    void Start()
    {
        aimTransform = transform.Find("aim");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Logic.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        Vector3 localScale = Vector3.one;
        if (angle > 90f || angle < -90f) 
        {
            localScale.y = -1f;
            player.flipX = false;
        }
        else 
        {
            localScale.y = +1f;
            player.flipX = true;
        }
        aimTransform.localScale = localScale;
    }
}
