using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPipe : MonoBehaviour
{
    private ShopManager shopManager;
    private OpenPipeButton buttonScript;
    [SerializeField] private GameObject locationButton;


    // Start is called before the first frame update
    void Start()
    {
        shopManager = FindObjectOfType<ShopManager>();
        buttonScript = locationButton.GetComponent<OpenPipeButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shopManager.shopContent.activeSelf)
        {
            buttonScript.shopOpen = true;
        }
        else
        {
            buttonScript.shopOpen = false;
        }
    }
}
