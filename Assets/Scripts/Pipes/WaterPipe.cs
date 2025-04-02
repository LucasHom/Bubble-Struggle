using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPipe : MonoBehaviour
{
    [SerializeField] private int waterAmount;
    [SerializeField] GameObject waterProjectilePrefab;
    [SerializeField] float shootForce = 8f;
    [SerializeField] private int pipeStrength;

    [SerializeField] private float shootWait;

    public static int activeWaterPipes = default;

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
            yield return new WaitForSeconds(shootWait);

            float highLowRoll = Random.Range(0f, 1f);

            if (highLowRoll < 0.6f)
            {
                pipeStrength = (int)Random.Range(2f, 4f);
            }
            else
            {
                pipeStrength = (int)Random.Range(8f, 10f);
            }

            //shootForce = Mathf.Log(pipeStrength, 1.45f);
            //waterAmount = (int)Mathf.Log(pipeStrength, 1.35f);
            shootForce = Mathf.Log(pipeStrength, 1.3f);
            waterAmount = (int)Mathf.Pow(pipeStrength, 1.2f);
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
