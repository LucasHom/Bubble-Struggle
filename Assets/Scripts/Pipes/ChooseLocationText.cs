using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseLocationText : MonoBehaviour
{
    private TextMeshProUGUI myText;
    [SerializeField] float baseFontSize;
    [SerializeField] float fontSizeMultiplier;
    private Coroutine emphasizeTextCoroutine = null;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(EmphasizeText());
    }

    // Update is called once per frame
    void Update()
    {
        if (GadgetPurchase.waitingForLocation)
        {
            if (emphasizeTextCoroutine == null)
            {
                emphasizeTextCoroutine = StartCoroutine(EmphasizeText());
            }
            myText.enabled = true;
        }
        else
        {
            if (emphasizeTextCoroutine != null)
            {
                StopCoroutine(emphasizeTextCoroutine);
                emphasizeTextCoroutine = null;
            }
            myText.enabled = false;
        }
    }

    private IEnumerator EmphasizeText()
    {
        TextMeshProUGUI text = myText;

        float elapsedTime = 0f;
        float growTime = 0.6f;
        float shrinkTime = 0.6f;

        while (true)
        {
            elapsedTime = 0f;

            // Growing
            while (elapsedTime < growTime)
            {
                elapsedTime += Time.unscaledDeltaTime;

                float newFontSize = Mathf.Lerp(baseFontSize, baseFontSize * fontSizeMultiplier, EaseIn(elapsedTime / growTime));

                text.fontSize = newFontSize;

                yield return null;
            }

            //Shrinking
            elapsedTime = 0f;

            while (elapsedTime < shrinkTime)
            {
                elapsedTime += Time.unscaledDeltaTime;

                float newFontSize = Mathf.Lerp(baseFontSize * fontSizeMultiplier, baseFontSize, EaseOut(elapsedTime / growTime));

                text.fontSize = newFontSize;

                yield return null;
            }
        }
    }

    private float EaseOut(float time)
    {
        return Mathf.Pow(time - 1f, 3) + 1f;

    }

    private float EaseIn(float time)
    {
        return Mathf.Pow(time, 3);
    }
}
