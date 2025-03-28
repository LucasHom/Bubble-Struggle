using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeGust : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float fadeDuration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(WaitDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private IEnumerator FadeIn()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color color = spriteRenderer.color;
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            spriteRenderer.color = color;
            yield return null;
        }
    }

    private IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
