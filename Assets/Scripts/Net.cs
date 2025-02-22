using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Net : MonoBehaviour
{
    private Coroutine slowDownRoutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.tag == "SupportBall")
        {
            SupportBall supportBall = col.GetComponent<SupportBall>();
            if (supportBall.isMaxSize == false)
            {
                slowDownRoutine = StartCoroutine(supportBall.StickToNet(gameObject));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.tag == "SupportBall")
        {
            Rigidbody2D ballrb2d = col.GetComponent<Rigidbody2D>();
            ballrb2d.gravityScale = 1f;
            StopCoroutine(slowDownRoutine);
            slowDownRoutine = null;

        }
    }
}
