using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainCollision : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Something hit the chain");
        Chain.IsFired = false;

        if (col.tag == "Ball")
        {
            Debug.Log("Split ball in two");
            col.GetComponent<Ball>().Split();
        }
        if (col.tag == "SupportBall")
        {
            Debug.Log("Hit support");
            col.GetComponent<SupportBall>().Grow();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
