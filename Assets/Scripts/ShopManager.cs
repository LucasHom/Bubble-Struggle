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
    public bool isBackgroundToggleReady = false;
    public bool isShopToggleReady = false;
    private bool isBackgroundActive = false;
    [SerializeField] float shopTransitionDelay = 0.5f;
    [SerializeField] private Image shopBackgroundImage;

    // Start is called before the first frame update
    void Start()
    {
        shopBackgroundImage.enabled = false;
        isBackgroundActive = false;
        Transform currencyCounter = currencyIndicator.transform.Find("CurrencyCounter");
        currencyCounterText = currencyCounter.GetComponent<TextMeshProUGUI>();
        updateCurrency(0);
        //currencyIndicator.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.B) && isBackgroundToggleReady && isShopToggleReady)
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
        isBackgroundActive = !isBackgroundActive;
        shopBackgroundImage.enabled = !shopBackgroundImage.enabled;

        Time.timeScale = isBackgroundActive ? 0f : 1f;
    }

    //Function used in Citizen Manager every once in a while
    public void updateCurrency(int amount) {
        currency += amount;

        string newCurrencyText = "$";

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
            newCurrencyText = "$999";
            currency = 999;
        }

        currencyCounterText.text = newCurrencyText;
    }

    private IEnumerator DelayShopToggle()
    {
        isBackgroundToggleReady = false;
        float elapsedTime = 0f;
        while (elapsedTime < shopTransitionDelay)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        isBackgroundToggleReady = true;

    }
}
