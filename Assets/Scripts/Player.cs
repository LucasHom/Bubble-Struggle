using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float playerSpeed = 6f;

    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] public float puddleBuffer = 2f;

    [SerializeField] public bool playerHealthy = true;
    [SerializeField] public bool isReloading = false;

    private float appliedPuddleBuffer = 0f;
    private float movement = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void getPlayerMovement()
    {
        if (!playerHealthy)
        {
            spriteRenderer.color = Color.gray;
            movement = 0f;
            
        }
        else if (playerHealthy && !isReloading)
        {
            spriteRenderer.color = new Color(255f, 255f, 255f);
            movement = Input.GetAxis("Horizontal") * (playerSpeed - appliedPuddleBuffer);
        }
        else
        {
            //means player is healthy and player is reloading
            spriteRenderer.color = new Color(255f, 255f, 255f);
            movement = 0f;
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

        rb2d.velocity = new Vector2(movement, 0f);
    }
}
