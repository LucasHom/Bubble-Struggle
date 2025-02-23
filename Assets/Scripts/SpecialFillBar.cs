using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialFillBar : MonoBehaviour
{
    [SerializeField] private Image specialFillMask;
    private GenerateWater WaterGenerator;

    [SerializeField] private float currentFill = 1f;
    private float barFillAmount = 1f;

    [SerializeField] private float barFillDelay = 0.05f;

    void Start()
    {
        WaterGenerator = FindObjectOfType<GenerateWater>();

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        specialFillMask.fillAmount = barFillAmount;
    }

    public IEnumerator fillSpecialBar()
    {
        barFillAmount = 0f;
        currentFill = 0f;
        while (barFillAmount < 1f)
        {
            barFillAmount = currentFill / 1f;
            currentFill += 0.01f;
            yield return new WaitForSeconds(barFillDelay);
        }
        WaterGenerator.specialReady = true;
    }
}
