using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInUI : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 1f;  // Duration of the fade-in effect
    private CanvasGroup canvasGroup;

    public bool isVisible;

    void Awake()
    {
        // Get the CanvasGroup component attached to the same GameObject
        canvasGroup = GetComponent<CanvasGroup>();

        // Ensure the UI is initially invisible
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }

        isVisible = false;
    }

    public void FadeIn()
    {
        if (canvasGroup != null)
        {
            StartCoroutine(FadeInRoutine());
        }
    }

    private IEnumerator FadeInRoutine()
    {
        float elapsedTime = 0f;

        // Fade in over the specified duration
        while (elapsedTime < fadeDuration)
        {
            // Gradually increase the alpha value
            elapsedTime += Time.unscaledDeltaTime; // Using unscaled delta time to ensure it works even when time is paused
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            yield return null;
        }

        // Ensure the alpha is fully 1 at the end
        canvasGroup.alpha = 1f;
        isVisible = true; // Set the visibility flag to true
    }
}
