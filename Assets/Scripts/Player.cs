using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D bc2d;
    [SerializeField] private ParticleSystem turnDust;

    [SerializeField] public bool playerHealthy = true;
    [SerializeField] public bool playerIsFrozen = false;
    [SerializeField] public bool isReloading = false;
    [SerializeField] private float invincibilityDuration;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float flashInterval = 0.2f;
    [SerializeField] private float freezeTime = 1.5f;

    //private Color originalColor;

    //Puddles
    [SerializeField] public float puddleBuffer = 2f;
    private float appliedPuddleBuffer = 0f;

    //Update based on real layer
    private int playerLayer = 6;
    private int ballLayer = 9;
    private int ballGuardLayer = 11;

    //Movement
    [SerializeField] public float playerSpeed = 6f;
    [SerializeField] public float accelerationRate = 8f;
    private float currentHorizontalInput;
    private float horizontalMaxInput = 1f;
    private float movement = 0f;
    [SerializeField] public bool isFacingRight = true;

    //Shooting
    //[SerializeField] private float upZoneAngleStart = -60f;
    //[SerializeField] private float upZoneAngleEnd = 60f;

    void Start()
    {
        //originalColor = spriteRenderer.color;
    }

    public void applyPuddleBuffer(bool isSlowed)
    {
        if (isSlowed)
        {
            appliedPuddleBuffer = puddleBuffer;
        }
        else
        {
            appliedPuddleBuffer = 0f;
        }
    }

    //private void toggleTransparency()
    //{
    //    if (spriteRenderer.color.a == 1f)
    //    {
    //        Color halfTransparentColor = originalColor;
    //        halfTransparentColor.a = 0.1f; 
    //        spriteRenderer.color = halfTransparentColor; 
    //    }
    //    else
    //    {

    //        Color fullOpaqueColor = originalColor; 
    //        fullOpaqueColor.a = 1f; 
    //        spriteRenderer.color = fullOpaqueColor;
    //    }
    //}

    public void getPlayerMovement()
    {
        if (!playerHealthy)
        {
            spriteRenderer.color = Color.gray;
            movement = 0f;
            currentHorizontalInput = 0f;

            if (!isInvincible)
            {
                StartCoroutine(InvincibilityPeriod());
                StartCoroutine(FlashDuringInvincibility());
            }
            
        }
        else if (playerHealthy && !isReloading && !playerIsFrozen)
        {
            spriteRenderer.color = new Color(255f, 255f, 255f);

            if (Input.GetKey(KeyCode.A))
            {
                currentHorizontalInput = Mathf.MoveTowards(currentHorizontalInput, -horizontalMaxInput, accelerationRate * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                currentHorizontalInput = Mathf.MoveTowards(currentHorizontalInput, horizontalMaxInput, accelerationRate * Time.deltaTime);
            }
            else
            {
                currentHorizontalInput = Mathf.MoveTowards(currentHorizontalInput, 0, accelerationRate * Time.deltaTime);
            }
            movement = currentHorizontalInput * (playerSpeed - appliedPuddleBuffer);

        }
        else
        {
            //means player is healthy and player is reloading
            spriteRenderer.color = new Color(255f, 255f, 255f);
            movement = 0f;
            currentHorizontalInput = 0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Gather input
        if (movement > 0 && !isFacingRight)
        {
            flipPlayer();
        }
        else if (movement < 0 && isFacingRight)
        {
            flipPlayer();
        }
        getPlayerMovement();
    }

    private void FixedUpdate()
    {
        //Use gathered input to make actual movements
        //Vector2 could also be Vector2.right * movement
        //rb2d.MovePosition(rb2d.position + new Vector2 (movement * Time.fixedDeltaTime, 0f));
        //Debug.Log(currentHorizontalInput);
        rb2d.velocity = new Vector2(movement, 0f);
    }

    public void startFreezePlayerCoroutine()
    {
        StartCoroutine(freezePlayer());
    }
    public IEnumerator freezePlayer()
    {
        if (playerIsFrozen) yield break;

        //Stop movement and shooting
        playerIsFrozen = true;
        invincibilityDuration = freezeTime + 1.2f;
        playerHealthy = false;
        //Debug.Log("waiting");
        //Debug.Log(freezeTime);
        yield return new WaitForSeconds(freezeTime);
        //Debug.Log("done waiting");
        //Start movement and shooting
        playerHealthy = true;
        playerIsFrozen = false;

    }
    private IEnumerator FlashDuringInvincibility()
    {
        float elapsedTime = 0f;

        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            //toggleTransparency();
            yield return new WaitForSeconds(flashInterval);   
            elapsedTime += flashInterval;
        }

        // Ensure player is visible at the end
        spriteRenderer.enabled = true;
    }

    private IEnumerator InvincibilityPeriod()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, ballLayer, true);
        Physics2D.IgnoreLayerCollision(playerLayer, ballGuardLayer, true);
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        Physics2D.IgnoreLayerCollision(playerLayer, ballLayer, false);
        Physics2D.IgnoreLayerCollision(playerLayer, ballGuardLayer, false);
        isInvincible = false;
    }

    private void flipPlayer()
    {
        createTurnDust();
        isFacingRight = !isFacingRight;
        //Uncomment if I want to actually flip the player, unfortunately also flips reloadCanvas
        //Vector3 scale = transform.localScale;
        //scale.x *= -1;
        //transform.localScale = scale;
    }

    private void createTurnDust()
    {
        turnDust.Play();
    }


    //public Vector2 GetShootDirection(Vector3 mousePosition)
    //{
    //    Vector2 direction = mousePosition - transform.position;
    //    float angle = Vector2.SignedAngle(Vector2.up, direction);
    //    Debug.Log(angle);

    //    if (angle >= upZoneAngleStart && angle <= upZoneAngleEnd)
    //    {
    //        return Vector2.up; // Shoot up
    //    }
    //    else
    //    {
    //        if (direction.x > 0)
    //        {
    //            return new Vector2(1, 0.25f); 
    //        }
    //        else
    //        {
    //            return new Vector2(-1, 0.25f); 
    //        } 
    //    }
    //}


    ////Draw Shoot Boundries
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.white;
    //    Gizmos.DrawWireSphere(transform.position, 0.2f); 

    //    Gizmos.color = Color.yellow;
    //    Vector3 rightBoundary = Quaternion.Euler(0, 0, upZoneAngleStart) * Vector3.up;
    //    Vector3 leftBoundary = Quaternion.Euler(0, 0, upZoneAngleEnd) * Vector3.up;

    //    Gizmos.DrawLine(transform.position, transform.position + rightBoundary * 2);
    //    Gizmos.DrawLine(transform.position, transform.position + leftBoundary * 2);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.right * 2);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawLine(transform.position, transform.position + Vector3.left * 2);
    //}
}
