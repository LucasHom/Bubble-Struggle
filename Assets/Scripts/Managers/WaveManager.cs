using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    //Objects to Spawn
    [SerializeField] GameObject largeBallPrefab;
    [SerializeField] GameObject mediumBallPrefab;
    [SerializeField] GameObject smallBallPrefab;
    [SerializeField] GameObject largeBallGuardedPrefab;
    [SerializeField] GameObject supportBallPrefab;



    //Spawning
    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float timeBetweenWave = 4f;
    [SerializeField] int waveSpawns = 3;
    private bool checkWaveIsOver = false;

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
    [SerializeField] int tempBalls;
    [SerializeField] GameObject citizenHealthIndicator;

    //Popups
    [SerializeField] GameObject popupPrefab;
    //  Popups images
    [SerializeField] Sprite waterTankImage;
    //  Popup pipe images
    [SerializeField] Sprite pipeImage;
    [SerializeField] Sprite waterProjImage;




    void Start()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        cameraManager = FindObjectOfType<CameraManager>();
        cloudMovement = FindObjectOfType<CloudMovement>();
        shopManager = FindObjectOfType<ShopManager>();
        cloudHeightChange = (maxCloudHeight - cloudMovement.startingCloudHeight) / maxWaves;
        citizenHealthIndicator.SetActive(false);
        shopManager.currencyIndicator.SetActive(false);



        StartCoroutine(SpawnWave());
    }


    void Update()
    {
        tempBalls = Ball.numActiveBalls;

        if (checkWaveIsOver)
        {
            updateWaveIsOver();
        }

    }

    private void updateWaveIsOver()
    {
        waveIsOver = Ball.numActiveBalls < 1;
    }



    IEnumerator SpawnWave()
    {
        while (true)
        {

            cameraManager.SwitchToGameView();

            yield return new WaitForSeconds(1f);
            DisableTransitionText();
            //Toggle currency on
            ToggleCitizenHealth();
            shopManager.ToggleCurrency();
            yield return new WaitUntil(() => !cinemachineBrain.IsBlending);
            shopManager.isShopToggleReady = true;
            shopManager.isBackgroundToggleReady = true;

            waveIsOver = false;
            


            for (int spawned = 0; spawned < waveSpawns; spawned++)
            {
                yield return new WaitForSeconds(timeBetweenSpawn);
                Instantiate(largeBallPrefab, new Vector3(Random.Range(minXSpawn, maxXSpawn), cloudMovement.transform.position.y, 0f), Quaternion.identity);
            }


            checkWaveIsOver = true;
            yield return new WaitUntil(() => waveIsOver);
            checkWaveIsOver = false;


            girlfriend.GiveThanks();

            yield return new WaitForSeconds(3f);
            //Toggle currency off
            ToggleCitizenHealth();
            shopManager.ToggleCurrency();
            shopManager.isShopToggleReady = false;
            shopManager.isBackgroundToggleReady = true;
            createPopup("Upgrade", "Increase the amount of water you can hold", "Water Tank", waterTankImage, 4f);
            //PipePopup example
            //createPopup("Upgrade", "Increase the amount of water you can hold", "Water Tank", waterProjImage, 3.2f, pipeImage, new Color(154f / 255f, 1f, 1f));

            cameraManager.SwitchToCloudView();
            yield return new WaitUntil(() => !cinemachineBrain.IsBlending);
            yield return new WaitForSeconds(2f);


            if (currentWave < maxWaves)
            {
                StartCoroutine(cloudMovement.ChangeCloudHeight(cloudHeightChange));
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

    private void createPopup(string unitType, string unitDesc, string unitName, Sprite unitSprite, float spriteSizeMult, Sprite pipeSprite = null, Color pipeColor = default)
    {
        GameObject popup = Instantiate(popupPrefab, new Vector2(382f, 215f), Quaternion.identity);
        popup.GetComponent<Popup>().SetUnitInfo(unitType, unitDesc, unitName, unitSprite, spriteSizeMult, pipeSprite, pipeColor);
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

    private void ToggleCitizenHealth()
    {
        citizenHealthIndicator.SetActive(!citizenHealthIndicator.activeSelf);
    }
}
