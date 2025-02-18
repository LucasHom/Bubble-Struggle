using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RankUpgrades : PuchaseProperties
{
    //Classes
    [SerializeField] private GenerateWater WaterGenerator;
    [SerializeField] private Player Player;


    //MaxUpgrades
    [SerializeField] private int maxAmountUpgrades = 20;

    //Ranks
    private int upgradeRank = 0;

    //Scalars
    private float upgradeMultiplier = 1.15f;

    // Start is called before the first frame update
    void Start()
    {
        StatusText.text = $"Rank {upgradeRank}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Rankupgrade override - only increase size if not max upgraded
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (upgradeRank < maxAmountUpgrades)
        {
            transform.localScale = new Vector3(1f, 1f, 1f) * 1.05f;
        }
    }

    //Rankupgrade override - remove prices
    public override void clickVisuals(int price)
    {
        removeOldPrice(price);
        base.clickVisuals(price);
    }

    //Override for rank upgrade purchase
    public override void AttemptPurchase(Action upgradeAction)
    {
        int price = (int)Mathf.Ceil(basePrice * Mathf.Pow(upgradeMultiplier, upgradeRank));

        if (upgradeRank < maxAmountUpgrades)
        {
            if (ShopManager.currency > price)
            {
                clickVisuals(price);

                upgradeRank += 1;
                StatusText.text = $"Rank {upgradeRank}";
                int nextPrice = (int)Mathf.Ceil(basePrice * Mathf.Pow(upgradeMultiplier, upgradeRank));

                PriceText.text = upgradeRank >= maxAmountUpgrades ? "Max" : $"${nextPrice}";

                upgradeAction();
            }
            else
            {
                notEnough();
            }
        }
        else
        {
            maxedUpgrade();
        }
    }


    //Specific purchases
    public void upgradeWaterCapacity()
    {
        AttemptPurchase(() => { WaterGenerator.maxWater += 1; });
    }

    public void upgradeReloadSpeed()
    {
        AttemptPurchase(() => { WaterGenerator.reloadDelay -= 0.008f; });
    }


    public void upgradePlayerSpeed()
    {
        AttemptPurchase(() => {
            Player.playerSpeed += 0.18f;
            Player.accelerationRate += 0.12f;
        });
    }

    
}
