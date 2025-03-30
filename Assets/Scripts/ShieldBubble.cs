using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubble : MonoBehaviour
{

    [SerializeField] private float growDuration = 0.25f;
    [SerializeField] private float shrinkDuration = 0.1f;

    [SerializeField] private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector2(0.2f, 0.2f);


        rb2d.bodyType = RigidbodyType2D.Dynamic;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GameObject col = collision.gameObject;
        GameObject col = collision.GetContact(0).collider.gameObject;
        Debug.Log(col.tag);
        if (col.tag == "Ground")
        {
            rb2d.bodyType = RigidbodyType2D.Kinematic;
            StartCoroutine(GrowEffect());

        }

        if (col.tag == "BallGuard")
        {

            //start pop coroutine or delete health
        }
        if (col.tag == "Ball")
        {
            col.GetComponent<Ball>().Split();

            //start pop coroutine or delete health
        }
    }

    private IEnumerator GrowEffect()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 shrunkScale = transform.localScale - new Vector3(0.1f, 0.1f);
        Vector3 targetScale = new Vector3(2f, 2f, 1f);

        // Shrink phase
        float elapsedTime = 0f;
        while (elapsedTime < shrinkDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, shrunkScale, elapsedTime / shrinkDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = Vector3.zero;

        // Grow phase
        elapsedTime = 0f;
        while (elapsedTime < growDuration)
        {
            transform.localScale = Vector3.Lerp(shrunkScale, targetScale, elapsedTime / growDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;


    }
}
