using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubblePipe : MonoBehaviour
{
    [SerializeField] GameObject shieldBubblePrefab;
    private Vector2 shootForce;
    private Vector2 previousShootForce;

    [SerializeField] private float maxShootForce;
    [SerializeField] private float minShootForce;

    [SerializeField] private float maxShootWait;
    [SerializeField] private float minShootWait;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shootShieldBubble());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator shootShieldBubble()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minShootWait, maxShootWait));
            Debug.Log(ShieldBubble.numActive);
            if (ShieldBubble.numActive < 2)
            {
                GameObject shieldBubble = Instantiate(shieldBubblePrefab, transform.position, Quaternion.identity);

                Rigidbody2D rb2d = shieldBubble.GetComponent<Rigidbody2D>();

                Vector2 direction = new Vector2(transform.localScale.x / 2, 0f).normalized;


                shootForce = new Vector2(Random.Range(minShootForce, maxShootForce), 1f);
                while (Mathf.Abs(shootForce.x - previousShootForce.x) < 2f)
                {
                    shootForce = new Vector2(Random.Range(minShootForce, maxShootForce), 1f);
                }
                previousShootForce = shootForce;
                 
                rb2d.AddForce(direction * shootForce, ForceMode2D.Impulse);
            }

        }
    }
}
