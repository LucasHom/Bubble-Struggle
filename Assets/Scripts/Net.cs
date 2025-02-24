using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public class Net : MonoBehaviour
{
    private Coroutine slowDownRoutine;
    [SerializeField] private TextMeshProUGUI netDurabilityText;
    [SerializeField] int maxDurbility = 20;
    public int durability;


    //Spawning
    private float minSpawnX = -6.8f;
    private float maxSpawnX = 6.8f;
    private float minSpawnY = -1.5f;
    private float maxSpawnY = 0.0f;
    private Vector2 spawnPosition = default;
    private int attempts = 0;
    private bool positionFound = false;
    private Vector2 boxSize = new Vector2(1.5f, 1.5f);



    private LayerMask netLayer;




    public static List<Net> activeNets = new List<Net>();

    // Start is called before the first frame update
    void Start()
    {
        netLayer = LayerMask.GetMask("Net");
        PurchaseNet purchaseNet = FindObjectOfType<PurchaseNet>();

        durability = maxDurbility;
        //createSpawnPosition();
        CreateSpawnPosition();
        activeNets.Add(this);

        purchaseNet.determineIsReady();
    }

    // Update is called once per frame
    void Update()
    {
        netDurabilityText.text = $"{durability}";
        if (durability <= 0)
        {
            //DO A FANCY DESTROY






            Debug.Log("Make a destroy net coroutine");
            activeNets.Remove(this);
            Destroy(gameObject);











        }
    }

    //private void createSpawnPosition()
    //{
    //    transform.position = new Vector2(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY));

    //}

    private void CreateSpawnPosition()
    {
        do
        {
            spawnPosition = new Vector2(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY));

            Collider2D hit = Physics2D.OverlapBox(spawnPosition, boxSize, 0f, netLayer);

            if (hit == null)
            {
                //Debug.Log("position found");
                positionFound = true;
            }
            else
            {
                //Debug.Log("hit other net");
            }
            attempts++;
        }
        while (!positionFound && attempts < 100);

        if (positionFound)
        {
            transform.position = spawnPosition;
        }
        else
        {
            Debug.LogWarning("Couldn't find a valid spawn position after 100 attempts.");
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (spawnPosition != default)
        {
            Gizmos.DrawWireCube(spawnPosition, boxSize);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject col = collision.gameObject;

        if (col.tag == "SupportBall")
        {
            SupportBall supportBall = col.GetComponent<SupportBall>();
            if (supportBall.isMaxSize == false)
            {
                slowDownRoutine = StartCoroutine(supportBall.StickToNet(gameObject));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.tag == "SupportBall")
        {
            Rigidbody2D ballrb2d = col.GetComponent<Rigidbody2D>();
            ballrb2d.gravityScale = 1f;
            if (slowDownRoutine != null)
            {
                StopCoroutine(slowDownRoutine);
                slowDownRoutine = null;
            }
        }
    }
}
