using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public Text textToDisplay;
    public float displayDuration = 1f; 
    public float fadeDuration = 1f;    

    private void Start()
    {
        textToDisplay.gameObject.SetActive(true);
        StartCoroutine(FadeOutAfterDelay(displayDuration, fadeDuration));
    }


    IEnumerator FadeOutAfterDelay(float delay, float fadeTime)
    {
        yield return new WaitForSeconds(delay);

        float elapsedTime = 0f;
        Color initialColor = textToDisplay.color;

        while (elapsedTime < fadeTime)
        {
            textToDisplay.color = Color.Lerp(initialColor, new Color(initialColor.r, initialColor.g, initialColor.b, 0f), elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        textToDisplay.color = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        textToDisplay.gameObject.SetActive(false);
    }
}
