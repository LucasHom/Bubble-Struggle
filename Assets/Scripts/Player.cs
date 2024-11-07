using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 6f;

    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D bc2d;

    [SerializeField] public bool playerHealthy = true;
    [SerializeField] public bool playerIsFrozen = false;
    [SerializeField] public bool isReloading = false;
    [SerializeField] public float invincibilityDuration;
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

    //Movement
    private float currentHorizontalInput;
    [SerializeField] private float accelerationRate = 8f;
    private float horizontalMaxInput = 1f;
    private float movement = 0f;

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

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                currentHorizontalInput = Mathf.MoveTowards(currentHorizontalInput, -horizontalMaxInput, accelerationRate * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
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
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        Physics2D.IgnoreLayerCollision(playerLayer, ballLayer, false);
        isInvincible = false;
    }
}
