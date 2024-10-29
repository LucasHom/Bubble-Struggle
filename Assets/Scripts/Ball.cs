using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Vector2 startForce;

    [SerializeField] private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d.AddForce(startForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
