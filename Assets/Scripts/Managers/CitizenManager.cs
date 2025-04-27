using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    //-----------------
    //Thanks
    //Express Thanks
    [SerializeField] private ParticleSystem coinPS;
    private Transform thanksReactionTransform;
    private GameObject thanksReaction;
    private TextMeshProUGUI thanksEarned;
    [SerializeField] private float reactionOffsetY = 0.9f;
    [SerializeField] private Sprite ecstaticReaction;
    [SerializeField] private Sprite happyReaction;
    [SerializeField] private Sprite neutralReaction;
    [SerializeField] private Sprite worriedReaction;
    [SerializeField] private Sprite sadReaction;


    //Calculate Thanks
    [SerializeField] public int maxThanks; //Allow to change when the wave changes
    [SerializeField] private float lowestThanksPercent = 0.5f;
    private Dictionary<int, float> healthToThanks = new Dictionary<int, float>();
    //------------------

    //Health 
    [SerializeField] public int maxCitizenHealth = 5;
    [SerializeField] public int citizenHealth;
    [SerializeField] private ParticleSystem healthPS;

    //Shop
    [SerializeField] private ShopManager shopManager;


    void Start()
    {
        //Create health and corresponding percent of thanks to give
        citizenHealth = maxCitizenHealth;
        createThanksPercentTable(maxCitizenHealth, 1f);

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

        if (Input.GetKeyDown(KeyCode.R)) 
        { 
            GiveThanks();
        }

    }

    private void FixedUpdate()
    {
        //movement = currentHorizontalStrength * (citizenSpeed - appliedPuddleBuffer);
        movement = currentHorizontalStrength * (citizenSpeed);
        rb2d.velocity = new Vector2(movement, 0f);
    }


    public void createThanksPercentTable(int health, float percent)
    {
        if (health <= 0)
        {
            healthToThanks.Add(0, lowestThanksPercent);
            return;
        }

        float decrement = (1f - lowestThanksPercent) / (maxCitizenHealth - 1);

        healthToThanks.Add(health, percent);

        if (percent <= lowestThanksPercent)
        {
            createThanksPercentTable(health - 1, lowestThanksPercent);
        }
        else
        {
            createThanksPercentTable(health - 1, percent - decrement);
        }
    }


    public void GiveThanks()
    {
        int thanksAmount = calcThanks();
        updateReaction(thanksAmount);
        shopManager.StartIncreaseCurrency(thanksAmount);
        coinPS.Play();
        StartCoroutine(ShowReaction());
    }


    private int calcThanks()
    {
        return (int)(healthToThanks[citizenHealth] * maxThanks);
    }

    private void updateReaction(int thanksAmount)
    {
        Sprite face = thanksReaction.GetComponent<Image>().sprite;
        thanksEarned.text = string.Format("${0}", thanksAmount.ToString());

        float healthPercentage = (float)citizenHealth / maxCitizenHealth;
        //Debug.Log(healthPercentage);

        if (healthPercentage >= 1f) 
        {
            face = ecstaticReaction;
        }
        else if (healthPercentage >= 0.8f) 
        {
            face = happyReaction;
        }
        else if (healthPercentage >= 0.6f) 
        {
            face = neutralReaction;
        }
        else if (healthPercentage >= 0.4f) 
        {
            face = worriedReaction;
        }
        else if (healthPercentage > 0.2)
        {
            face = sadReaction;
        }
        else
        {
            face = sadReaction;
            throw new ArgumentOutOfRangeException("Citizen cannot give reaction with health <= 0, Game should have ended");
        }
        thanksReaction.GetComponent<Image>().sprite = face;
    }


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
            randomDirection = UnityEngine.Random.value < moveLeftProbability ? 1 : -1;
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

    public void AttemptHeal()
    {
        if (citizenHealth < maxCitizenHealth)
        {
            citizenHealth++;
            healthPS.Play();
        }
        
    }

    public int getCitizenHealth()
    {
        return citizenHealth;
    }

    public void setCitizenHealth(int health)
    {
        {
            citizenHealth = health;
        }
    }
}
