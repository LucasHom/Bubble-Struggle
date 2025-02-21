using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseUmbrella : ItemPurchase
{
    [SerializeField] GameObject umbrellaPrefab;
    [SerializeField] string notReadyText = "Umbrella max reached";

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
        if (Umbrella.activeUmbrellas.Count < 10)
        {
            setReadyVisual(true);
        }
        else
        {
            setReadyVisual(false);
        }

    }

    public override string NotReadyText()
    {
        return notReadyText;
    }

    public void purchaseUmbrella()
    {

        AttemptPurchase(() => {
            Instantiate(umbrellaPrefab);
        });
    }
}
