using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseNet : ItemPurchase
{
    [SerializeField] private GameObject netPrefab;

    [SerializeField] private static string notReadyFloatText = "Out of stock, net loss!";

    [SerializeField] int maxNets = 5;

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
        if (Net.activeNets.Count < maxNets)
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

    public override string GetNotReadyFloatText()
    {
        return notReadyFloatText;
    }

    public override string GetStatusAmount()
    {
        return $"{maxNets - Net.activeNets.Count}";
    }
}
