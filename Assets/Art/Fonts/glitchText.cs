using TMPro;
using UnityEngine;
using System.Collections;

public class GlitchText : MonoBehaviour
{
    public TMP_Text textComponent;
    private string originalText;
    public float glitchInterval = 0.1f; // kaç saniyede bir glitch yapsýn
    public float glitchDuration = 0.05f; // her glitch ne kadar sürsün
    private bool isGlitching = true;

    private void Start()
    {
        originalText = textComponent.text;
        StartCoroutine(GlitchLoop());
    }

    IEnumerator GlitchLoop()
    {
        while (isGlitching)
        {
            yield return new WaitForSeconds(glitchInterval);
            string glitchedText = GetGlitchedText(originalText);
            textComponent.text = glitchedText;

            yield return new WaitForSeconds(glitchDuration);
            textComponent.text = originalText;
        }
    }

    string GetGlitchedText(string input)
    {
        char[] chars = input.ToCharArray();
        for (int i = 0; i < chars.Length; i++)
        {
            if (Random.value > 0.85f && chars[i] != ' ' && chars[i] != '\n')
            {
                chars[i] = (char)Random.Range(33, 126); // ASCII: ! to ~
            }
        }
        return new string(chars);
    }
}