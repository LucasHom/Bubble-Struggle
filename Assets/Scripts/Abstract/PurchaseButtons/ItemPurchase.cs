using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ItemPurchase : PurchaseButton
{

    //Ready
    [SerializeField] protected bool isReady = false;
    [SerializeField] protected GameObject notReadyTextPrefab;

    //Ranks
    private int priceRank = default;

    //Scalars
    private float priceMultiplier = 1.1f;

    //Colors
    private Color readyWhite = Color.white;
    private Color readyRed = new Color(193f / 255f, 64f / 255f, 72f / 255f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitializeVisibleFields()
    {
        determineIsReady();
        PriceText = PriceObject.GetComponent<TextMeshProUGUI>();
        PriceText.text = $"${basePrice}";
    }


    //Itempurchase override
    public override void clickVisuals(int price)
    {
        removeOldPrice(price);
        base.clickVisuals(price);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        determineIsReady();
    }

    public abstract void determineIsReady();

    //Override for item purchase
    public override void AttemptPurchase(Action upgradeAction)
    {
        int price = (int)Mathf.Ceil(basePrice * Mathf.Pow(priceMultiplier, priceRank));

        if (isReady)
        {
            if (ShopManager.currency >= price)
            {
                clickVisuals(price);

                priceRank += 1;
                int nextPrice = (int)Mathf.Ceil(basePrice * Mathf.Pow(priceMultiplier, priceRank));

                PriceText.text = $"${nextPrice}";

                upgradeAction();
                determineIsReady();
            }
            else
            {
                notEnough();
            }
        }
        else
        {
            NotReady();
        }
        
    }

    public abstract string GetNotReadyFloatText();

    public abstract string GetStatusAmount();

    protected void NotReady()
    {
        GameObject notReadyText = Instantiate(notReadyTextPrefab, transform);
        notReadyText.GetComponent<TextMeshProUGUI>().text = GetNotReadyFloatText();
        instantiatedObjects.Add(notReadyText);
        RectTransform notReadyTransform = maxedUpgradeTextPrefab.GetComponent<RectTransform>();
        notReadyTransform.position = new Vector3(0f, 0f, 0f);

        StartCoroutine(FloatingText.FloatAndDeleteText(notReadyText, 0.8f));
    }

    public void setReadyVisual(bool ready)
    {
        StatusText.text = $"Ready<space=-0.025>:<scale=0.3> </scale>{GetStatusAmount()}";
        if (ready)
        {
            StatusText.fontSize = 0.5f;
            StatusText.color = readyWhite;
            isReady = true;
        }
        else
        {
            StatusText.fontSize = 0.5f;
            StatusText.color = readyRed;
            isReady = false;
        }
    }

}
