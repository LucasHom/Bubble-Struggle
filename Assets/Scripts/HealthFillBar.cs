using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFillBar : MonoBehaviour
{
    [SerializeField] private Image healthFillMask;

    private CitizenManager citizen;
    // Start is called before the first frame update
    void Start()
    {
        citizen = FindObjectOfType<CitizenManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float fillAmount = (float)citizen.getCitizenHealth() / (float)citizen.maxCitizenHealth;
        healthFillMask.fillAmount = fillAmount;
    }
}
