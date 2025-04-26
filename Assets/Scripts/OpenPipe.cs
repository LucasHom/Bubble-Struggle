using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPipe : MonoBehaviour
{
    private ShopManager shopManager;
    private OpenPipeButton buttonScript;
    [SerializeField] private GameObject locationButton;

    private bool lastActiveState = false;


    // Start is called before the first frame update
    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        buttonScript = locationButton.GetComponent<OpenPipeButton>();
        locationButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldBeActive = shopManager.shopContent.activeSelf && GadgetPurchase.waitingForLocation && !buttonScript.occupied;

        if (shouldBeActive != lastActiveState)
        {
            locationButton.SetActive(shouldBeActive);
            lastActiveState = shouldBeActive;
        }
    }

}
