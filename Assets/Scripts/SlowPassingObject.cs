using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowPassingObject : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ball") 
        {
            Debug.Log("Ball entered slow zone");
            col.GetComponent<Ball>().SlowDown();
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
