using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [SerializeField] public float startingCloudHeight = 10f;
    [SerializeField] private float moveDuration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0f, startingCloudHeight, 0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeCloudHeight(float height)
    {
        StartCoroutine(ChangeCloudHeightCoroutine(height));
    }
    private IEnumerator ChangeCloudHeightCoroutine(float height)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y + height, transform.position.z);
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

    }
}