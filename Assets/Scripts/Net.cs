using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Net : MonoBehaviour
{
    private Coroutine slowDownRoutine;
    [SerializeField] private TextMeshProUGUI netDurabilityText;
    [SerializeField] int maxDurbility = 20;
    public int durability;

    private float minSpawnX = -6.8f;
    private float maxSpawnX = 6.8f;
    private float minSpawnY = -1.5f;
    private float maxSpawnY = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        durability = maxDurbility;
        createSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        netDurabilityText.text = $"{durability}";
        if (durability <= 0)
        {
            //DO A FANCY DESTROY






            Debug.Log("Make a destroy net coroutine");
            Destroy(gameObject);











        }
    }

    private void createSpawnPosition()
    {
        transform.position = new Vector2(Random.Range(minSpawnX, maxSpawnX), Random.Range(minSpawnY, maxSpawnY));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GameObject col = collision.gameObject;
        if (col.tag == "Net")
        {
            createSpawnPosition();
        }

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
