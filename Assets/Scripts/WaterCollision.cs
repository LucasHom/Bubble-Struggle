using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainCollision : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        //Chain.IsFired = false;

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
        if (col.gameObject.tag == "Wall")
        {
            //Special wall particlees?
            Destroy(transform.parent.gameObject);
        }
        if (col.gameObject.tag == "Ground")
        {
            Destroy(transform.parent.gameObject);
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
