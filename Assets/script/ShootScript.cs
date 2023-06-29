using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GlobalVariables variables;
    public GameObject bullet;
    public int gunState = 1;
    public PlayerMovement playerMovement;
    public float timer = 0;
    private bool canShoot = true;

    [Header("Gun Settings")]
    public SpriteRenderer weapon;
   
    public Sprite pistol;
    public Sprite rifle;
    public float pistolFireRate;
    public float rifleFireRate;
    private bool isShooting;
    // 1 = pistol, 2 = shotgun, 3 = rifle

    void Start()
    {
        
  
    }

    
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") < 0f && variables.canUseRifle == true) 
        {
            if(gunState == 2) 
            {
                gunState = 1;
            }
            else 
            {
                gunState = 2;
            }
            
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && variables.canUseRifle == true) 
        {
            if (gunState == 1)
            {
                gunState = 2;
            }
            else 
            {
                gunState = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            gunState = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && variables.canUseRifle == true)
        {
            gunState = 2;
        }

        if (gunState == 1) 
        {
            weapon.sprite = pistol;
            PistolShoot();
        }
        if (gunState == 2) 
        {
            weapon.sprite = rifle;
            RifleShoot();
        }
        if (!Input.GetMouseButton(0)) 
        {
            isShooting = false;
        }
        if (isShooting == false)
        {
            playerMovement.moveSpeed = 4;
        }

    }
    public void PistolShoot()
    {
        if (canShoot && Input.GetMouseButton(0))
        {
            Instantiate(bullet, transform.position, transform.rotation);
           
            Debug.Log(isShooting);
            canShoot = false;
            timer = 0;
        }
        if (Input.GetMouseButton(0))
        {
            isShooting = true;
            playerMovement.moveSpeed = 2;
        }
        if (!Input.GetMouseButton(0))
        {
            playerMovement.moveSpeed = 4;
        }
        {
            timer += Time.deltaTime;
            if (timer >= pistolFireRate)
            {
                canShoot = true;
                timer = 0; 
            }
        }
    }
    public void RifleShoot() 
    {
        if (canShoot && Input.GetMouseButton(0) && variables.ammo > 0 )
        {
            Instantiate(bullet, transform.position, transform.rotation);
            isShooting = true;
            playerMovement.moveSpeed = 1;
            canShoot = false;
            timer = 0;
            variables.ammo--;
            Debug.Log(variables.ammo);
        }
      

        if (!canShoot)
        {
            timer += Time.deltaTime;
            if (timer >= rifleFireRate)
            {
                canShoot = true;
                timer = 0;
            }
        }
    }
}
