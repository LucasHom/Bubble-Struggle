using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6f;

    [SerializeField] private Rigidbody2D rb2d;

    private float movement = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Gather input
        movement = Input.GetAxis("Horizontal") * speed;
    }

    private void FixedUpdate()
    {
        //Use gathered input to make actual movements
        //Vector2 could also be Vector2.right * movement
        rb2d.MovePosition(rb2d.position + new Vector2 (movement * Time.fixedDeltaTime, 0f));
    }
}
