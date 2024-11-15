using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleCycle : MonoBehaviour
{
    [SerializeField] private float remainDuration = 1.5f;
    [SerializeField] private float fadeDuration = 0.1f;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite altSprite;
    [SerializeField] private ParticleSystem puddleSplash;

    [SerializeField] private float splashStrenthScalar = 5f; 
    private Player player;
    // Start is called before the first frame update

    void Start()
    {

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (Random.Range(0, 2) == 0)
        {
            transform.localScale = new Vector3(-(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        if (Random.Range(0, 2) == 0)
        {
            spriteRenderer.sprite = altSprite;
        }
        StartCoroutine(PuddleLifeCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createPuddleSplash(float ballSpeed) 
    {
        ParticleSystem.MainModule mainModule = puddleSplash.main;
        mainModule.startSpeed = ballSpeed / splashStrenthScalar;
        puddleSplash.Play();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "SupportBall")
        {
            //Debug.Log("got rid of puddle");
            Destroy(gameObject);
        }
        if (col.gameObject.tag == "Player")
        {
            player = col.GetComponent<Player>(); 
            //Debug.Log("slow player");

            player.applyPuddleBuffer(true);

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.GetComponent<Player>();
            //Debug.Log("release player");

            player.applyPuddleBuffer(false);
        }
    }

    private IEnumerator FadeOut()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Color startColor = spriteRenderer.color;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                float a = Mathf.Lerp(startColor.a, 0f, elapsedTime / fadeDuration);
                spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, a);

                elapsedTime += Time.deltaTime;
                yield return null; 
            }
        }

        Destroy(gameObject);
    }

    private IEnumerator PuddleLifeCycle()
    {
        yield return new WaitForSeconds(remainDuration);
        StartCoroutine(FadeOut());
    }


}
