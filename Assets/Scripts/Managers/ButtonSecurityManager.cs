using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSecurityManager : MonoBehaviour
{
    [SerializeField] private GameObject lockedButtonPrefab;

    private static Dictionary<string, GameObject> shopButtonDict;

    //Buttons
    [SerializeField] private GameObject waterCapacityButton;
    [SerializeField] private GameObject reloadSpeedButton;
    [SerializeField] private GameObject playerSpeedButton;

    [SerializeField] private GameObject umbrellaButton;
    //[SerializeField] private GameObject waterCapacityButton;
    [SerializeField] private GameObject medpackButton;

    //[SerializeField] private GameObject waterCapacityButton;
    //[SerializeField] private GameObject waterCapacityButton;
    //[SerializeField] private GameObject waterCapacityButton;

    // Start is called before the first frame update
    void Start()
    {
        shopButtonDict = new Dictionary<string, GameObject>
        {
            { "WaterCapacityButton", waterCapacityButton },
            { "ReloadSpeedButton", reloadSpeedButton },
            { "PlayerSpeedButton", playerSpeedButton },
            { "UmbrellaButton", umbrellaButton },
            //{ "", object5 },
            { "MedpackButton", medpackButton }
            //{ "", object7 },
            //{ "", object8 },
            //{ "", object9 }
        };

        Lock("WaterCapacityButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Unlock(string button)
    {
        shopButtonDict[button].SetActive(true);
    }

    public void Lock(string button)
    {

        shopButtonDict[button].SetActive(false);
    }
}
