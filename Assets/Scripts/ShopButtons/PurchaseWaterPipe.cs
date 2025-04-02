using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseWaterPipe : GadgetPurchase
{
    [SerializeField] private GameObject waterPipePrefab;
    [SerializeField] private static string notReadyFloatText = "Out of stock, lost your flow?";

    // Start is called before the first frame update
    void Start()
    {
        InitializeVisibleFields();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void purchaseWaterPipe()
    {
        AttemptPurchase(() => {
            WaterPipe.activeWaterPipes++;
            Instantiate(waterPipePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
        });
    }

    public override void determineIsReady()
    {
        Debug.Log(GetStatusAmount());
        if (GetStatusAmount() != "0")
        {
            setReadyVisual(true);
        }
        else
        {
            setReadyVisual(false);
        }
    }

    public override string GetNotReadyFloatText()
    {
        return notReadyFloatText;
    }

    public override string GetStatusAmount()
    {
        return $"{maxGadget - WaterPipe.activeWaterPipes}";
    }


}
