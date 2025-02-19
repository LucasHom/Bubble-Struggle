using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    //Objects to Spawn
    [SerializeField] GameObject largeBallPrefab;
    [SerializeField] GameObject mediumBallPrefab;
    [SerializeField] GameObject smallBallPrefab;
    [SerializeField] GameObject largeBallGuardedPrefab;
    [SerializeField] GameObject supportBallPrefab;
    [SerializeField] GameObject umbrella;

    //Spawning
    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float timeBetweenWave = 4f;
    [SerializeField] int waveSpawns = 3;
    public int ballsRemaining;
    private float minXSpawn = -7;
    private float maxXSpawn = 7;

    //Cloud
    [SerializeField] private float maxCloudHeight = 50f;
    [SerializeField] private float maxWaves = 25;
    private float cloudHeightChange;
    private CloudMovement cloudMovement;

    //Camera
    private CameraManager cameraManager;
    private CinemachineBrain cinemachineBrain;

    //Wave Tracking
    [SerializeField] private int currentWave = 1;
    private bool waveIsOver = true;

    //Transition Text
    [SerializeField] TextMeshProUGUI waveNumText;
    [SerializeField] TextMeshProUGUI waveDescriptionText;

    //Shop
    private ShopManager shopManager;

    //Citizens
    [SerializeField] CitizenManager girlfriend;

    void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        cameraManager = FindObjectOfType<CameraManager>();
        cloudMovement = FindObjectOfType<CloudMovement>();
        shopManager = FindObjectOfType<ShopManager>();
        cloudHeightChange = (maxCloudHeight - cloudMovement.startingCloudHeight) / maxWaves;
        shopManager.currencyIndicator.SetActive(false);

        StartCoroutine(SpawnWave());
    }


    void Update()
    {
        waveIsOver = ballsRemaining < 1;


        //DEv thing
        if (Input.GetKeyDown(KeyCode.U))
        {
            Instantiate(umbrella);
        }
        
    }

    IEnumerator SpawnWave()
    {
        while (true)
        {
            cameraManager.SwitchToGameView();
            //This only applies to the large balls
            ballsRemaining += waveSpawns * 8;
            yield return new WaitForSeconds(1f);
            DisableTransitionText();
            //Toggle currency on
            shopManager.ToggleCurrency();
            yield return new WaitUntil(() => !cinemachineBrain.IsBlending);
            shopManager.isShopToggleReady = true;
            shopManager.isBackgroundToggleReady = true;

            for (int spawned = 0; spawned < waveSpawns; spawned++)
            {
                yield return new WaitForSeconds(timeBetweenSpawn);
                Instantiate(largeBallPrefab, new Vector3(Random.Range(minXSpawn, maxXSpawn), cloudMovement.transform.position.y, 0f), Quaternion.identity);
            }

            yield return new WaitUntil(() => waveIsOver);
            girlfriend.GiveThanks();

            yield return new WaitForSeconds(3f);
            //Toggle currency off
            shopManager.ToggleCurrency();
            shopManager.isShopToggleReady = false;
            shopManager.isBackgroundToggleReady = true;

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
            yield return new WaitForSeconds(2f);

            EnableTransitionText();
            yield return new WaitForSeconds(timeBetweenWave);
 
        }
    }

    private void EnableTransitionText()
    {
        waveNumText.enabled = true;
        waveDescriptionText.enabled = true;
    }

    private void DisableTransitionText()
    {
        waveNumText.enabled = false;
        waveDescriptionText.enabled = false;
    }
}
