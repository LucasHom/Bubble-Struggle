using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ItemPurchase : PurchaseButton
{
    [SerializeField] protected bool isReady = false;

    //Ranks
    private int priceRank = default;

    //Scalars
    private float priceMultiplier = 1.15f;

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

    //Rankupgrade override - only increase size if not max upgraded
    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (isReady)
        {
            transform.localScale = new Vector3(1f, 1f, 1f) * 1.05f;
        }
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
            if (ShopManager.currency > price)
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
            Debug.Log("Float text purchase not ready");
        }
        
    }

}
