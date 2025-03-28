using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePipe : MonoBehaviour
{
    [SerializeField] GameObject freezeGustPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shootGust());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator shootGust()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1f, 2f));

            GameObject freezeGust = Instantiate(freezeGustPrefab, transform.position, Quaternion.identity);

            //Flip gust
            if (transform.localScale.x < 0)
            {
                //Vector3 freezeGustScale = freezeGust.transform.localScale;
                //freezeGustScale.z *= -1;
                //freezeGust.transform.localScale = freezeGustScale;

                freezeGust.transform.rotation = Quaternion.Euler(0, 180, 0);

            }

        }
    }
}
