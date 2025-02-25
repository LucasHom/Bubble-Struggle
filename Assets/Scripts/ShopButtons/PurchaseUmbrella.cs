using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseUmbrella : ItemPurchase
{
    [SerializeField] GameObject umbrellaPrefab;
    [SerializeField] int maxUmbrellas = 10;

    [SerializeField] private static string notReadyFloatText = "Out of stock, pour timing!";

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
        if (Umbrella.activeUmbrellas.Count < maxUmbrellas)
        {
            setReadyVisual(true);
        }
        else
        {
            setReadyVisual(false);
        }

    }


    public void purchaseUmbrella()
    {

        AttemptPurchase(() => {
            Instantiate(umbrellaPrefab);
        });
    }
    public override string GetNotReadyFloatText()
    {
        return notReadyFloatText;
    }

    public override string GetStatusAmount()
    {
        return $"{maxUmbrellas - Umbrella.activeUmbrellas.Count}";
    }
}
