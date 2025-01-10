using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CitizenManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private SpriteRenderer spriteRenderer;

    //Time intervals
    [SerializeField] private float pauseTime = 2f;
    [SerializeField] private float moveTime = 1f;

    //rb movement
    [SerializeField] private float citizenSpeed = 2f;
    [SerializeField] private float accelerationRate = 8f;
    [SerializeField] private float currentHorizontalStrength;
    private float horizontalMaxInput = 1f;
    private float movement = 0f;
    private float randomDirection = 1f;

    private bool canCitizenMove = false;

    //Chance to move direction
    private float screenLeftBoundary = -7.5f; 
    private float screenRightBoundary = 7.5f;

    //Invincibility
    [SerializeField] public bool citizenIsFrozen = false;
    [SerializeField] private float invincibilityDuration;
    [SerializeField] private float freezeTime = 3f;
    [SerializeField] private float flashInterval = 0.2f;
    [SerializeField] private bool isInvincible = false;

    //Layers
    private int citizenLayer = 12;
    private int ballLayer = 9;
    private int ballGuardLayer = 11;

    //ProvideThanks
    private ParticleSystem coinPS;
    private Transform thanksReactionTransform;
    private GameObject thanksReaction;
    private TextMeshProUGUI thanksEarned;
    [SerializeField] private float reactionOffsetY = 0.9f;
    [SerializeField] private Texture2D ecstaticReaction;
    [SerializeField] private Texture2D gladReaction;
    [SerializeField] private Texture2D neutralReaction;
    [SerializeField] private Texture2D sadReaction;

    //[SerializeField] private float chanceToMove = 0.5f;
    void Start()
    {
        coinPS = transform.Find("CitizenThanksPS").GetComponent<ParticleSystem>();
        thanksReactionTransform = transform.Find("CitizenCanvas").Find("ThanksReaction");
        thanksReaction = thanksReactionTransform.gameObject;
        thanksEarned = thanksReactionTransform.Find("ThanksEarned").GetComponent<TextMeshProUGUI>();
        thanksReaction.SetActive(false);

        invincibilityDuration = freezeTime + 1.2f;

        StartCoroutine(MoveBackAndForth());
    }

    // Update is called once per frame
    void Update()
    {
        if (!citizenIsFrozen)
        {
            spriteRenderer.color = Color.magenta;
            if (canCitizenMove)
            {
                currentHorizontalStrength = Mathf.MoveTowards(currentHorizontalStrength, horizontalMaxInput * randomDirection, accelerationRate * Time.deltaTime);
            }
            else
            {
                currentHorizontalStrength = Mathf.MoveTowards(currentHorizontalStrength, 0f, accelerationRate * Time.deltaTime);
            }
        }
        else
        {
            spriteRenderer.color = Color.gray;
            movement = 0f;
            currentHorizontalStrength = 0f;

            if (!isInvincible)
            {
                StartCoroutine(InvincibilityPeriod());
                StartCoroutine(FlashDuringInvincibility());
            }
        }
    }

    private void FixedUpdate()
    {
        //movement = currentHorizontalStrength * (citizenSpeed - appliedPuddleBuffer);
        movement = currentHorizontalStrength * (citizenSpeed);
        rb2d.velocity = new Vector2(movement, 0f);
    }

    public void GiveThanks()
    {
        coinPS.Play();
        StartCoroutine(ShowReaction());
    }












    //updateCurrency here with the new text in a new function to change thanksEarned.text and base the reaction off of the number in text















    private IEnumerator ShowReaction()
    {
        thanksReaction.SetActive(true);
        Vector3 startPosition = new Vector3(transform.position.x, transform.position.y + reactionOffsetY, 0f);
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + reactionOffsetY + 0.6f, 0f);

        Color startColor = Color.white;
        Color targetColor = new Color(1f, 1f, 1f, 0f);

        float startTime = Time.time;
        float glideDuration = 0.8f;

        while ((Time.time - startTime) < glideDuration)
        {
            float glideProgress = (Time.time - startTime) / glideDuration;

            thanksReaction.GetComponent<RectTransform>().position = Vector3.Lerp(startPosition, targetPosition, glideProgress);
            thanksReaction.GetComponent<Image>().color = Color.Lerp(startColor, targetColor, glideProgress);
            thanksEarned.color = Color.Lerp(startColor, targetColor, glideProgress);

            yield return null;
        }

        thanksReaction.SetActive(false);
    }


    private IEnumerator MoveBackAndForth()
    {
        while (true)
        {
            yield return new WaitForSeconds(pauseTime);
            float position = transform.position.x;
            float distanceToLeftWall = Mathf.Abs(position - screenLeftBoundary);
            float distanceToRightWall = Mathf.Abs(position - screenRightBoundary);

            float totalDistance = distanceToLeftWall + distanceToRightWall;
            float moveLeftProbability = distanceToRightWall / totalDistance;
            //Debug.Log("Left prob" + moveLeftProbability);
            randomDirection = Random.value < moveLeftProbability ? 1 : -1;
            //Debug.Log("Random direction" + randomDirection);

            canCitizenMove = true;
            yield return new WaitForSeconds(moveTime);
            canCitizenMove = false;
        }
    }

    private IEnumerator FlashDuringInvincibility()
    {
        float elapsedTime = 0f;

        while (elapsedTime < invincibilityDuration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(flashInterval);
            elapsedTime += flashInterval;
        }

        //Citizen is visible at the end
        spriteRenderer.enabled = true;
    }

    private IEnumerator InvincibilityPeriod()
    {
        Physics2D.IgnoreLayerCollision(citizenLayer, ballLayer, true);
        Physics2D.IgnoreLayerCollision(citizenLayer, ballGuardLayer, true);
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        Physics2D.IgnoreLayerCollision(citizenLayer, ballLayer, false);
        Physics2D.IgnoreLayerCollision(citizenLayer, ballGuardLayer, false);
        isInvincible = false;
    }

    public void startFreezeCitizenCoroutine()
    {
        StartCoroutine(freezeCitizen());
    }
    public IEnumerator freezeCitizen()
    {
        if (citizenIsFrozen) yield break;

        citizenIsFrozen = true;
        //playerHealthy = false;

        yield return new WaitForSeconds(freezeTime);

        //playerHealthy = true;
        citizenIsFrozen = false;

    }
}
