using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] GameObject unitInfo;
    [SerializeField] PopupUnitEffects popupUnitEffects;
    [SerializeField] GameObject continueText;

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
}
