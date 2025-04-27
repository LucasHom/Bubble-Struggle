using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmphasizeObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Example usage (optional): StartCoroutine(Emphasize(gameObject, Vector3.one, 1.2f));
    }

    public static IEnumerator Emphasize(GameObject obj, Vector3 baseScale, float scaleMultiplier)
    {
        Transform objTransform = obj.transform;

        float elapsedTime = 0f;
        float growTime = 0.3f;
        float shrinkTime = 0.3f;

        while (true)
        {
            elapsedTime = 0f;

            // Growing
            while (elapsedTime < growTime)
            {
                elapsedTime += Time.unscaledDeltaTime;

                float t = EaseIn(elapsedTime / growTime);
                Vector3 newScale = Vector3.Lerp(baseScale, baseScale * scaleMultiplier, t);

                objTransform.localScale = newScale;

                yield return null;
            }

            // Shrinking
            elapsedTime = 0f;

            while (elapsedTime < shrinkTime)
            {
                elapsedTime += Time.unscaledDeltaTime;

                float t = EaseOut(elapsedTime / shrinkTime);
                Vector3 newScale = Vector3.Lerp(baseScale * scaleMultiplier, baseScale, t);

                objTransform.localScale = newScale;

                yield return null;
            }
        }
    }

    private static float EaseOut(float time)
    {
        return Mathf.Pow(time - 1f, 3) + 1f;
    }

    private static float EaseIn(float time)
    {
        return Mathf.Pow(time, 3);
    }
}
