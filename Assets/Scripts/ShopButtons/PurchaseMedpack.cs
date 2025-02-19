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
            StatusText.fontSize = 0.5f;
            StatusText.color = Color.green;
            StatusText.text = $"Ready";
            isReady = true;
        }
        else
        {
            StatusText.fontSize = 0.4f;
            StatusText.color = Color.red;
            StatusText.text = $"Not Ready";
            isReady = false;
        }

    }

    public void purchaseMedpack()
    {
        AttemptPurchase(() => { Citizen.citizenHealth += 1; });
    }
}
