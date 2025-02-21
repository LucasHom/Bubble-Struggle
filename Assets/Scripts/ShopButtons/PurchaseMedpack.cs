using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PurchaseMedpack : ItemPurchase
{

    [SerializeField] private CitizenManager Citizen;
    [SerializeField] private GameObject medpackPrefab;

    private float minSpawnX = -7;
    private float maxSpawnX = 7;
    private float spawnY = 5.5f;

    [SerializeField] string notReadyText = "Max health reached";

    // Start is called before the first frame update
    void Start()
    {
        InitializeVisibleFields();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public override void determineIsReady()
    {
        if (Citizen.citizenHealth < Citizen.maxCitizenHealth && Citizen.citizenHealth + Medpack.activeMedpacks < Citizen.maxCitizenHealth)
        {
            setReadyVisual(true);
        }
        else
        {
            setReadyVisual(false);
        }

    }

    public void purchaseMedpack()
    {

        AttemptPurchase(() => {
            Medpack.activeMedpacks++;
            Instantiate(medpackPrefab, new Vector3(Random.Range(minSpawnX, maxSpawnX), spawnY, 0f), Quaternion.identity); 
        });
    }

    public override string NotReadyText()
    {
        return notReadyText;
    }
}
