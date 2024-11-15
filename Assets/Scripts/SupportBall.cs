using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportBall : MonoBehaviour
{
    public Vector2 supportStartForce;
    [SerializeField] private Vector2 growForce;
    [SerializeField] private Rigidbody2D rb2d;
    private WaveManager BallGenerator;

    //Flashing Ball
    [SerializeField] private bool isSBFlashing = false;
    [SerializeField] private SpriteRenderer spriteRenderer; 

    // Start is called before the first frame update
    void Start()
    {
        rb2d.AddForce(supportStartForce, ForceMode2D.Impulse);
        BallGenerator = FindObjectOfType<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x >= 2)
        {
            Debug.Log("isFull");
            if (!isSBFlashing)
            {
                StartCoroutine(FlashSupportBall());
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
            BallGenerator.ballsRemaining -= 1;
        }
    }
    public void Grow()
    {  
        if (transform.localScale.x < 2)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        }
        rb2d.AddForce(growForce, ForceMode2D.Impulse);
    }

    private IEnumerator FlashSupportBall()
    {
        isSBFlashing = true;
        Color currentColor = spriteRenderer.color;

        while (true)
        {








            //Toggle transparency and add a whte ball behind original











            //if (currentColor == originalColor)
            //{
            //    spriteRenderer.color = whiterColor;
            //}
            //else
            //{
            //    spriteRenderer.color = originalColor;
            //}
            currentColor = spriteRenderer.color;
            yield return new WaitForSeconds(0.15f);
        }
    }
}
