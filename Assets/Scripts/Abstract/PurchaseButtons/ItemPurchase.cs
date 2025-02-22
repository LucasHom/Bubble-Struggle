using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ItemPurchase : PurchaseButton
{
    [SerializeField] protected bool isReady = false;
    [SerializeField] protected GameObject notReadyTextPrefab;

    //Ranks
    private int priceRank = default;

    //Scalars
    private float priceMultiplier = 1.1f;

    //Colors
    private Color readyGreen = new Color(103f / 255f, 190f / 255f, 109f / 255f);
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
            Debug.Log("Floating text");
            NotReady();
        }
        
    }

    public abstract string NotReadyText();

    protected void NotReady()
    {
        GameObject notReadyText = Instantiate(notReadyTextPrefab, transform);
        notReadyText.GetComponent<TextMeshProUGUI>().text = NotReadyText();
        instantiatedObjects.Add(notReadyText);
        RectTransform notReadyTransform = maxedUpgradeTextPrefab.GetComponent<RectTransform>();
        notReadyTransform.position = new Vector3(0f, 0f, 0f);

        StartCoroutine(FloatingText.FloatAndDeleteText(notReadyText, 0.8f));
    }

    public void setReadyVisual(bool ready)
    {
        if (ready)
        {
            StatusText.fontSize = 0.5f;
            StatusText.color = readyGreen;
            StatusText.text = $"Ready";
            isReady = true;
        }
        else
        {
            StatusText.fontSize = 0.4f;
            StatusText.color = readyRed;
            StatusText.text = $"Not Ready";
            isReady = false;
        }
    }

}
