using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameObject largeBallPrefab;
    [SerializeField] GameObject mediumBallPrefab;
    [SerializeField] GameObject smallBallPrefab;
    [SerializeField] GameObject supportBallPrefab;

    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float timeBetweenWave = 4f;
    [SerializeField] int waveSpawns = 3;
    public int ballsRemaining;

    [SerializeField] private int currentWave = 1;
    [SerializeField] private float maxCloudHeight = 50f;
    [SerializeField] private float maxWaves = 25;
    private float cloudHeightChange;

    private float minXSpawn = -7;
    private float maxXSpawn = 7;
    private bool waveIsOver = true;
    private CameraManager cameraManager;
    private CinemachineBrain cinemachineBrain;
    private CloudMovement cloudMovement;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        cameraManager = FindObjectOfType<CameraManager>();
        cloudMovement = FindObjectOfType<CloudMovement>();
        cloudHeightChange = (maxCloudHeight - cloudMovement.startingCloudHeight) / maxWaves;

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
            cameraManager.SwitchToGameView();
            //This only applies to the large balls
            ballsRemaining += waveSpawns * 8;
            yield return new WaitForSeconds(1f);

            for (int spawned = 0; spawned < waveSpawns; spawned++)
            {
                yield return new WaitForSeconds(timeBetweenSpawn);
                Instantiate(largeBallPrefab, new Vector3(Random.Range(minXSpawn, maxXSpawn), cloudMovement.transform.position.y, 0f), Quaternion.identity);
                
            }

            yield return new WaitUntil(() => waveIsOver);
            

            yield return new WaitForSeconds(1f);

            cameraManager.SwitchToCloudView();
            yield return new WaitUntil(() => !cinemachineBrain.IsBlending);
            yield return new WaitForSeconds(2f);

            if (currentWave < maxWaves)
            {
                cloudMovement.ChangeCloudHeight(cloudHeightChange);
            }
            yield return new WaitForSeconds(2f);

            cameraManager.SwitchToWaveView();
            currentWave++;
            yield return new WaitUntil(() => !cinemachineBrain.IsBlending);

            yield return new WaitForSeconds(timeBetweenWave);
        }
    }
}
