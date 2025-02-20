using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medpack : MonoBehaviour
{
    [SerializeField] float maxStartForceX = 3f;

    [SerializeField] private Vector2 hitForce = new Vector2(0f, 2.5f);
    [SerializeField] private Rigidbody2D rb2d;

    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private ParticleSystem mainPS;
    [SerializeField] private ParticleSystem healthExplosionPS;

    public static int activeMedpacks = 0;


    // Start is called before the first frame update
    void Start()
    {
        //activeMedpacks++;
        rb2d.AddForce(new Vector2(Random.Range(-maxStartForceX, maxStartForceX), 0f), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Bounce when hit ground or chain, lose health when hit chain
    public void Bounce()
    {
        rb2d.AddForce(hitForce, ForceMode2D.Impulse);
    }

    private IEnumerator FlashyDestroy()
    {
        mainPS.Stop();
        //Play animation if get working
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        Destroy(GetComponent<CircleCollider2D>());
        healthExplosionPS.Play();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        float startAlpha = color.a;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0, timeElapsed / fadeDuration);
            spriteRenderer.color = color;
            yield return null;
        }

        activeMedpacks--;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.tag == "Citizen")
        {
            //Add 1 health to citizen
            CitizenManager cman = col.GetComponent<CitizenManager>();
            cman.setCitizenHealth(cman.getCitizenHealth() + 1);


            StartCoroutine(FlashyDestroy());

        }
    }
}
