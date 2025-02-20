using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medpack : MonoBehaviour
{
    [SerializeField] float maxStartForceX = 3f;

    [SerializeField] private Vector2 hitForce = new Vector2(0f, 2.5f);
    [SerializeField] private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d.AddForce(new Vector2(Random.Range(-maxStartForceX, maxStartForceX), 0f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Bounce when hit ground or chain, lose health when hit chain
    public void Weaken()
    {
        rb2d.AddForce(hitForce, ForceMode2D.Impulse);
    }
}
