using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float maxWaves = 10;
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
    [SerializeField] private ButtonSecurityManager buttonSecurityManager;

    //Citizens
    [SerializeField] CitizenManager girlfriend;
    [SerializeField] GameObject citizenHealthIndicator;


    //Popups
    Queue<Action> unlockQueue = new Queue<Action>();
    [SerializeField] GameObject popupPrefab;
    //  Popups images
    [SerializeField] Sprite waterTankImage;
    [SerializeField] Sprite netImage;
    [SerializeField] Sprite pigeonImage;
    [SerializeField] Sprite reloadSpeedImage;
    [SerializeField] Sprite umbrellaImage;
    [SerializeField] Sprite swiftShoeImage;
    [SerializeField] Sprite medpackImage;

    //  Popup pipe images
    [SerializeField] Sprite pipeImage;
    [SerializeField] Sprite waterProjImage;
    [SerializeField] Sprite freezeGustImage;
    [SerializeField] Sprite shieldBubbleImage;


    private void Awake()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        cameraManager = GetComponent<CameraManager>();
        shopManager = GetComponent<ShopManager>();
        waveInfo = GetComponent<WaveInfo>();
    }

    void Start()
    {
        //Allocate all popups
        unlockQueue.Enqueue(() => {
            createPopup("Upgrade", "Increase water capacity", "Water Tank", waterTankImage, 4f);
            ButtonSecurityManager.Unlock("WaterCapacityButton");
        });
        unlockQueue.Enqueue(() => {
            createPopup("Item", "Catches up to 15 purified sludge", "Splash Net", netImage, 2.5f);
            ButtonSecurityManager.Unlock("NetButton");
        });
        unlockQueue.Enqueue(() => {
            createPopup("Pipe", "Periodically shoots water", "Water pipe", waterProjImage, 3.2f, pipeImage, new Color(6f / 255f, 154f / 255f, 1f));
            ButtonSecurityManager.Unlock("WaterPipeButton");
        });
        unlockQueue.Enqueue(() => {
            createPopup("Upgrade", "Increase reload speed", "Reload speed", reloadSpeedImage, 4f);
            ButtonSecurityManager.Unlock("ReloadSpeedButton");
        });
        unlockQueue.Enqueue(() => {
            createPopup("Item", "Shields citizen from incoming sludge", "Umbrella", umbrellaImage, 3f);
            ButtonSecurityManager.Unlock("UmbrellaButton");
        });
        unlockQueue.Enqueue(() => {
            createPopup("Pipe", "Periodically shoots a cold gust, momentarily freezing contacted sludge", "Freeze pipe", freezeGustImage, 2f, pipeImage, new Color(71f / 255f, 227f / 255f, 1f));
            ButtonSecurityManager.Unlock("FreezePipeButton");
        });
        unlockQueue.Enqueue(() => {
            createPopup("Upgrade", "Increase player speed", "Swift shoe", swiftShoeImage, 4f);
            ButtonSecurityManager.Unlock("PlayerSpeedButton");
        });
        unlockQueue.Enqueue(() => {
            createPopup("Item", "Crack open to heal citizen", "Medpack", medpackImage, 2.5f);
            ButtonSecurityManager.Unlock("MedpackButton");
        });
        unlockQueue.Enqueue(() => {
            createPopup("Pipe", "Generates shield bubbles to deflect sludge", "Shield bubble pipe", shieldBubbleImage, 2f, pipeImage, new Color(102f / 255f, 148f / 255f, 172f / 255f));
            ButtonSecurityManager.Unlock("ShieldBubblePipeButton");
        });


        //Lock all pipes
        //buttonSecurityManager.Lock("WaterCapacityButton");
        //buttonSecurityManager.Lock("ReloadSpeedButton");
        //buttonSecurityManager.Lock("PlayerSpeedButton");

        //buttonSecurityManager.Lock("NetButton");
        //buttonSecurityManager.Lock("UmbrellaButton");
        //buttonSecurityManager.Lock("MedpackButton");

        //buttonSecurityManager.Lock("WaterPipeButton");
        //buttonSecurityManager.Lock("FreezePipeButton");
        //buttonSecurityManager.Lock("ShieldBubblePipeButton");


        cloudHeightChange = (maxCloudHeight - cloudMovement.startingCloudHeight) / maxWaves;
        citizenHealthIndicator.SetActive(false);
        shopManager.currencyIndicator.SetActive(false);

        randomSpawnPosition = new Vector3(UnityEngine.Random.Range(minXSpawn, maxXSpawn), cloudMovement.transform.position.y, 0f);

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
        while (currentWave <= maxWaves)
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




            yield return StartCoroutine(SpawnWave());



            //Toggle currency off
            ToggleCitizenHealth();
            shopManager.ToggleCurrency();
            shopManager.isShopToggleReady = false;
            shopManager.isBackgroundToggleReady = true;

            if (unlockQueue.Count > 0)
            {
                Action popup = unlockQueue.Dequeue();
                popup();
            }

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

    private IEnumerator SpawnWave()
    {
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
