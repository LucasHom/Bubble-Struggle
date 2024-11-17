using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CitizenManager : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb2d;

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

    //[SerializeField] private float chanceToMove = 0.5f;
    void Start()
    {
        StartCoroutine(MoveBackAndForth());
    }

    // Update is called once per frame
    void Update()
    {
        if (canCitizenMove)
        {
            currentHorizontalStrength = Mathf.MoveTowards(currentHorizontalStrength, horizontalMaxInput * randomDirection, accelerationRate * Time.deltaTime);
        }
        else
        {
            currentHorizontalStrength = Mathf.MoveTowards(currentHorizontalStrength, 0f, accelerationRate * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        //movement = currentHorizontalStrength * (citizenSpeed - appliedPuddleBuffer);
        movement = currentHorizontalStrength * (citizenSpeed);
        rb2d.velocity = new Vector2(movement, 0f);
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
}
