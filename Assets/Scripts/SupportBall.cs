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
    [SerializeField] private float SBTransparency = 0.8f;
    [SerializeField] private float maxSize = 1.5f;

    //Create Explosion
    [SerializeField] private GameObject waterPellet;
    [SerializeField] private float maxExplosionForceX = 2f;
    [SerializeField] private float minExplosionForceY = 8f;
    [SerializeField] private float maxExplosionForceY = 12f;
    [SerializeField] private int numExplosionPellets = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb2d.AddForce(supportStartForce, ForceMode2D.Impulse);
        BallGenerator = FindObjectOfType<WaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x >= maxSize)
        {
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
            if (isSBFlashing)
            {
                SupportExplosion();
            }
            Destroy(gameObject);
            BallGenerator.ballsRemaining -= 1;
        }
    }
    public void Grow()
    {  
        if (transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0f);
        }
        rb2d.AddForce(growForce, ForceMode2D.Impulse);
    }

    private IEnumerator FlashSupportBall()
    {
        isSBFlashing = true;
        Color SBColor = spriteRenderer.color;

        while (true)
        {
            if (spriteRenderer.color.a == 1f)
            {

                SBColor.a = SBTransparency;
            }
            else
            {
                SBColor.a = 1f;
            }
            spriteRenderer.color = SBColor;
            yield return new WaitForSeconds(0.15f);
        }
    }

    private void SupportExplosion()
    {
        for (int pellets = 0; pellets < numExplosionPellets; pellets++)
        {
            Vector2 spawnPosition = new Vector2(transform.position.x, -4.1f);
            GameObject pellet = Instantiate(waterPellet, spawnPosition, Quaternion.identity);

            Rigidbody2D waterrb2d = pellet.GetComponent<Rigidbody2D>();

            waterrb2d.AddForce(new Vector2(Random.Range(-maxExplosionForceX, maxExplosionForceX), Random.Range(minExplosionForceY, maxExplosionForceY)), ForceMode2D.Impulse);
        }
    }
}
