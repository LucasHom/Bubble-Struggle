using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSecurityManager : MonoBehaviour
{
    [SerializeField] private GameObject lockedButtonPrefab;

    private static Dictionary<string, GameObjectPair> shopButtonDict;

    [SerializeField] private Transform UIParent;

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
        shopButtonDict = new Dictionary<string, GameObjectPair>
        {
            { "WaterCapacityButton", new GameObjectPair(waterCapacityButton) },
            { "ReloadSpeedButton", new GameObjectPair(reloadSpeedButton) },
            { "PlayerSpeedButton", new GameObjectPair(playerSpeedButton) },
            { "UmbrellaButton", new GameObjectPair(umbrellaButton) },
            //{ "", object5 },
            { "MedpackButton", new GameObjectPair(medpackButton) }
            //{ "", object7 },
            //{ "", object8 },
            //{ "", object9 }
        };

        //Lock("WaterCapacityButton");
        //Lock("ReloadSpeedButton");
        //Lock("PlayerSpeedButton");
        //Lock("UmbrellaButton");
        //Lock("MedpackButton");

        //Unlock("WaterCapacityButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Unlock(string button)
    {
        if(shopButtonDict[button].object2 != null)
        {
            Destroy(shopButtonDict[button].object2);
        }
        shopButtonDict[button].object1.SetActive(true);
    }

    public void Lock(string button)
    {
        shopButtonDict[button].object2 = Instantiate(lockedButtonPrefab, shopButtonDict[button].object1.transform.position, Quaternion.identity, UIParent);
        shopButtonDict[button].object1.SetActive(false);
    }
}
