using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWater : MonoBehaviour
{
    [SerializeField] GameObject waterProjectilePrefab; 
    [SerializeField] Transform shootPoint;
    [SerializeField] Rigidbody2D rbPlayer;
    [SerializeField] float shootForce = 10f;
    [SerializeField] float horizontalForcePadding = 0.3f;
    [SerializeField] float remainingWater;
    [SerializeField] float maxWater = 10f;

    [SerializeField] private float shootDelay = 0.1f;  
    private bool canShoot = true; 
    [SerializeField] private GameObject player;

    [SerializeField] float reloadDelay = 0.06f;
    [SerializeField] float reloadAmount = 1f;
    private bool isReloading = false;

    private Player playerScript;

    void Start()
    {
        remainingWater = maxWater;
        playerScript = player.GetComponent<Player>();  
    }

    void Update()
    {
        if (remainingWater <= 0)
        {
            canShoot = false;
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Mouse1)))
        {
            //if (!isReloading)
            if (!isReloading && !playerScript.playerIsFrozen)
            {
                playerScript.isReloading = true;
                StartCoroutine(ReloadWater());
            }
        }
        else
        {
            playerScript.isReloading = false;
            isReloading = false; 
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && canShoot && playerScript.playerHealthy && !playerScript.isReloading && !playerScript.playerIsFrozen)
        {
            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator ReloadWater()
    {
        isReloading = true;

        while ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Mouse1)) && remainingWater < maxWater)
        {
            remainingWater += reloadAmount;

            if (remainingWater > maxWater)
            {
                remainingWater = maxWater;
            }

            canShoot = true;
            Debug.Log(remainingWater);

            yield return new WaitForSeconds(reloadDelay);
        }
        isReloading = false;
        playerScript.isReloading = false;
    }

    IEnumerator ShootDelay()
    {
        canShoot = false;  

        yield return new WaitForSeconds(shootDelay);

        canShoot = true;

        ShootWater();
 
        
    }

    void ShootWater()
    {
        GameObject waterProjectile = Instantiate(waterProjectilePrefab, shootPoint.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        //GameObject waterProjectile = Instantiate(waterProjectilePrefab, shootPoint.position, Quaternion.identity);

        Rigidbody2D rb2d = waterProjectile.GetComponent<Rigidbody2D>();
        if (rb2d != null)
        {
            rb2d.AddForce(new Vector2(rbPlayer.velocity.x * horizontalForcePadding, shootForce), ForceMode2D.Impulse);
        }
        remainingWater -= 1;
    }
}
