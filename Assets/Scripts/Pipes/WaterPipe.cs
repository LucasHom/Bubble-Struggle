using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPipe : MonoBehaviour
{
    [SerializeField] private int waterAmount;
    [SerializeField] GameObject waterProjectilePrefab;
    [SerializeField] float shootForce = 8f;
    [SerializeField] private int pipeStrength;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shootWater());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator shootWater()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            float highLowRoll = Random.Range(0f, 1f);

            if (highLowRoll < 0.6f)
            {
                // Random value between 2 and 7
                pipeStrength = (int)Random.Range(2f, 7f);
            }
            else
            {
                // Random value between 15 and 20
                pipeStrength = (int)Random.Range(15f, 20f);
            }

            shootForce = Mathf.Log(pipeStrength, 1.45f);
            waterAmount = (int)Mathf.Log(pipeStrength, 1.35f);
            while (waterAmount > 0)
            {
                yield return new WaitForSeconds(0.04f);
                GameObject waterProjectile = Instantiate(waterProjectilePrefab, transform.position, Quaternion.identity);

                Rigidbody2D rb2d = waterProjectile.GetComponent<Rigidbody2D>();

                Vector2 direction = new Vector2(transform.localScale.x/2, Random.Range(-0.1f, 0.15f)).normalized;

                rb2d.AddForce(direction * shootForce, ForceMode2D.Impulse);

                waterAmount -= 1;
            }
        }
    }
}
