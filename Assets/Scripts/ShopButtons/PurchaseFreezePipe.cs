using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseFreezePipe : GadgetPurchase
{
    [SerializeField] private GameObject freezePipePrefab;
    [SerializeField] private static string notReadyFloatText = "Out of stock, frozen assets?";

    // Start is called before the first frame update
    void Start()
    {
        InitializeVisibleFields();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void purchaseFreezePipe()
    {
        AttemptPurchase(() => {
            FreezePipe.activeFreezePipes++;
            GameObject pipe = Instantiate(freezePipePrefab, nextPipe.transform.position, Quaternion.identity);
            if (nextPipe.transform.position.x > 0)
            {
                pipe.transform.localScale = new Vector3(-Mathf.Abs(pipe.transform.localScale.x), pipe.transform.localScale.y, pipe.transform.localScale.z);
            }
        });
    }

    public override void determineIsReady()
    {
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
        return $"{maxGadget - FreezePipe.activeFreezePipes}";
    }


}
