using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] GameObject unitInfo;
    [SerializeField] PopupUnitEffects popupUnitEffects;
    [SerializeField] GameObject continueText;

    [SerializeField] TextMeshProUGUI unitTypeText;
    [SerializeField] TextMeshProUGUI unitDescText;
    [SerializeField] TextMeshProUGUI unitNameText;
    [SerializeField] Image unitImage;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f; // Pause the game
        unitInfo.SetActive(false);
        continueText.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (popupUnitEffects.isIdle)
        {
            unitInfo.SetActive(true);
            unitInfo.GetComponent<FadeInUI>().FadeIn();

            if (unitInfo.GetComponent<FadeInUI>().isVisible)
            {
                continueText.SetActive(true);
                // Check if the user presses any key
                if (Input.anyKeyDown)
                {
                    Time.timeScale = 1f; // Resume the game
                    Destroy(gameObject);
                }
            }

        }
    }

    public void SetUnitInfo(string unitType, string unitDesc, string unitName, Sprite unitSprite, float spriteSizeMult)
    {
        unitTypeText.text = unitType;
        unitDescText.text = unitDesc;
        unitNameText.text = unitName;
        unitImage.sprite = unitSprite;
        //Set unit sprite size
        unitImage.GetComponent<RectTransform>().sizeDelta = new Vector2(unitSprite.rect.width * spriteSizeMult, unitSprite.rect.height * spriteSizeMult);
    }
}