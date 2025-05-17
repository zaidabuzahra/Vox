using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text textDisplay;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private string cachedInitialText;

    private void Start()
    {
        // Save whatever text is in the TMP field at start
        cachedInitialText = textDisplay.text;

        // Start typing that cached text
        StartTypewriter(cachedInitialText);
    }

    public void StartTypewriter(string message)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        textDisplay.text = "";
        foreach (char letter in message)
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
