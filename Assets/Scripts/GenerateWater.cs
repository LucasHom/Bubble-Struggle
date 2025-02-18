using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateWater : MonoBehaviour
{
    [SerializeField] GameObject waterProjectilePrefab; 
    [SerializeField] Transform shootPoint;
    [SerializeField] Rigidbody2D rbPlayer;
    [SerializeField] float shootForce = 10f;
    [SerializeField] float horizontalForcePadding = 0.3f;

    [SerializeField] public float remainingWater;
    [SerializeField] public float maxWater;

    [SerializeField] private float shootDelay = 0.1f;  
    private bool canShoot = true;

    [SerializeField] private Image ReloadWaterIcon;
    [SerializeField] private GameObject player;

    [SerializeField] public float reloadDelay = 0.06f;
    [SerializeField] float reloadAmount = 1f;
    private bool isReloading = false;
    private bool canShowReloadIcon = false;
    [SerializeField] private bool isReloadIconFlashing = false;


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
            canShowReloadIcon = true;
        }

        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Mouse1)))
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

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && canShowReloadIcon)
        {
            if (!isReloadIconFlashing)
            {
                StartCoroutine(FlashReloadIcon());
            }
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Mouse0)) && canShoot && playerScript.playerHealthy && !playerScript.isReloading && !playerScript.playerIsFrozen)
        {
            StartCoroutine(ShootDelay());
        }
    }

    private IEnumerator FlashReloadIcon()
    {
        if (isReloadIconFlashing)
        {
            yield break;
        }
        isReloadIconFlashing = true;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            if (isReloading)
            {
                ReloadWaterIcon.enabled = false;
                isReloadIconFlashing = false;
                yield break;
            }
            ReloadWaterIcon.enabled = !ReloadWaterIcon.enabled;
            //toggleTransparency();
            yield return new WaitForSeconds(0.15f);
            elapsedTime += 0.2f;
        }
        ReloadWaterIcon.enabled = false;
        isReloadIconFlashing = false;
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
            canShowReloadIcon = false;
            //Debug.Log(remainingWater);

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
