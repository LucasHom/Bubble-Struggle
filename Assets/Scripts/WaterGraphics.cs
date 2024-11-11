using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGraphics : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Vector3 initialScale = new Vector3(1f, 1f, 1f);

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {

        if (rb2d.velocity.y >= 0)
        {
            transform.localScale = new Vector3(initialScale.x, -Mathf.Abs(initialScale.y), initialScale.z);
        }
        else
        {
            transform.localScale = new Vector3(initialScale.x, Mathf.Abs(initialScale.y), initialScale.z);
        }
    }
}
