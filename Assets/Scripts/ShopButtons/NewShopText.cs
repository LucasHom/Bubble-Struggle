using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewShopText : MonoBehaviour
{
    private TextMeshProUGUI myText;
    [SerializeField] float baseFontSize;
    [SerializeField] float fontSizeMultiplier;

    private EmphasizeText emphasizeText;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
        emphasizeText = GetComponent<EmphasizeText>();
    }

    private void OnEnable()
    {
        StartCoroutine(emphasizeText.Emphasize(myText, baseFontSize, fontSizeMultiplier));
    }
}
