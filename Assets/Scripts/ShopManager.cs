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
    [SerializeField] float currencyTextMaxSize;
    [SerializeField] float currencyTextMinSize;
    private int currency = 0;

    
    //Open-Close shop
    public bool isBackgroundToggleReady = false;
    public bool isShopToggleReady = false;
    private bool isBackgroundActive = false;
    [SerializeField] float shopTransitionDelay = 0.5f;
    [SerializeField] private GameObject shopContent;

    // Start is called before the first frame update
    void Start()
    {
        shopContent.SetActive(false);
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
        currencyIndicator.SetActive(!currencyIndicator.activeSelf);
    }

    private void ToggleShop()
    {
        isBackgroundActive = !isBackgroundActive;
        shopContent.SetActive(!shopContent.activeSelf);

        Time.timeScale = isBackgroundActive ? 0f : 1f;
    }

    //Function used in Citizen Manager every once in a while
    public IEnumerator updateCurrency(int amount) 
    {
        //Make text bigger
        float elapsedTime = 0f;
        float lerpDuration = 0.2f;

        while (elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;

            float lerpedSize = Mathf.Lerp(currencyTextMinSize, currencyTextMaxSize, elapsedTime / lerpDuration);
            currencyCounterText.fontSize = lerpedSize;

            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        //Gradually increase currency text
        elapsedTime = 0f;
        lerpDuration = 0.2f;
        int startCurrency = currency;
        int targetCurrency = currency + amount;

        while (elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            int lerpedCurrency = (int)Mathf.Lerp(startCurrency, targetCurrency, elapsedTime / lerpDuration);
            currencyCounterText.text = createCurrencyText(lerpedCurrency);

            yield return null;
        }

        currencyCounterText.text = createCurrencyText(targetCurrency);
        currency = targetCurrency;
        currency += amount;

        yield return new WaitForSeconds(0.25f);

        //Make text smaller
        elapsedTime = 0f;
        lerpDuration = 0.1f;

        while (elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;

            float lerpedSize = Mathf.Lerp(currencyTextMaxSize, currencyTextMinSize, elapsedTime / lerpDuration);
            currencyCounterText.fontSize = lerpedSize;

            yield return null;
        }
    }


    public string createCurrencyText(int amount)
    {
        int clampedAmount = Mathf.Clamp(amount, 0, 999);
        return $"${clampedAmount:000}";
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
