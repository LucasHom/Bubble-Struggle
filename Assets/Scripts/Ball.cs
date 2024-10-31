using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Vector2 startForce;
    [SerializeField] private Rigidbody2D rb2d;

    public GameObject nextBall;

    private GenerateBalls BallGenerator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d.AddForce(startForce, ForceMode2D.Impulse);
        BallGenerator = FindObjectOfType<GenerateBalls>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Split()
    {
        if(nextBall != null)
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
                BallGenerator.ballsRemaining -= 1;
                Debug.Log(BallGenerator.ballsRemaining);
                ball01.GetComponent<SupportBall>().supportStartForce = new Vector2(2f, 7f);
                ball02.GetComponent<SupportBall>().supportStartForce = new Vector2(-2f, 7f);
            }

        }

        Destroy(gameObject);
    }
}
