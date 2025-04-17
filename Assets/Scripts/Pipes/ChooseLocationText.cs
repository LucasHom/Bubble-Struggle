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

    private EmphasizeText emphasizeText;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        if (GetComponent<EmphasizeText>() != null)
        {
            emphasizeText = GetComponent<EmphasizeText>();
        }

        StartCoroutine(emphasizeText.Emphasize(myText, baseFontSize, fontSizeMultiplier));
    }

    // Update is called once per frame
    void Update()
    {
        if (GadgetPurchase.waitingForLocation)
        {
            if (emphasizeTextCoroutine == null)
            {
                emphasizeTextCoroutine = StartCoroutine(emphasizeText.Emphasize(myText, baseFontSize, fontSizeMultiplier));
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

}
