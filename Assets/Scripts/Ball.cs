using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.Overlays;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Vector2 startForce;

    [SerializeField] private Rigidbody2D rb2d;

    [SerializeField] private GameObject nextBall;
    [SerializeField] private GameObject largePuddle;
    [SerializeField] private GameObject mediumPuddle;
    [SerializeField] private GameObject smallPuddle;
    [SerializeField] private float horizontalBarrierForce = 2f;

    private WaveManager BallGenerator;

    private bool hasSplit = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d.AddForce(startForce, ForceMode2D.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Gets name of prefab without "(Clone)" modifier
    private string getPureName(string oldName)
    {
        string newName = oldName;

        // Remove "(Clone)" if it exists
        if (newName.EndsWith("(Clone)"))
        {
            newName = newName.Substring(0, newName.Length - "(Clone)".Length);
        }
        return newName;
    }

    //Create puddle
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            if (getPureName(gameObject.name) == "ball_large")
            {
                Instantiate(largePuddle, new Vector3(rb2d.position.x, -4.378f, 0f), Quaternion.identity);
            }
            else if (getPureName(gameObject.name) == "ball_medium")
            {
                Instantiate(mediumPuddle, new Vector3(rb2d.position.x, -4.378f, 0f), Quaternion.identity);
            }
            else if (getPureName(gameObject.name) == "ball_small")
            {
                Instantiate(smallPuddle, new Vector3(rb2d.position.x, -4.378f, 0f), Quaternion.identity);
            }
            else 
            {
                Debug.LogError("Ball name cannot be found, cannot create puddle");
            }
        }
        if (col.gameObject.tag == "Player")
        {
            if (!col.gameObject.GetComponent<Player>().playerIsFrozen)
            {
                Debug.Log("Started freeze routine");
                //StartCoroutine(freezePlayer(col.gameObject));
                col.gameObject.GetComponent<Player>().startFreezePlayerCoroutine();

            }
            
        }
    }

    public void Split()
    {
        if (hasSplit) return;
        hasSplit = true;

        if (nextBall != null)
        {
            GameObject ball01 = Instantiate(nextBall, rb2d.position + Vector2.right / 4f, Quaternion.identity);
            GameObject ball02 = Instantiate(nextBall, rb2d.position + Vector2.left / 4f, Quaternion.identity);

            if (nextBall.tag == "Ball")
            {
                ball01.GetComponent<Ball>().startForce = new Vector2(2f, 5f);
                ball02.GetComponent<Ball>().startForce = new Vector2(-2f, 5f);
            }
            if (nextBall.tag == "SupportBall")
            {
                ball01.GetComponent<SupportBall>().supportStartForce = new Vector2(2f, 7f);
                ball02.GetComponent<SupportBall>().supportStartForce = new Vector2(-2f, 7f);
            }

        }

        Destroy(gameObject);
    }

    public void SlowDown()
    {
        rb2d.velocity = new Vector2(0f, 0f);
        rb2d.AddForce(new Vector3(Random.Range(-horizontalBarrierForce, horizontalBarrierForce), 0f, 0f), ForceMode2D.Impulse);
    }
}
