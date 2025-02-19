using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PurchaseMedpack : ItemPurchase
{

    [SerializeField] private CitizenManager Citizen;

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
        if (Citizen.citizenHealth < Citizen.maxCitizenHealth)
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
        AttemptPurchase(() => { Citizen.citizenHealth += 1; });
    }
}
