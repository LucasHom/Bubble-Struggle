using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
    [SerializeField] private GameObject netButton;
    [SerializeField] private GameObject medpackButton;

    [SerializeField] private GameObject waterPipeButton;
    [SerializeField] private GameObject FreezePipeButton;
    [SerializeField] private GameObject ShieldBubblePipeButton;

    // Start is called before the first frame update
    void Start()
    {
        shopButtonDict = new Dictionary<string, GameObjectPair>
        {
            { "WaterCapacityButton", new GameObjectPair(waterCapacityButton) },
            { "ReloadSpeedButton", new GameObjectPair(reloadSpeedButton) },
            { "PlayerSpeedButton", new GameObjectPair(playerSpeedButton) },

            { "UmbrellaButton", new GameObjectPair(umbrellaButton) },
            { "NetButton", new GameObjectPair(netButton) },
            { "MedpackButton", new GameObjectPair(medpackButton) },

            { "WaterPipeButton", new GameObjectPair(waterPipeButton) },
            { "FreezePipeButton", new GameObjectPair(FreezePipeButton) },
            { "ShieldBubblePipeButton", new GameObjectPair(ShieldBubblePipeButton) }
        };

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
