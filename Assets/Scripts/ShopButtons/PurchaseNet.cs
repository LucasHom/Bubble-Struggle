using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseNet : ItemPurchase
{
    [SerializeField] private GameObject netPrefab;

    [SerializeField] string notReadyText = "Max nets reached";

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
        if (Net.activeNets.Count < 5)
        {
            setReadyVisual(true);
        }
        else
        {
            setReadyVisual(false);
        }

    }

    public void purchaseNet()
    {

        AttemptPurchase(() => {
            Instantiate(netPrefab);
        });
    }

    public override string NotReadyText()
    {
        return notReadyText;
    }
}
