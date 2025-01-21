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

    //Objects
    [SerializeField] private GameObject waterCapacityPriceObject;
    [SerializeField] private TextMeshProUGUI waterCapacityRankText;
    private TextMeshProUGUI waterCapacityPriceText;
    [SerializeField] private GameObject upgradeCostTextPrefab;
    [SerializeField] private GameObject maxedUpgradeTextPrefab;
    [SerializeField] private GameObject notEnoughTextPrefab;

    //Prices
    [SerializeField] private int upgradeWaterCapacityBasePrice = 4;

    //MaxUpgrades
    [SerializeField] private int maxWaterCapacityUpgrades = 20;

    //Ranks
    private int waterCapacityRank = 0;

    //Scalars
    private float upgradeMultiplier = 1.15f;

    private List<GameObject> instantiatedObjects = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        waterCapacityPriceText = waterCapacityPriceObject.GetComponent<TextMeshProUGUI>();

        waterCapacityRankText.text = $"Rank {waterCapacityRank}";
        waterCapacityPriceText.text = $"${upgradeWaterCapacityBasePrice}";
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
        if (waterCapacityRank < maxWaterCapacityUpgrades)
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
        oldCostRectTransform.position = waterCapacityPriceObject.GetComponent<RectTransform>().position;

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
        int price = (int)Mathf.Ceil(upgradeWaterCapacityBasePrice * Mathf.Pow(upgradeMultiplier, waterCapacityRank));

        if (waterCapacityRank < maxWaterCapacityUpgrades)
        {
            if (ShopManager.currency > price)
            {
                clickVisuals(price);

                waterCapacityRank += 1;
                waterCapacityRankText.text = $"Rank {waterCapacityRank}";
                int nextPrice = (int)Mathf.Ceil(upgradeWaterCapacityBasePrice * Mathf.Pow(upgradeMultiplier, waterCapacityRank));

                waterCapacityPriceText.text = waterCapacityRank >= maxWaterCapacityUpgrades ? "Max" : $"${nextPrice}";

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

    //Mkae function to create frame for citizen and player upgrades
}
