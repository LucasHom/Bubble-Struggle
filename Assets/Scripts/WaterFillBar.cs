using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterFillBar : MonoBehaviour
{
    [SerializeField] private Image waterFillMask;

    private GenerateWater WaterGenerator;
    // Start is called before the first frame update
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
        float fillAmount = WaterGenerator.remainingWater / WaterGenerator.maxWater;
        waterFillMask.fillAmount = fillAmount;
    }
}
