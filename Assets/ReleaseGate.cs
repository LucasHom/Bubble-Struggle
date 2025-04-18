using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReleaseGate : MonoBehaviour
{
    public Image uiImage;     
    public Sprite[] frames;    
    public float frameRate = 12f; 

    private int currentFrame = 0;
    private float timer = 0f;
    private bool isPlaying = false;



    void Start()
    {
        if (frames.Length > 0)
            uiImage.sprite = frames[0]; 
        StartCoroutine(WaitToStart()); // Start the coroutine to wait before playing
    }

    private IEnumerator WaitToStart()
    {
        yield return new WaitForSecondsRealtime(3f);
        isPlaying = true;
    }

    void Update()
    {
        if (!isPlaying || frames.Length == 0)
            return;

        timer += Time.unscaledDeltaTime; 

        if (timer >= 1f / frameRate)
        {
            currentFrame++;
            timer = 0f;

            if (currentFrame < frames.Length)
            {
                uiImage.sprite = frames[currentFrame];
            }
            else
            {
                isPlaying = false;
                Destroy(gameObject);
            }
        }
    }
}
