using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    //Spawning
    public int ballsRemaining;
    private float minXSpawn = -7;
    private float maxXSpawn = 7;
    
    private Vector3 randomSpawnPosition;


    //Wave Tracking
    private WaveInfo waveInfo;
    [SerializeField] float timeBetweenWave = 4f;
    [SerializeField] private int currentWave = 1;
    private int currentWaveIndex = 0;
    private int currentSubWaveIndex = 0;

    private bool checkSubWaveIsOver = false;
    private bool subWaveIsOver = true;



    //Cloud
    [SerializeField] private float maxCloudHeight = 50f;
    [SerializeField] private float maxWaves = 25;
    private float cloudHeightChange;
    [SerializeField] private CloudMovement cloudMovement;

    //Camera
    private CameraManager cameraManager;
    private CinemachineBrain cinemachineBrain;


    //Transition Text
    [SerializeField] TextMeshProUGUI waveNumText;
    [SerializeField] TextMeshProUGUI waveDescriptionText;

    //Shop
    private ShopManager shopManager;

    //Citizens
    [SerializeField] CitizenManager girlfriend;
    [SerializeField] GameObject citizenHealthIndicator;


    //Popups
    [SerializeField] GameObject popupPrefab;
    //  Popups images
    [SerializeField] Sprite waterTankImage;
    [SerializeField] Sprite pigeonImage;
    //  Popup pipe images
    [SerializeField] Sprite pipeImage;
    [SerializeField] Sprite waterProjImage;


    private void Awake()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        cameraManager = GetComponent<CameraManager>();
        shopManager = GetComponent<ShopManager>();
        waveInfo = GetComponent<WaveInfo>();
    }

    void Start()
    {

        cloudHeightChange = (maxCloudHeight - cloudMovement.startingCloudHeight) / maxWaves;
        citizenHealthIndicator.SetActive(false);
        shopManager.currencyIndicator.SetActive(false);

        randomSpawnPosition = new Vector3(Random.Range(minXSpawn, maxXSpawn), cloudMovement.transform.position.y, 0f);

        StartCoroutine(GameLoop());
    }


    void Update()
    {
        if (checkSubWaveIsOver)
        {
            updateWaveIsOver();
        }
    }

    private void updateWaveIsOver()
    {
        subWaveIsOver = Ball.numActiveBalls < 1;
    }



    private IEnumerator GameLoop()
    {
        while (currentWave < maxWaves)
        {
            cameraManager.SwitchToGameView();

            yield return new WaitForSeconds(1f);
            DisableTransitionText();
            //Toggle currency on
            ToggleCitizenHealth();
            shopManager.ToggleCurrency();
            yield return new WaitUntil(() => !cinemachineBrain.IsBlending);
            createPopup("???", "It seems well-hydrated...", "Stupid pigeon", pigeonImage, 5f);

            shopManager.isShopToggleReady = true;
            shopManager.isBackgroundToggleReady = true;






            currentSubWaveIndex = 0;

            //Check if the current wave is over
            while (currentSubWaveIndex < waveInfo.allWaves[currentWaveIndex].subWaves.Count)
            {
                subWaveIsOver = false;
                waveInfo.SpawnSubWave(currentWaveIndex, currentSubWaveIndex, randomSpawnPosition);

                // Wait for subwave to end
                checkSubWaveIsOver = true;
                yield return new WaitUntil(() => subWaveIsOver);
                checkSubWaveIsOver = false;

                yield return new WaitForSeconds(1f);
                girlfriend.GiveThanks();
                yield return new WaitForSeconds(2f);

                currentSubWaveIndex++;
            }

            currentWaveIndex++;






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
