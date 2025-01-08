using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    //Currency-Tracking
    [SerializeField] public GameObject currencyIndicator;
    TextMeshProUGUI currencyCounterText;
    private int currency = 0;
    
    //Open-Close shop
    private bool isBackgroundActive = false;
    [SerializeField] float shopTransitionDelay = 0.5f;
    [SerializeField] private Image shopBackgroundImage;

    // Start is called before the first frame update
    void Start()
    {
        shopBackgroundImage.enabled = false;
        Transform currencyCounter = currencyIndicator.transform.Find("CurrencyCounter");
        currencyCounterText = currencyCounter.GetComponent<TextMeshProUGUI>();
        updateCurrency(0);
        //currencyIndicator.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B) && !isBackgroundActive)
        {
            ToggleShop();
            StartCoroutine(DelayShopToggle());
        }
    }

    public void ToggleCurrency()
    {
        bool isCurrencyActive = currencyIndicator.activeSelf;
        currencyIndicator.SetActive(!isCurrencyActive);
    }

    private void ToggleShop()
    {
        shopBackgroundImage.enabled = !shopBackgroundImage.enabled;
    }

    //Function used in Citizen Manager every once in a while
    public void updateCurrency(int amount) {
        currency += amount;

        string newCurrencyText = "";

        for (int i = 3; i > 0; i--)
        {
            if (i == currency.ToString().Length)
            {
                newCurrencyText += currency.ToString();
                break;
            }
            else
            {
                newCurrencyText += "0";
            }
        }
        if (currency >= 999)
        {
            newCurrencyText = "999";
            currency = 999;
        }

        currencyCounterText.text = newCurrencyText;
    }

    private IEnumerator DelayShopToggle()
    {
        isBackgroundActive = true;
        yield return new WaitForSeconds(shopTransitionDelay);
        isBackgroundActive = false; 
    }
}
