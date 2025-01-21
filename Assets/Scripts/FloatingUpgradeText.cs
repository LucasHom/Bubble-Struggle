using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingUpgradeText : MonoBehaviour
{
    [SerializeField] private ShopManager ShopManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FloatAndDeleteText(GameObject floatingTextObject, float glideSpeed)
    {
        floatingTextObject.transform.Rotate(0, 0, 6);
        RectTransform rectTransform = floatingTextObject.GetComponent<RectTransform>();

        Vector2 startPosition = rectTransform.position;
        Vector2 targetPosition = startPosition + new Vector2(-0.2f, 1f);

        TextMeshProUGUI textComponent = floatingTextObject.GetComponent<TextMeshProUGUI>();
        Color startColor = textComponent.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        float glideDuration = glideSpeed;
        float elapsedTime = 0f;

        while (elapsedTime < glideDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float glideProgress = elapsedTime / glideDuration;

            rectTransform.position = Vector2.Lerp(startPosition, targetPosition, glideProgress);
            textComponent.color = Color.Lerp(startColor, targetColor, glideProgress);

            yield return null;
        }
        Destroy(floatingTextObject);
    }
}
