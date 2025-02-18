using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RankUpgrades : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Classes
    [SerializeField] private GenerateWater WaterGenerator;
    [SerializeField] private ShopManager ShopManager;
    [SerializeField] private FloatingUpgradeText FloatingText;
    [SerializeField] private Player Player;

    //Objects
    [SerializeField] private GameObject PriceObject;
    [SerializeField] private TextMeshProUGUI RankText;
    private TextMeshProUGUI PriceText;
    [SerializeField] private GameObject upgradeCostTextPrefab;
    [SerializeField] private GameObject maxedUpgradeTextPrefab;
    [SerializeField] private GameObject notEnoughTextPrefab;

    //Prices
    [SerializeField] private int upgradeBasePrice = 4;

    //MaxUpgrades
    [SerializeField] private int maxAmountUpgrades = 20;

    //Ranks
    private int upgradeRank = 0;

    //Scalars
    private float upgradeMultiplier = 1.15f;

    private List<GameObject> instantiatedObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        PriceText = PriceObject.GetComponent<TextMeshProUGUI>();

        RankText.text = $"Rank {upgradeRank}";
        PriceText.text = $"${upgradeBasePrice}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        foreach (GameObject obj in instantiatedObjects)
        {
            Destroy(obj);
        }

        instantiatedObjects.Clear();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (upgradeRank < maxAmountUpgrades)
        {
            transform.localScale = new Vector3(1f, 1f, 1f) * 1.05f;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void notEnough()
    {
        GameObject notEnoughText = Instantiate(notEnoughTextPrefab, transform);
        instantiatedObjects.Add(notEnoughText);
        RectTransform notEnoughTransform = maxedUpgradeTextPrefab.GetComponent<RectTransform>();
        notEnoughTransform.position = new Vector3(0f, 0f, 0f);

        StartCoroutine(FloatingText.FloatAndDeleteText(notEnoughText, 0.8f));
    }
    
    private void maxedUpgrade()
    {
        GameObject maxedUpgradeText = Instantiate(maxedUpgradeTextPrefab, transform);
        instantiatedObjects.Add(maxedUpgradeText);
        RectTransform maxedUpgradeTransform = maxedUpgradeTextPrefab.GetComponent<RectTransform>();
        maxedUpgradeTransform.position = new Vector3(0f, 0f, 0f);

        StartCoroutine(FloatingText.FloatAndDeleteText(maxedUpgradeText, 0.8f));
    }

    private IEnumerator ClickEffect()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSecondsRealtime(0.06f);
        transform.localScale = new Vector3(1f, 1f, 1f) * 1.05f;
    }

    private void removeOldPrice(int price)
    {
        ShopManager.currency -= price;
        ShopManager.updateCurrency();

        GameObject oldUpgradeCostText = Instantiate(upgradeCostTextPrefab, transform);
        instantiatedObjects.Add(oldUpgradeCostText);
        RectTransform oldCostRectTransform = oldUpgradeCostText.GetComponent<RectTransform>();
        oldCostRectTransform.position = PriceObject.GetComponent<RectTransform>().position;

        oldUpgradeCostText.GetComponent<TextMeshProUGUI>().text = $"${price}";
        
        StartCoroutine(FloatingText.FloatAndDeleteText(oldUpgradeCostText, 0.4f));
    }

    private void clickVisuals(int price)
    {
        removeOldPrice(price);
        StartCoroutine(ClickEffect());  
        //Activate particle system
    }

    public void upgradeWaterCapacity()
    {
        int price = (int)Mathf.Ceil(upgradeBasePrice * Mathf.Pow(upgradeMultiplier, upgradeRank));

        if (upgradeRank < maxAmountUpgrades)
        {
            if (ShopManager.currency > price)
            {
                clickVisuals(price);

                upgradeRank += 1;
                RankText.text = $"Rank {upgradeRank}";
                int nextPrice = (int)Mathf.Ceil(upgradeBasePrice * Mathf.Pow(upgradeMultiplier, upgradeRank));

                PriceText.text = upgradeRank >= maxAmountUpgrades ? "Max" : $"${nextPrice}";

                WaterGenerator.maxWater += 1;
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

    public void upgradeReloadSpeed()
    {
        int price = (int)Mathf.Ceil(upgradeBasePrice * Mathf.Pow(upgradeMultiplier, upgradeRank));

        if (upgradeRank < maxAmountUpgrades)
        {
            if (ShopManager.currency > price)
            {
                clickVisuals(price);

                upgradeRank += 1;
                RankText.text = $"Rank {upgradeRank}";
                int nextPrice = (int)Mathf.Ceil(upgradeBasePrice * Mathf.Pow(upgradeMultiplier, upgradeRank));

                PriceText.text = upgradeRank >= maxAmountUpgrades ? "Max" : $"${nextPrice}";

                WaterGenerator.reloadDelay -= 0.008f;
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


    public void upgradePlayerSpeed()
    {
        int price = (int)Mathf.Ceil(upgradeBasePrice * Mathf.Pow(upgradeMultiplier, upgradeRank));

        if (upgradeRank < maxAmountUpgrades)
        {
            if (ShopManager.currency > price)
            {
                clickVisuals(price);

                upgradeRank += 1;
                RankText.text = $"Rank {upgradeRank}";
                int nextPrice = (int)Mathf.Ceil(upgradeBasePrice * Mathf.Pow(upgradeMultiplier, upgradeRank));

                PriceText.text = upgradeRank >= maxAmountUpgrades ? "Max" : $"${nextPrice}";

                Player.playerSpeed += 0.18f;
                Player.accelerationRate += 0.12f;
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

    //Mkae function to create frame for citizen and player upgrades
}
