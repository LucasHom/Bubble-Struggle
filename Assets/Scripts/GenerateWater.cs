using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    [SerializeField] GameObject waterProjectilePrefab; 
    [SerializeField] Transform shootPoint;
    [SerializeField] Rigidbody2D rbPlayer;
    [SerializeField] float shootForce = 10f;
    [SerializeField] float horizontalForcePadding = 0.3f;
    [SerializeField] float remainingWater = 50f;

    [SerializeField] private float delayTime = 0.1f;  
    private bool canShoot = true; 

    void Update()
    {
        if (remainingWater <= 0)
        {
            canShoot = false;
        }
        //Temporary refill
        if (Input.GetKeyDown(KeyCode.R))
        {
            remainingWater += 5;
            if (remainingWater > 50)
            {
                remainingWater = 50;
            }
            canShoot = true;
        }
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && canShoot)
        {
            StartCoroutine(ShootDelay());
        }
    }

    IEnumerator ShootDelay()
    {
        canShoot = false;  

        yield return new WaitForSeconds(delayTime);

        canShoot = true;

        ShootWater();

        
    }

    void ShootWater()
    {
        GameObject waterProjectile = Instantiate(waterProjectilePrefab, shootPoint.position + new Vector3(0f, 1f, 0f), Quaternion.identity);

        Rigidbody2D rb2d = waterProjectile.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.AddForce(new Vector2(rbPlayer.velocity.x * horizontalForcePadding, shootForce), ForceMode2D.Impulse);
        }
        remainingWater -= 1;
    }
}
