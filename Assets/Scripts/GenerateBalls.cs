using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBalls : MonoBehaviour
{
    [SerializeField] GameObject largeBallPrefab;
    [SerializeField] GameObject mediumBallPrefab;
    [SerializeField] GameObject smallBallPrefab;
    [SerializeField] GameObject supportBallPrefab;

    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float timeBetweenWave = 4f;
    [SerializeField] float waveSpawns = 3f;
    public int ballsRemaining;

    //[SerializeField] private int currentWave = 0;

    private float minX = -7;
    private float maxX = 7;
    private bool waveIsOver = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        waveIsOver = ballsRemaining < 1;
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {

            for (int spawned = 0; spawned < waveSpawns; spawned++)
            {
                yield return new WaitForSeconds(timeBetweenSpawn);

                Instantiate(largeBallPrefab, new Vector3(Random.Range(minX, maxX), 6.5f, 0f), Quaternion.identity);
                ballsRemaining += 4;
            }

            yield return new WaitUntil(() => waveIsOver);
            yield return new WaitForSeconds(timeBetweenWave);
        }
    }
}
